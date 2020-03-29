using System.Collections.Generic;

namespace GoodsLogistics.Models.DTO.Location
{
    public class CountryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Language { get; set; }

        public List<RegionModel> Regions { get; set; }
    }
}
