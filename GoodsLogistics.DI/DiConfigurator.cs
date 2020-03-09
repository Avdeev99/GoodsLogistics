using GoodsLogistics.DI.Projects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI
{
    public static class DiConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            DalDiConfigurator.Configure(services, configuration);

            AutomapperDiConfigurator.Configure(services);
        }
    }
}
