using GoodsLogistics.BLL.Services;
using GoodsLogistics.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI.Projects
{
    internal static class BllDiConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IUserCompanyService, UserCompanyService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IObjectiveService, ObjectiveService>();
        }
    }
}
