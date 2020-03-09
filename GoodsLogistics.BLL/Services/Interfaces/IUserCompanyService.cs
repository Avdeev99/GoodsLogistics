using System.Threading;
using GoodsLogistics.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services.Interfaces
{
    public interface IUserCompanyService
    {
        ObjectResult GetUserCompanies(CancellationToken cancellationToken = default);

        ObjectResult GetUserCompanyByEmail(
            string email,
            CancellationToken cancellationToken = default);

        ObjectResult CreateUser(
            UserCompanyModel user,
            CancellationToken cancellationToken = default);

        ObjectResult UpdateUserCompany(
            string email,
            CancellationToken cancellationToken = default);

        ObjectResult DeleteUserCompany(
            string email,
            CancellationToken cancellationToken = default);
    }
}
