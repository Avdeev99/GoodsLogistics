using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.Models.DTO.UserCompany
{
    public class UserCompanyCreateRequestModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRoles Role { get; set; }
    }
}
