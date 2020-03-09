using System.Linq;
using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Helpers;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ObjectResult GetUserCompanies(CancellationToken cancellationToken = default)
        {
            var userCompanies = _unitOfWork.GetRepository<UserCompanyModel>()
                .GetMany(
                source => !source.IsRemoved, 
                null, 
                TrackingState.Disabled)
                .ToList();
            var userCompaniesViewModels = _mapper.Map<UserCompanyViewModel>(userCompanies);

            var result = new OkObjectResult(userCompaniesViewModels);
            return result;
        }

        public ObjectResult GetUserCompanyByEmail(string email, CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                source => source.Email == email, 
                TrackingState.Disabled,
                "Offices.City.Country");
            if (userCompany == null)
            {
                var notFoundResult = new NotFoundObjectResult("Company by provided email not found");
                return notFoundResult;
            }

            var userViewModel = _mapper.Map<UserCompanyViewModel>(userCompany);

            var result = new OkObjectResult(userViewModel);
            return result;
        }

        public ObjectResult CreateUser(UserCompanyModel userCompany, CancellationToken cancellationToken = default)
        {
            var isUserCompanyExist = _unitOfWork.GetRepository<UserCompanyModel>().IsExist(
                userCompanyModel => userCompanyModel.Email == userCompany.Email);
            if (isUserCompanyExist)
            {
                var badResult = BadRequestObjectResultFactory.Create(
                    nameof(userCompany.Email),
                    "Company with provided email already exist");
                return badResult;
            }

            userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Create(userCompany);
            _unitOfWork.Save();
            var userCompanyViewModel = _mapper.Map<UserCompanyViewModel>(userCompany);

            var result = new OkObjectResult(userCompanyViewModel);
            return result;
        }

        public ObjectResult UpdateUserCompany(string email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public ObjectResult DeleteUserCompany(string email, CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == email);
            userCompany.IsRemoved = true;
            _unitOfWork.Save();

            var result = new OkObjectResult(null);
            return result;
        }
    }
}
