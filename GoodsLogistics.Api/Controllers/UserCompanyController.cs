using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.Api.Controllers
{
    [ApiController]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService _userCompanyService;
        private readonly IMapper _mapper;

        public UserCompanyController(
            IUserCompanyService userCompanyService, 
            IMapper mapper)
        {
            _userCompanyService = userCompanyService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult CreateUserCompany(
            [FromBody] UserCompanyViewModel createRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var createRequestModel = _mapper.Map<UserCompanyModel>(createRequestViewModel);
            var result = _userCompanyService.RegisterUserCompany(createRequestModel, cancellationToken);
            return result;
        }

        [HttpPost("login")]
        public IActionResult LoginUserCompany(
            [FromBody] UserCompanyLoginRequestViewModel loginRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var loginRequestModel = _mapper.Map<UserCompanyLoginRequestModel>(loginRequestViewModel);
            var result = _userCompanyService.LoginUserCompany(loginRequestModel, cancellationToken);
            return result;
        }

        [HttpPatch("users/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateUserCompany(
            [FromRoute] string email,
            [FromBody] UserCompanyUpdateRequestViewModel updateRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var updateRequestModel = _mapper.Map<UserCompanyUpdateRequestModel>(updateRequestViewModel);
            var result = _userCompanyService.UpdateUserCompany(
                email,
                updateRequestModel,
                cancellationToken);
            return result;
        }

        [HttpDelete("users/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteUserCompany(
            [FromRoute] string email,
            CancellationToken cancellationToken = default)
        {
            var result = _userCompanyService.DeleteUserCompany(email, cancellationToken);
            return result;
        }
    }
}