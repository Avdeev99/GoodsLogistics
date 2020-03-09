using AutoMapper;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class UserCompanyProfile : Profile
    {
        public UserCompanyProfile()
        {
            CreateMap<UserCompanyModel, UserCompanyViewModel>().ReverseMap();
        }
    }
}
