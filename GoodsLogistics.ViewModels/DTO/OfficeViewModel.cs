using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Location;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.ViewModels.DTO
{
    public class OfficeViewModel
    {
        public string OfficeId { get; set; }

        public string Key { get; set; }

        public string Address { get; set; }

        public string CityId { get; set; }

        public CityModel City { get; set; }

        public string CompanyId { get; set; }

        public UserCompanyModel Company { get; set; }
    }
}
