using GoodsLogistics.Models.DTO.Enum;

namespace GoodsLogistics.Models.DTO.Objective
{
    public class ObjectiveFilteringModel
    {
        public SortingMethod SortingMethod { get; set; }

        public decimal? PriceFrom { get; set; }

        public decimal? PriceTo { get; set; }

        public int? CountryId { get; set; }

        public int? RegionId { get; set; }

        public int? CityId { get; set; }

        public string ReceiverCompanyEmail { get; set; }

        public string SenderCompanyEmail { get; set; }
    }
}
