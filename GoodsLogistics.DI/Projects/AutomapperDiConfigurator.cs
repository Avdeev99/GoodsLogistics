using System;
using AutoMapper;
using GoodsLogistics.Automapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI.Projects
{
    internal static class AutomapperDiConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            var automapperProfiles = new Type[]
            {
                typeof(UserCompanyProfile),
                typeof(OfficeProfile),
                typeof(ObjectiveProfile),
                typeof(RequestProfile)
            };

            services.AddAutoMapper(automapperProfiles);
        }
    }
}
