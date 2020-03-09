using GoodsLogistics.DAL.EF;
using GoodsLogistics.DAL.Factories;
using GoodsLogistics.DAL.Factories.Interfaces;
using GoodsLogistics.DAL.Options;
using GoodsLogistics.DAL.Repositories;
using GoodsLogistics.DAL.Repositories.Interfaces;
using GoodsLogistics.DAL.UOF;
using GoodsLogistics.DAL.UOF.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DI.Projects
{
    internal static class DalDiConfigurator
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlServerDbOptions>(configuration.GetSection(nameof(SqlServerDbOptions)));
            var sqlServerDbOptions = configuration.GetSection(nameof(SqlServerDbOptions)).Get<SqlServerDbOptions>();

            services.AddDbContext<GoodsLogisticsContext>(options => options.UseSqlServer(sqlServerDbOptions.ConnectionString));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
        }
    }
}
