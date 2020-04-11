using AutoMapper;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Objective;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class ObjectiveProfile : Profile
    {
        public ObjectiveProfile()
        {
            CreateMap<ObjectiveModel, ObjectiveViewModel>().ReverseMap();

            CreateMap<ObjectiveUpdateRequestModel, ObjectiveUpdateRequestViewModel>().ReverseMap();

            CreateMap<ObjectiveFilteringModel, ObjectiveFilteringViewModel>().ReverseMap();
        }
    }
}
