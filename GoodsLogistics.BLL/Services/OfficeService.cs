using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Helpers;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO.Office;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfficeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ObjectResult GetOffices(CancellationToken cancellationToken = default)
        {
            var offices = _unitOfWork.GetRepository<OfficeModel>()
                .GetAll(
                    TrackingState.Disabled,
                    "City.Region")
                .ToList();
            var officeViewModels = _mapper.Map<List<OfficeViewModel>>(offices);

            var result = new OkObjectResult(officeViewModels);
            return result;
        }

        public ObjectResult GetOfficeByKey(
            string key,
            CancellationToken cancellationToken = default)
        {
            var office = _unitOfWork.GetRepository<OfficeModel>().Get(
                officeModel => officeModel.Key == key,
                TrackingState.Disabled,
                "City.Region.Country");
            if (office == null)
            {
                var notFoundResult = new NotFoundObjectResult("Office by provided key not found");
                return notFoundResult;
            }

            var officeViewModel = _mapper.Map<OfficeViewModel>(office);

            var result = new OkObjectResult(officeViewModel);
            return result;
        }

        public ObjectResult CreateOffice(
            OfficeModel office,
            CancellationToken cancellationToken = default)
        {
            var isOfficeExist = _unitOfWork.GetRepository<OfficeModel>().IsExist(
                officeModel => officeModel.Key == office.Key);
            if (isOfficeExist)
            {
                var badResult = BadRequestObjectResultFactory.Create(
                    nameof(office.Key),
                    "Office with provided key already exist");
                return badResult;
            }

            office = _unitOfWork.GetRepository<OfficeModel>().Create(office);
            _unitOfWork.Save();
            var officeViewModel = _mapper.Map<OfficeViewModel>(office);

            var result = new OkObjectResult(officeViewModel);
            return result;
        }

        public ObjectResult UpdateOffice(
            string key,
            OfficeUpdateRequestModel updateRequestModel,
            CancellationToken cancellationToken = default)
        {
            var office = _unitOfWork.GetRepository<OfficeModel>().Get(
                officeModel => officeModel.Key == key,
                TrackingState.Enabled,
                "City.Region.Country");
            if (office == null)
            {
                var notFoundResult = new NotFoundObjectResult("Office by provided key not found");
                return notFoundResult;
            }

            office.Address = updateRequestModel.Address ?? office.Address;
            office.CityId = updateRequestModel.CityId;
            _unitOfWork.Save();

            var officeViewModel = _mapper.Map<OfficeViewModel>(office);

            var result = new OkObjectResult(officeViewModel);
            return result;
        }

        public ObjectResult DeleteOffice(
            string key,
            CancellationToken cancellationToken = default)
        {
            var office = _unitOfWork.GetRepository<OfficeModel>().Get(
                officeModel => officeModel.Key == key,
                TrackingState.Enabled,
                "City.Region");
            if (office == null)
            {
                var notFoundResult = new NotFoundObjectResult("Office by provided key not found");
                return notFoundResult;
            }

            _unitOfWork.GetRepository<OfficeModel>().Delete(office.OfficeId);
            _unitOfWork.Save();

            var result = new OkObjectResult(null);
            return result;
        }

        public ObjectResult GetOfficesByCompanyId(string id, CancellationToken cancellationToken = default)
        {
            var offices = _unitOfWork.GetRepository<OfficeModel>()
                .GetMany(officeModel => officeModel.CompanyId.Equals(id),
                    null,
                    TrackingState.Disabled,
                    "City.Region")
                .ToList();
            var officeViewModels = _mapper.Map<List<OfficeViewModel>>(offices);

            var result = new OkObjectResult(officeViewModels);
            return result;
        }
    }
}
