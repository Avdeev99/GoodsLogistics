using System.Text;
using GoodsLogistics.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GoodsLogistics.Api.AppStart
{
    internal static class AuthConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var apiAuthOptions = configuration.GetSection(nameof(ApiAuthOptions)).Get<ApiAuthOptions>();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiAuthOptions.Secret)),
                        ValidIssuer = apiAuthOptions.Issuer,
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
