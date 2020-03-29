using System.Collections.Generic;

namespace GoodsLogistics.ViewModels.DTO
{
    public class AuthResultViewModel
    {
        public AuthResultViewModel(string jwtToken, UserCompanyViewModel userCompany)
        {
            JwtToken = jwtToken;
            UserCompany = userCompany;
            IsAuthorized = true;
        }

        public AuthResultViewModel(Dictionary<string, string> errors)
        {
            Errors = errors;
            IsAuthorized = false;
        }

        public string JwtToken { get; }

        public UserCompanyViewModel UserCompany { get; }

        public Dictionary<string, string> Errors { get; }

        public bool IsAuthorized { get; }
    }
}
