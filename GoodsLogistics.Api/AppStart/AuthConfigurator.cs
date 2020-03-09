using Microsoft.AspNetCore.Builder;

namespace GoodsLogistics.Api.AppStart
{
    internal static class AuthConfigurator
    {
        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
