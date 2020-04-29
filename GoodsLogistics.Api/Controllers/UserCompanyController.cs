using System;
using System.IO;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using GoodsLogistics.DAL;
using Microsoft.AspNetCore.Hosting;

namespace GoodsLogistics.Api.Controllers
{
    [ApiController]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService _userCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public UserCompanyController(
            IUserCompanyService userCompanyService,
            IMapper mapper, 
            IHostingEnvironment hostingEnvironment)
        {
            _userCompanyService = userCompanyService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("register")]
        public IActionResult CreateUserCompany(
            [FromBody] UserCompanyCreateRequestViewModel createRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var createRequestModel = _mapper.Map<UserCompanyCreateRequestModel>(createRequestViewModel);
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


        [HttpGet("users/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserCompany(
            [FromRoute] string email,
            CancellationToken cancellationToken = default)
        {
            var result = _userCompanyService.GetUserCompanyByEmail(
                email,
                cancellationToken);
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

        [HttpGet("/home")]
        public IActionResult Home()
        {
            return Ok("Hello");
        }

        [HttpGet("users/requests/objectives/{objectiveId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUserCompaniesByObjectiveId(
            [FromRoute] string objectiveId,
            CancellationToken cancellationToken = default)
        {
            var result = _userCompanyService.GetUserCompaniesByObjectiveId(
                objectiveId,
                cancellationToken);
            return result;
        }

        [HttpGet("database/backUp")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DatabaseBackUp(CancellationToken cancellationToken = default)
        {
            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "backups");
            var fullPath = Path.Combine(folderPath, $"backUp-{DateTime.UtcNow.ToString("yyyy_MM_dd_hh_mm_ss")}.bak");
            DatabaseFunctions.Backup("GoodsLogisticsDb", fullPath);
            return Ok();
        }
    }
}