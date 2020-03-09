using System.Collections.Generic;
using System.Security.Claims;
using GoodsLogistics.Auth.Factories.Interfaces;
using GoodsLogistics.Auth.Tokens.Interfaces;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.Auth.Tokens
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ITokenFactory _tokenFactory;

        public TokenProvider(ITokenFactory tokenFactory)
        {
            _tokenFactory = tokenFactory;
        }

        public string GenerateTokenForUser(UserCompanyModel userCompany)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userCompany.CompanyId),
                new Claim(ClaimTypes.Name, userCompany.Name),
                new Claim(ClaimTypes.Email, userCompany.Email)
            };

            //if (user.Role != null) claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            return _tokenFactory.Create(claims);
        }
    }
}
