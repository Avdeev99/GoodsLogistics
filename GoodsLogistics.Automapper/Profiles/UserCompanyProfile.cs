using AutoMapper;
using CryptoHelper;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class UserCompanyProfile : Profile
    {
        public UserCompanyProfile()
        {
            CreateMap<UserCompanyModel, UserCompanyViewModel>();

            CreateMap<UserCompanyViewModel, UserCompanyModel>()
                .ForMember(
                    dest => dest.PasswordHash, 
                    m => m.MapFrom(s => GetPasswordHash(s.Password)));

            CreateMap<UserCompanyUpdateRequestModel, UserCompanyUpdateRequestViewModel>().ReverseMap();

            CreateMap<UserCompanyLoginRequestModel, UserCompanyLoginRequestViewModel>();

            CreateMap<UserCompanyLoginRequestViewModel, UserCompanyLoginRequestModel>();

            CreateMap<UserCompanyCreateRequestViewModel, UserCompanyCreateRequestModel>().ReverseMap();

            CreateMap<UserCompanyCreateRequestModel, UserCompanyModel>()
                .ForMember(
                    dest => dest.PasswordHash,
                    m => m.MapFrom(s => GetPasswordHash(s.Password)))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
        }

        private string GetPasswordHash(string password)
        {
            var result = Crypto.HashPassword(password);
            return result;
        }
    }
}
