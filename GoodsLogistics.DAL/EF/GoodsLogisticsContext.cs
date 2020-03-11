using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Office;
using GoodsLogistics.Models.DTO.UserCompany;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryModel>().HasKey(countryModel => countryModel.CountryId);
            modelBuilder.Entity<CityModel>().HasKey(cityModel => cityModel.CityId);
            modelBuilder.Entity<OfficeModel>().HasKey(officeModel => officeModel.OfficeId);
            modelBuilder.Entity<ObjectiveModel>().HasKey(objectiveModel => objectiveModel.ObjectiveId);
            modelBuilder.Entity<UserCompanyModel>().HasKey(userCompanyModel => userCompanyModel.CompanyId);

            modelBuilder.Entity<OfficeModel>()
                .HasIndex(officeModel => officeModel.Key)
                .IsUnique();

            modelBuilder.Entity<CountryModel>()
                .Property(countryModel => countryModel.CountryId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CityModel>()
                .Property(cityModel => cityModel.CityId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OfficeModel>()
                .Property(officeModel => officeModel.OfficeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ObjectiveModel>()
                .Property(objectiveModel => objectiveModel.ObjectiveId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserCompanyModel>()
                .Property(userCompanyModel => userCompanyModel.CompanyId)
                .ValueGeneratedOnAdd();
        }
    }
}
