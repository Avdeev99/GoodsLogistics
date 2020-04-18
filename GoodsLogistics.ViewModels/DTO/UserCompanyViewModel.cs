using System.Collections.Generic;
using GoodsLogistics.Models.DTO.Office;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.ViewModels.DTO
{
    public class UserCompanyViewModel
    {
        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<OfficeModel> Offices { get; set; }

        public string RoleId { get; set; }

        public RoleModel Role { get; set; }
    }
}
