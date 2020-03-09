using System.Collections.Generic;

namespace GoodsLogistics.Models.DTO
{
    public class UserCompanyModel
    {
        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<OfficeModel> Offices { get; set; }

        public bool IsRemoved { get; set; }
    }
}
