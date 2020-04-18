using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Request;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ObjectResult GetRequests(CancellationToken cancellationToken = default)
        {
            var requests = _unitOfWork.GetRepository<RequestModel>()
                .GetMany(
                    requestModel => !requestModel.IsRemoved,
                    null,
                    TrackingState.Disabled,
                    "Objective.SenderCompany.Offices.City.Region.Country",
                    "Objective.ReceiverCompany.Offices.City.Region.Country",
                    "Objective.Location.City.Region.Country",
                    "Objective.Good",
                    "Objective.Rules",
                    "UserCompany")
                .ToList();
            var requestViewModels = _mapper.Map<List<RequestViewModel>>(requests);

            var result = new OkObjectResult(requestViewModels);
            return result;
        }

        public ObjectResult GetRequestById(string id, CancellationToken cancellationToken = default)
        {
            var request = _unitOfWork.GetRepository<RequestModel>().Get(
                requestModel => requestModel.RequestId == id,
                TrackingState.Disabled,
                "Objective.SenderCompany.Offices.City.Region.Country",
                "Objective.ReceiverCompany.Offices.City.Region.Country",
                "Objective.Location.City.Region.Country",
                "Objective.Good",
                "Objective.Rules",
                "UserCompany");
            if (request == null)
            {
                var notFoundResult = new NotFoundObjectResult("Request by provided id not found");
                return notFoundResult;
            }

            var requestViewModel = _mapper.Map<RequestViewModel>(request);

            var result = new OkObjectResult(requestViewModel);
            return result;
        }

        public ObjectResult CreateRequest(RequestModel createRequestModel, CancellationToken cancellationToken = default)
        {
            var request = _unitOfWork.GetRepository<RequestModel>().Create(createRequestModel);
            _unitOfWork.Save();
            var requestViewModel = _mapper.Map<RequestViewModel>(request);

            var result = new OkObjectResult(requestViewModel);
            return result;
        }

        public ObjectResult UpdateRequest(
            string id,
            RequestUpdateModel updateRequestModel,
            CancellationToken cancellationToken = default)
        {
            var request = _unitOfWork.GetRepository<RequestModel>().Get(
                requestModel => requestModel.RequestId == id,
                TrackingState.Enabled,
                "Objective",
                "UserCompany");
            if (request == null)
            {
                var notFoundResult = new NotFoundObjectResult("Request by provided id not found");
                return notFoundResult;
            }

            request.Status = updateRequestModel.Status;
            _unitOfWork.Save();

            var requestViewModel = _mapper.Map<RequestViewModel>(request);

            var result = new OkObjectResult(requestViewModel);
            return result;
        }

        public ObjectResult DeleteRequest(string id, CancellationToken cancellationToken = default)
        {
            var request = _unitOfWork.GetRepository<RequestModel>().Get(
                requestModel => requestModel.RequestId == id,
                TrackingState.Disabled);
            if (request == null)
            {
                var notFoundResult = new NotFoundObjectResult("Request by provided key not found");
                return notFoundResult;
            }

            _unitOfWork.GetRepository<RequestModel>().Delete(request.RequestId);
            _unitOfWork.Save();

            var result = new OkObjectResult(null);
            return result;
        }

        public ObjectResult GetRequestsByCompanyId(
            string id, 
            CancellationToken cancellationToken = default)
        {
            var requests = _unitOfWork.GetRepository<RequestModel>()
                .GetMany(requestModel => requestModel.CompanyId.Equals(id),
                    null,
                    TrackingState.Disabled,
                    "Objective.SenderCompany.Offices.City.Region.Country",
                    "Objective.ReceiverCompany.Offices.City.Region.Country",
                    "Objective.Location.City.Region.Country",
                    "Objective.Good",
                    "Objective.Rules",
                    "UserCompany")
                .ToList();
            var requestViewModels = _mapper.Map<List<RequestViewModel>>(requests);

            var result = new OkObjectResult(requestViewModels);
            return result;
        }
    }
}
