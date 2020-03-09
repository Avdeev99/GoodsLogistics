using GoodsLogistics.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI.Projects
{
    internal static class ApiDiConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiAuthOptions>(configuration.GetSection(nameof(ApiAuthOptions)));
        }
    }
}
