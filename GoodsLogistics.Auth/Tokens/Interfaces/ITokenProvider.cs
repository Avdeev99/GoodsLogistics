using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.Auth.Tokens.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateTokenForUser(UserCompanyModel userCompany);
    }
}
