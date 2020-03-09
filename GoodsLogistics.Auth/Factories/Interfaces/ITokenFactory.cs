using System.Collections.Generic;
using System.Security.Claims;

namespace GoodsLogistics.Auth.Factories.Interfaces
{
    public interface ITokenFactory
    {
        string Create(List<Claim> claims);
    }
}
