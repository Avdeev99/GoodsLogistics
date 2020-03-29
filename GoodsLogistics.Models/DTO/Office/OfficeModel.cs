using GoodsLogistics.Models.DTO.Location;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.Models.DTO.Office
{
    public class OfficeModel
    {
        public string OfficeId { get; set; }

        public string Key { get; set; }

        public string Address { get; set; }

        public int CityId { get; set; }

        public CityModel City { get; set; }

        public string CompanyId { get; set; }

        public UserCompanyModel Company { get; set; }
    }
}
