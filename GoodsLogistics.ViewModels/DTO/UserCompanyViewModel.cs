using System.Collections.Generic;
using GoodsLogistics.Models.DTO;

namespace GoodsLogistics.ViewModels.DTO
{
    public class UserCompanyViewModel
    {
        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<OfficeModel> Offices { get; set; }
    }
}
