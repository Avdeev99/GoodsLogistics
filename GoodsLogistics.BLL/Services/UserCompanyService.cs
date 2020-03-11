using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using GoodsLogistics.Auth.Tokens.Interfaces;
using GoodsLogistics.BLL.Helpers;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;

        public UserCompanyService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ITokenProvider tokenProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
        }

        public ObjectResult GetUserCompanies(CancellationToken cancellationToken = default)
        {
            var userCompanies = _unitOfWork.GetRepository<UserCompanyModel>()
                .GetMany(
                    userCompanyModel => !userCompanyModel.IsRemoved, 
                null, 
                TrackingState.Disabled,
                "Offices.City.Country")
                .ToList();
            var userCompaniesViewModels = _mapper.Map<List<UserCompanyViewModel>>(userCompanies);

            var result = new OkObjectResult(userCompaniesViewModels);
            return result;
        }

        public ObjectResult GetUserCompanyByEmail(
            string email, 
            CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == email, 
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

        public ObjectResult CreateUser(
            UserCompanyModel userCompany, 
            CancellationToken cancellationToken = default)
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

            var r = _unitOfWork.GetRepository<UserCompanyModel>();
            userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Create(userCompany);
            _unitOfWork.Save();
            var userCompanyViewModel = _mapper.Map<UserCompanyViewModel>(userCompany);

            var result = new OkObjectResult(userCompanyViewModel);
            return result;
        }

        public ObjectResult UpdateUserCompany(
            string email,
            UserCompanyUpdateRequestModel updateRequestModel,
            CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == email,
                TrackingState.Enabled,
                "Offices.City.Country");
            if (userCompany == null)
            {
                var notFoundResult = new NotFoundObjectResult("Company by provided email not found");
                return notFoundResult;
            }

            userCompany.Offices.Clear();
            userCompany.Offices = updateRequestModel.Offices;
            userCompany.Name = updateRequestModel.Name ?? userCompany.Name;
            _unitOfWork.Save();

            var userViewModel = _mapper.Map<UserCompanyViewModel>(userCompany);

            var result = new OkObjectResult(userViewModel);
            return result;
        }

        public ObjectResult DeleteUserCompany(
            string email, 
            CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == email,
                TrackingState.Enabled,
                "Offices.City.Country");
            if (userCompany == null)
            {
                var notFoundResult = new NotFoundObjectResult("Company by provided email not found");
                return notFoundResult;
            }

            userCompany.IsRemoved = true;
            _unitOfWork.Save();

            var result = new OkObjectResult(null);
            return result;
        }

        public ObjectResult RegisterUserCompany(
            UserCompanyModel userCompany,
            CancellationToken cancellationToken = default)
        {
            var creationObjectResult = CreateUser(userCompany, cancellationToken);
            if (creationObjectResult.StatusCode != 200)
            {
                return creationObjectResult;
            }

            var token = _tokenProvider.GenerateTokenForUser(userCompany);

            var result = new OkObjectResult(token);
            return result;
        }

        public ObjectResult LoginUserCompany(
            UserCompanyLoginRequestModel loginRequestModel, 
            CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == loginRequestModel.Email,
                TrackingState.Enabled,
                "Offices.City.Country");
            if (userCompany != null && userCompany.PasswordHash == loginRequestModel.PasswordHash)
            {
                var token = _tokenProvider.GenerateTokenForUser(userCompany);
                var result = new OkObjectResult(token);
                return result;
            }

            var badResult = BadRequestObjectResultFactory.Create(
                nameof(userCompany.Email),
                "Invalid credentials");
            return badResult;
        }
    }
}
