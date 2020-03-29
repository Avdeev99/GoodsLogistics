using System.Collections.Generic;
using GoodsLogistics.Models.DTO.Office;

namespace GoodsLogistics.Models.DTO.Location
{
    public class CityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public RegionModel Region { get; set; }

        public List<OfficeModel> Offices { get; set; }
    }
}
