namespace GoodsLogistics.Models.DTO.Location
{
    public class RegionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public CountryModel Country { get; set; }
    }
}
