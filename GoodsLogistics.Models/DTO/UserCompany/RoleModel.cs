using System.Collections.Generic;

namespace GoodsLogistics.Models.DTO.UserCompany
{
    public class RoleModel
    {
        public string RoleId { get; set; }

        public string Name { get; set; }

        public List<UserCompanyModel> Users { get; set; }
    }
}
