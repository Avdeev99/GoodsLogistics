using GoodsLogistics.Auth.Factories;
using GoodsLogistics.Auth.Factories.Interfaces;
using GoodsLogistics.Auth.Tokens;
using GoodsLogistics.Auth.Tokens.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI.Projects
{
    internal static class AuthDiConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ITokenFactory, JwtTokenFactory>();
            services.AddScoped<ITokenProvider, TokenProvider>();
        }
    }
}
