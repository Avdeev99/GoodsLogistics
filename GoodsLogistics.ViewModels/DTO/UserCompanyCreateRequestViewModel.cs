using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.ViewModels.DTO
{
    public class UserCompanyCreateRequestViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRoles Role { get; set; }
    }
}
