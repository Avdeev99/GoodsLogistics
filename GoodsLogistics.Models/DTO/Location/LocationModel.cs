namespace GoodsLogistics.Models.DTO.Location
{
    public class LocationModel
    {
        public string Id { get; set; }

        public int CityId { get; set; }

        public CityModel City { get; set; }

        public string Address { get; set; }
    }
}
