using System.Collections.Generic;
using GoodsLogistics.Models.DTO.Office;

namespace GoodsLogistics.Models.DTO.UserCompany
{
    public class UserCompanyUpdateRequestModel
    {
        public string Name { get; set; }

        public List<OfficeModel> Offices { get; set; }
    }
}
