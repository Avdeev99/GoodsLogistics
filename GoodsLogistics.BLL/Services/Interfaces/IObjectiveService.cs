using System.Threading;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Objective;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services.Interfaces
{
    public interface IObjectiveService
    {
        ObjectResult GetObjectives(CancellationToken cancellationToken = default);

        ObjectResult GetObjectiveById(
            string id,
            CancellationToken cancellationToken = default);

        ObjectResult CreateObjective(
            ObjectiveModel objective,
            CancellationToken cancellationToken = default);

        ObjectResult UpdateObjective(
            string id,
            ObjectiveUpdateRequestModel updateRequestModel,
            CancellationToken cancellationToken = default);

        ObjectResult DeleteObjective(
            string id,
            CancellationToken cancellationToken = default);
    }
}
