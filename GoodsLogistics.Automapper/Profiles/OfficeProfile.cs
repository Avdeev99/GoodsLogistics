using AutoMapper;
using GoodsLogistics.Models.DTO.Office;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<OfficeModel, OfficeViewModel>().ReverseMap();
            CreateMap<OfficeUpdateRequestModel, OfficeUpdateRequestViewModel>().ReverseMap();
        }
    }
}
