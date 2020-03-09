using System.Collections.Generic;

namespace GoodsLogistics.Models.DTO
{
    public class CountryModel
    {
        public string CountryId { get; set; }

        public string Name { get; set; }

        public List<CityModel> Cities { get; set; }
    }
}
