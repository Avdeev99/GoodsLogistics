using System;
using AutoMapper;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Request;
using GoodsLogistics.ViewModels.DTO;

namespace GoodsLogistics.Automapper.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<RequestModel, RequestViewModel>();
            CreateMap<RequestUpdateModel, RequestUpdateViewModel>();
        }
    }
}
