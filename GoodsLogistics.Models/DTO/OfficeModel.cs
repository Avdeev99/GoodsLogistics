namespace GoodsLogistics.Models.DTO
{
    public class OfficeModel
    {
        public string OfficeId { get; set; }

        public string Address { get; set; }

        public string CityId { get; set; }

        public CityModel City { get; set; }

        public string CompanyId { get; set; }

        public UserCompanyModel Company { get; set; }
    }
}
