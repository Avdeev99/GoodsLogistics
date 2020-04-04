using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO;
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
                "Good");
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
                "ReceiverCompany.Offices.City.Region.Country");
            if (objective == null)
            {
                var notFoundResult = new NotFoundObjectResult("Objective by provided id not found");
                return notFoundResult;
            }

            objective.ReceiverCompanyId = updateRequestModel.ReceiverCompanyId;
            objective.EndDate = updateRequestModel.EndDate == default(DateTime)
                ? objective.EndDate
                : updateRequestModel.EndDate;
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
    }
}
