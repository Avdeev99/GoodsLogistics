using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Objective;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GoodsLogistics.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ObjectiveController : ControllerBase
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IMapper _mapper;

        public ObjectiveController(IObjectiveService objectiveService, IMapper mapper)
        {
            _objectiveService = objectiveService;
            _mapper = mapper;
        }

        [HttpGet("objectives")]
        public IActionResult GetObjectives(CancellationToken cancellationToken = default)
        {
            var result = _objectiveService.GetObjectives(cancellationToken);
            return result;
        }

        [HttpGet("objectives/{id}")]
        public IActionResult GetObjectiveById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = _objectiveService.GetObjectiveById(id, cancellationToken);
            return result;
        }

        [HttpPost("objectives")]
        public IActionResult CreateObjective(
            [FromBody] ObjectiveViewModel createRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var createRequestModel = _mapper.Map<ObjectiveModel>(createRequestViewModel);
            var result = _objectiveService.CreateObjective(createRequestModel, cancellationToken);
            return result;
        }

        [HttpPatch("objectives/{id}")]
        public IActionResult UpdateObjective(
            [FromRoute] string id,
            [FromBody] ObjectiveUpdateRequestViewModel updateRequestViewModel,
            CancellationToken cancellationToken = default)
        {
            var updateRequestModel = _mapper.Map<ObjectiveUpdateRequestModel>(updateRequestViewModel);
            var result = _objectiveService.UpdateObjective(
                id,
                updateRequestModel,
                cancellationToken);
            return result;
        }

        [HttpDelete("objectives/{id}")]
        public IActionResult DeleteObjective(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var result = _objectiveService.DeleteObjective(id, cancellationToken);
            return result;
        }
    }
}