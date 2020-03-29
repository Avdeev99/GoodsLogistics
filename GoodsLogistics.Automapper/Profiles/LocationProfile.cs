using AutoMapper;
using GoodsLogistics.Models.DTO.Location;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<CountryModel, CountryViewModel>().ReverseMap();

            CreateMap<RegionModel, RegionViewModel>().ReverseMap();

            CreateMap<CityModel, CityViewModel>().ReverseMap();
        }
    }
}
