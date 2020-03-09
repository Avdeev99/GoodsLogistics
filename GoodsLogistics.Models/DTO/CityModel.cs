using System.Collections.Generic;
using GoodsLogistics.Models.DTO.Office;

namespace GoodsLogistics.Models.DTO
{
    public class CityModel
    {
        public string CityId { get; set; }

        public string Name { get; set; }

        public string CountryId { get; set; }

        public CountryModel Country { get; set; }

        public List<OfficeModel> Offices { get; set; }
    }
}
