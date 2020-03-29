using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.Models.DTO.Office;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GoodsLogistics.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;
        private readonly IMapper _mapper;

        public OfficeController(IOfficeService officeService, IMapper mapper)
        {
            _officeService = officeService;
            _mapper = mapper;
        }

        [HttpGet("offices")]
        public IActionResult GetOffices(CancellationToken cancellationToken = default)
        {
            var result = _officeService.GetOffices(cancellationToken);
            return result;
        }

        [HttpGet("offices/{key}")]
        public IActionResult GetOfficeByKey(
            [FromRoute] string key,
            CancellationToken cancellationToken = default)
        {
            var result = _officeService.GetOfficeByKey(key, cancellationToken);
            return result;
        }

        [HttpPost("offices")]
        public IActionResult CreateOffice(
            [FromBody] OfficeViewModel officeCreateRequestModel,
            CancellationToken cancellationToken = default)
        {
            var officeModel = _mapper.Map<OfficeModel>(officeCreateRequestModel);
            var result = _officeService.CreateOffice(officeModel, cancellationToken);
            return result;
        }

        [HttpPatch("offices/{key}")]
        public IActionResult UpdateOffice(
            [FromRoute] string key,
            [FromBody] OfficeUpdateRequestViewModel updateRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var updateRequestModel = _mapper.Map<OfficeUpdateRequestModel>(updateRequestViewModel);
            var result = _officeService.UpdateOffice(
                key,
                updateRequestModel,
                cancellationToken);
            return result;
        }

        [HttpDelete("offices/{key}")]
        public IActionResult DeleteOffice(
            [FromRoute] string key,
            CancellationToken cancellationToken = default)
        {
            var result = _officeService.DeleteOffice(key, cancellationToken);
            return result;
        }
    }
}