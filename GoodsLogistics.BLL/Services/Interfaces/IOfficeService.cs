using System.Threading;
using GoodsLogistics.Models.DTO.Office;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services.Interfaces
{
    public interface IOfficeService
    {
        ObjectResult GetOffices(CancellationToken cancellationToken = default);

        ObjectResult GetOfficeByKey(
            string key,
            CancellationToken cancellationToken = default);

        ObjectResult CreateOffice(
            OfficeModel office,
            CancellationToken cancellationToken = default);

        ObjectResult UpdateOffice(
            string key,
            OfficeUpdateRequestModel updateRequestModel,
            CancellationToken cancellationToken = default);

        ObjectResult DeleteOffice(
            string key,
            CancellationToken cancellationToken = default);

        ObjectResult GetOfficesByCompanyId(
            string id,
            CancellationToken cancellationToken = default);
    }
}
