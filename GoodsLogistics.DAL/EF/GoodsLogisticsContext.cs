using GoodsLogistics.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GoodsLogistics.DAL.EF
{
    public sealed class GoodsLogisticsContext : DbContext
    {
        public GoodsLogisticsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<UserCompanyModel> Companies { get; set; }
        public DbSet<OfficeModel> Offices { get; set; }
        public DbSet<ObjectiveModel> Objectives { get; set; }
    }
}
