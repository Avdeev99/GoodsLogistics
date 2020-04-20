using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using CryptoHelper;
using GoodsLogistics.Auth.Tokens.Interfaces;
using GoodsLogistics.BLL.Helpers;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                "Offices.City.Region.Country")
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
                "Offices.City.Region.Country");
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

            userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Create(userCompany);
            _unitOfWork.Save();

            var userRole = _unitOfWork
                .GetRepository<RoleModel>()
                .Get(roleModel => roleModel.RoleId.Equals(userCompany.RoleId), TrackingState.Disabled);
            userCompany.Role = userRole;
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
                "Offices.City.Region.Country");
            if (userCompany == null)
            {
                var notFoundResult = new NotFoundObjectResult("Company by provided email not found");
                return notFoundResult;
            }

            if (!string.IsNullOrEmpty(updateRequestModel.NewPassword))
            {
                if (!Crypto.VerifyHashedPassword(userCompany.PasswordHash, updateRequestModel.OldPassword))
                {
                    var badRequestResult = BadRequestObjectResultFactory.Create(
                        nameof(updateRequestModel.OldPassword), 
                        "Wrong old password");
                    return badRequestResult;
                }

                var passwordHash = Crypto.HashPassword(updateRequestModel.NewPassword);
                userCompany.PasswordHash = passwordHash;
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
                "Offices.City.Region.Country");
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
            UserCompanyCreateRequestModel createRequestModel,
            CancellationToken cancellationToken = default)
        {
            var company = _unitOfWork
                .GetRepository<UserCompanyModel>()
                .Get(userCompanyModel => userCompanyModel.Email.Equals(createRequestModel.Email), 
                    TrackingState.Disabled);
            if (company != null)
            {
                var badResult = BadRequestObjectResultFactory.Create(
                    nameof(createRequestModel.Email),
                    "User with provided email already exists");
                return badResult;
            }

            var userRole = _unitOfWork
                .GetRepository<RoleModel>()
                .Get(roleModel => roleModel.Name.Equals(createRequestModel.Role.ToString()), TrackingState.Disabled);

            if (userRole == null)
            {
                var badResult = BadRequestObjectResultFactory.Create(
                    nameof(createRequestModel.Email),
                    "Registration failed.");
                return badResult;
            }

            var userCompany = _mapper.Map<UserCompanyModel>(createRequestModel);
            userCompany.RoleId = userRole.RoleId;

            var creationObjectResult = CreateUser(userCompany, cancellationToken);
            if (creationObjectResult.StatusCode != 200)
            {
                return creationObjectResult;
            }

            userCompany.Role = userRole;
            var token = _tokenProvider.GenerateTokenForUser(userCompany);

            var authResult = new AuthResultViewModel(
                token, 
                (UserCompanyViewModel)creationObjectResult.Value);

            var result = new OkObjectResult(authResult);
            return result;
        }

        public ObjectResult LoginUserCompany(
            UserCompanyLoginRequestModel loginRequestModel, 
            CancellationToken cancellationToken = default)
        {
            var userCompany = _unitOfWork.GetRepository<UserCompanyModel>().Get(
                userCompanyModel => userCompanyModel.Email == loginRequestModel.Email,
                TrackingState.Enabled,
                "Offices.City.Region.Country",
                "Role");
            if (userCompany != null
                && Crypto.VerifyHashedPassword(userCompany.PasswordHash, loginRequestModel.Password))
            {
                var token = _tokenProvider.GenerateTokenForUser(userCompany);
                var userCompanyViewModel = _mapper.Map<UserCompanyViewModel>(userCompany);
                var authResult = new AuthResultViewModel(token, userCompanyViewModel);

                var result = new OkObjectResult(authResult);
                return result;
            }

            var errors = new Dictionary<string, string>
            {
                {nameof(userCompany.Email), "Invalid credentials"}
            };

            var badAuthResult = new AuthResultViewModel(errors);
            var badResult = new ObjectResult(badAuthResult);
            return badResult;
        }

        public ObjectResult GetUserCompaniesByObjectiveId(
            string objectiveId, 
            CancellationToken cancellationToken = default)
        {
            var userCompaniesId = _unitOfWork.GetRepository<RequestModel>()
                .GetMany(m => m.ObjectiveId.Equals(objectiveId)).Select(m => m.CompanyId);

            var userCompanies = _unitOfWork.GetRepository<UserCompanyModel>()
                .GetMany(
                    userCompanyModel => !userCompanyModel.IsRemoved && userCompaniesId.Contains(userCompanyModel.CompanyId),
                    null,
                    TrackingState.Disabled,
                    "Offices.City.Region.Country")
                .ToList();
            var userCompaniesViewModels = _mapper.Map<List<UserCompanyViewModel>>(userCompanies);

            var result = new OkObjectResult(userCompaniesViewModels);
            return result;
        }
    }
}
