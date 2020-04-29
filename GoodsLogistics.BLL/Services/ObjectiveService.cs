using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Extensions;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Enum;
using GoodsLogistics.Models.DTO.Objective;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class ObjectiveService : IObjectiveService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObjectiveService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ObjectResult GetObjectives(CancellationToken cancellationToken = default)
        {
            var objectives = _unitOfWork.GetRepository<ObjectiveModel>()
                .GetMany(
                    objectiveModel => !objectiveModel.IsRemoved,
                    null,
                    TrackingState.Disabled,
                    "SenderCompany.Offices.City.Region.Country",
                    "ReceiverCompany.Offices.City.Region.Country")
                .ToList();
            var objectiveViewModels = _mapper.Map<List<ObjectiveViewModel>>(objectives);

            var result = new OkObjectResult(objectiveViewModels);
            return result;
        }

        public ObjectResult GetObjectivesByFilter(
            ObjectiveFilteringModel filteringModel,
            CancellationToken cancellationToken = default)
        {
            var filterExpressions = new List<Expression<Func<ObjectiveModel, bool>>>();

            Expression<Func<ObjectiveModel, bool>> expression = x => !x.IsRemoved;
            filterExpressions.Add(expression);

            if (!string.IsNullOrEmpty(filteringModel.OnlyNotRequestedByCompanyId))
            {
                var objectivesId = _unitOfWork.GetRepository<RequestModel>()
                    .GetMany(m => m.CompanyId.Equals(filteringModel.OnlyNotRequestedByCompanyId)).Select(m => m.ObjectiveId);
                expression = m => !objectivesId.Contains(m.ObjectiveId);
                filterExpressions.Add(expression);
            }

            if (!string.IsNullOrEmpty(filteringModel.ReceiverCompanyEmail))
            {
                expression = m => m.ReceiverCompany.Email.Equals(filteringModel.ReceiverCompanyEmail);
                filterExpressions.Add(expression);
            }

            if (!string.IsNullOrEmpty(filteringModel.SenderCompanyEmail) && !filteringModel.IsWithoutSender)
            {
                expression = m => m.SenderCompany.Email.Equals(filteringModel.SenderCompanyEmail);
                filterExpressions.Add(expression);
            }

            if (string.IsNullOrEmpty(filteringModel.SenderCompanyEmail) && filteringModel.IsWithoutSender)
            {
                expression = m => string.IsNullOrEmpty(m.SenderCompanyId);
                filterExpressions.Add(expression);
            }

            if (filteringModel.PriceFrom != null)
            {
                expression = m => m.Price >= filteringModel.PriceFrom;
                filterExpressions.Add(expression);
            }

            if (filteringModel.PriceTo != null)
            {
                expression = m => m.Price <= filteringModel.PriceTo;
                filterExpressions.Add(expression);
            }

            if (filteringModel.CountryId != null)
            {
                expression = m => m.Location.City.Region.CountryId.Equals(filteringModel.CountryId);
                filterExpressions.Add(expression);
            }

            if (filteringModel.RegionId != null)
            {
                expression = m => m.Location.City.RegionId.Equals(filteringModel.RegionId);
                filterExpressions.Add(expression);
            }

            if (filteringModel.CityId != null)
            {
                expression = m => m.Location.CityId.Equals(filteringModel.CityId);
                filterExpressions.Add(expression);
            }

            var filter = filterExpressions.Combine(Expression.AndAlso);
            var objectives = _unitOfWork.GetRepository<ObjectiveModel>().GetMany(
                    filter, 
                    null, 
                    TrackingState.Disabled, 
                    "SenderCompany.Offices.City.Region.Country",
                    "ReceiverCompany.Offices.City.Region.Country",
                    "Location.City.Region.Country",
                    "Good");

            switch (filteringModel.SortingMethod)
            {
                case SortingMethod.FromLast:
                    objectives = objectives.OrderByDescending(m => m.CreationDate);
                    break;
                case SortingMethod.FromNew:
                default:
                    objectives = objectives.OrderBy(m => m.CreationDate);
                    break;

            }

            var objectivesViewModel = _mapper.Map<List<ObjectiveViewModel>>(objectives);
            var result = new OkObjectResult(objectivesViewModel);
            return result;
        }

        public ObjectResult GetObjectiveById(
            string id, 
            CancellationToken cancellationToken = default)
        {
            var objective = _unitOfWork.GetRepository<ObjectiveModel>().Get(
                objectiveModel => objectiveModel.ObjectiveId == id,
                TrackingState.Disabled,
                "SenderCompany.Offices.City.Region.Country",
                "ReceiverCompany.Offices.City.Region.Country",
                "Location.City.Region.Country",
                "Good",
                "Rules");
            if (objective == null)
            {
                var notFoundResult = new NotFoundObjectResult("Objective by provided id not found");
                return notFoundResult;
            }

            var objectiveViewModel = _mapper.Map<ObjectiveViewModel>(objective);

            var result = new OkObjectResult(objectiveViewModel);
            return result;
        }

        public ObjectResult CreateObjective(
            ObjectiveModel objective, 
            CancellationToken cancellationToken = default)
        {
            objective.CreationDate = DateTime.UtcNow;
            objective = _unitOfWork.GetRepository<ObjectiveModel>().Create(objective);
            _unitOfWork.Save();
            var objectiveViewModel = _mapper.Map<ObjectiveViewModel>(objective);

            var result = new OkObjectResult(objectiveViewModel);
            return result;
        }

        public ObjectResult UpdateObjective(
            string id, 
            ObjectiveUpdateRequestModel updateRequestModel,
            CancellationToken cancellationToken = default)
        {
            var objective = _unitOfWork.GetRepository<ObjectiveModel>().Get(
                objectiveModel => objectiveModel.ObjectiveId == id,
                TrackingState.Enabled,
                "SenderCompany.Offices.City.Region.Country",
                "ReceiverCompany.Offices.City.Region.Country",
                "Rules");
            if (objective == null)
            {
                var notFoundResult = new NotFoundObjectResult("Objective by provided id not found");
                return notFoundResult;
            }

            if (!string.IsNullOrEmpty(updateRequestModel.SenderCompanyId))
            {
                objective.SenderCompanyId = updateRequestModel.SenderCompanyId;
                var requests = _unitOfWork.GetRepository<RequestModel>().GetMany(
                    m => m.ObjectiveId.Equals(id), 
                    null, 
                    TrackingState.Enabled);

                foreach (var request in requests)
                {
                    request.Status = request.CompanyId.Equals(updateRequestModel.SenderCompanyId) 
                        ? RequestStatus.Accepted 
                        : RequestStatus.Declined;
                }
            }
            
            objective.Frequency = updateRequestModel.Frequency;
            objective.EndDate = updateRequestModel.EndDate == default(DateTime)
                ? objective.EndDate
                : updateRequestModel.EndDate;

            objective.OrderDate = updateRequestModel.OrderDate == default(DateTime)
                ? objective.EndDate
                : updateRequestModel.OrderDate;
            objective.Rules.Clear();
            objective.Rules = updateRequestModel.Rules;
            _unitOfWork.Save();

            var objectiveViewModel = _mapper.Map<ObjectiveViewModel>(objective);

            var result = new OkObjectResult(objectiveViewModel);
            return result;
        }

        public ObjectResult DeleteObjective(
            string id, 
            CancellationToken cancellationToken = default)
        {
            var objective = _unitOfWork.GetRepository<ObjectiveModel>().Get(
                objectiveModel => objectiveModel.ObjectiveId == id,
                TrackingState.Enabled,
                "SenderCompany.Offices.City.Region.Country",
                "ReceiverCompany.Offices.City.Region.Country");
            if (objective == null)
            {
                var notFoundResult = new NotFoundObjectResult("Objective by provided id not found");
                return notFoundResult;
            }

            objective.IsRemoved = true;
            _unitOfWork.Save();

            var result = new OkObjectResult(null);
            return result;
        }

        public ObjectResult GetObjectivesMinPrice(CancellationToken cancellationToken = default)
        {
            var objective = _unitOfWork
                .GetRepository<ObjectiveModel>()
                .GetMany(m => !m.IsRemoved, m => m.Price, TrackingState.Disabled)
                .FirstOrDefault();

            var price = objective?.Price ?? 0;
            var result = new OkObjectResult(price);
            return result;
        }

        public ObjectResult GetObjectivesMaxPrice(CancellationToken cancellationToken = default)
        {
            var objective = _unitOfWork
                .GetRepository<ObjectiveModel>()
                .GetMany(m => !m.IsRemoved, null, TrackingState.Disabled)
                .OrderByDescending(m => m.Price)
                .FirstOrDefault();

            var price = objective?.Price ?? 0;
            var result = new OkObjectResult(price);
            return result;
        }
    }
}
