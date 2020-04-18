using System.Threading;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services.Interfaces
{
    public interface IRequestService
    {
        ObjectResult GetRequests(CancellationToken cancellationToken = default);

        ObjectResult GetRequestById(
            string id,
            CancellationToken cancellationToken = default);

        ObjectResult CreateRequest(
            RequestModel createRequestModel,
            CancellationToken cancellationToken = default);

        ObjectResult UpdateRequest(
            string id,
            RequestUpdateModel updateRequestModel,
            CancellationToken cancellationToken = default);

        ObjectResult DeleteRequest(
            string id,
            CancellationToken cancellationToken = default);

        ObjectResult GetRequestsByCompanyId(
            string id,
            CancellationToken cancellationToken = default);
    }
}
