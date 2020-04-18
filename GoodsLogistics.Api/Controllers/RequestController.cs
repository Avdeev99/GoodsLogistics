using System.Threading;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Request;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public RequestController(
            IMapper mapper, 
            IRequestService requestService)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        [HttpGet("requests")]
        public IActionResult GetRequests(CancellationToken cancellationToken = default)
        {
            var result = _requestService.GetRequests(cancellationToken);
            return result;
        }

        [HttpGet("requests/{id}")]
        public IActionResult GetRequestById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = _requestService.GetRequestById(id, cancellationToken);
            return result;
        }

        [HttpPost("requests")]
        public IActionResult CreateOffice(
            [FromBody] RequestViewModel requestViewModel,
            CancellationToken cancellationToken = default)
        {
            var requestModel = _mapper.Map<RequestModel>(requestViewModel);
            var result = _requestService.CreateRequest(requestModel, cancellationToken);
            return result;
        }

        [HttpPatch("requests/{id}")]
        public IActionResult UpdateRequest(
            [FromRoute] string id,
            [FromBody] RequestUpdateViewModel updateRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var updateRequestModel = _mapper.Map<RequestUpdateModel>(updateRequestViewModel);
            var result = _requestService.UpdateRequest(
                id,
                updateRequestModel,
                cancellationToken);
            return result;
        }

        [HttpDelete("requests/{id}")]
        public IActionResult DeleteRequest(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = _requestService.DeleteRequest(id, cancellationToken);
            return result;
        }

        [HttpGet("company/{id}/requests")]
        public IActionResult GetOfficesByCompanyEmail(
            string id,
            CancellationToken cancellationToken = default)
        {
            var result = _requestService.GetRequestsByCompanyId(
                id,
                cancellationToken);
            return result;
        }
    }
}