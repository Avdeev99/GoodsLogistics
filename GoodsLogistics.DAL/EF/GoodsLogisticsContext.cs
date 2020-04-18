using System;
using System.IO;
using System.Linq;
using CryptoHelper;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Location;
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
            SeedData();
        }

        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<RegionModel> Regions { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<UserCompanyModel> Companies { get; set; }
        public DbSet<OfficeModel> Offices { get; set; }
        public DbSet<ObjectiveModel> Objectives { get; set; }
        public DbSet<GoodModel> Goods { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<RuleModel> Rules { get; set; }
        public DbSet<RequestModel> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryModel>().HasKey(countryModel => countryModel.Id);
            modelBuilder.Entity<RegionModel>().HasKey(regionModel => regionModel.Id);
            modelBuilder.Entity<CityModel>().HasKey(cityModel => cityModel.Id);
            modelBuilder.Entity<OfficeModel>().HasKey(officeModel => officeModel.OfficeId);
            modelBuilder.Entity<ObjectiveModel>().HasKey(objectiveModel => objectiveModel.ObjectiveId);
            modelBuilder.Entity<UserCompanyModel>().HasKey(userCompanyModel => userCompanyModel.CompanyId);
            modelBuilder.Entity<GoodModel>().HasKey(goodModel => goodModel.Id);
            modelBuilder.Entity<LocationModel>().HasKey(locationModel => locationModel.Id);
            modelBuilder.Entity<RoleModel>().HasKey(roleModel => roleModel.RoleId);
            modelBuilder.Entity<RuleModel>().HasKey(ruleModel => ruleModel.RuleId);
            modelBuilder.Entity<RequestModel>().HasKey(requestModel => requestModel.RequestId);

            modelBuilder.Entity<OfficeModel>()
                .HasIndex(officeModel => officeModel.Key)
                .IsUnique();

            modelBuilder.Entity<CountryModel>()
                .Property(countryModel => countryModel.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RegionModel>()
                .Property(regionModel => regionModel.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CityModel>()
                .Property(cityModel => cityModel.Id)
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

            modelBuilder.Entity<LocationModel>()
                .Property(locationModel => locationModel.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GoodModel>()
                .Property(goodModel => goodModel.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RoleModel>()
                .Property(roleModel => roleModel.RoleId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RuleModel>()
                .Property(ruleModel => ruleModel.RuleId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RequestModel>()
                .Property(requestModel => requestModel.RequestId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { RoleId = "1", Name = "Admin" },
                new RoleModel { RoleId = "2", Name = "Customer" },
                new RoleModel { RoleId = "3", Name = "Provider" });

            modelBuilder.Entity<UserCompanyModel>().HasData(
                new UserCompanyModel
                {
                    CompanyId = Guid.NewGuid().ToString(),
                    Name = "Admin Company",
                    Email = "admin@gmail.com",
                    RoleId = "1",
                    PasswordHash = Crypto.HashPassword("qwe123")
                },
                new UserCompanyModel
                {
                    CompanyId = Guid.NewGuid().ToString(),
                    Name = "Customer Company",
                    Email = "customer@gmail.com",
                    RoleId = "2",
                    PasswordHash = Crypto.HashPassword("qwe123")
                },
                new UserCompanyModel
                {
                    CompanyId = Guid.NewGuid().ToString(),
                    Name = "Provider Company",
                    Email = "provider@gmail.com",
                    RoleId = "3",
                    PasswordHash = Crypto.HashPassword("qwe123")
                });
        }

        private void SeedData()
        {
            if (!Countries.Any())
            {
                var query = "InsertAllCountries";
                Database.ExecuteSqlRaw(query);
            }

            if (!Regions.Any())
            {
                var query = "InsertAllRegions";
                Database.ExecuteSqlRaw(query);
            }

            if (!Cities.Any())
            {
                var filePath1 = Path.Combine(Directory.GetParent(
                    Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 01.sql");
                var sql1 = File.ReadAllText(filePath1);
                Database.ExecuteSqlRaw(sql1);

                var filePath2 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 02.sql");
                var sql2 = File.ReadAllText(filePath2);
                Database.ExecuteSqlRaw(sql2);

                var filePath3 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 03.sql");
                var sql3 = File.ReadAllText(filePath3);
                Database.ExecuteSqlRaw(sql3);

                var filePath4 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 04.sql");
                var sql4 = File.ReadAllText(filePath4);
                Database.ExecuteSqlRaw(sql4);

                var filePath5 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 05.sql");
                var sql5 = File.ReadAllText(filePath5);
                Database.ExecuteSqlRaw(sql5);

                var filePath6 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 06.sql");
                var sql6 = File.ReadAllText(filePath6);
                Database.ExecuteSqlRaw(sql6);

                var filePath7 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 07.sql");
                var sql7 = File.ReadAllText(filePath7);
                Database.ExecuteSqlRaw(sql7);

                var filePath8 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 08.sql");
                var sql8 = File.ReadAllText(filePath8);
                Database.ExecuteSqlRaw(sql8);

                var filePath9 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 09.sql");
                var sql9 = File.ReadAllText(filePath9);
                Database.ExecuteSqlRaw(sql9);

                var filePath10 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 10.sql");
                var sql10 = File.ReadAllText(filePath10);
                Database.ExecuteSqlRaw(sql10);

                var filePath11 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 11.sql");
                var sql11 = File.ReadAllText(filePath11);
                Database.ExecuteSqlRaw(sql11);

                var filePath12 = Path.Combine(Directory.GetParent(
                        Directory.GetCurrentDirectory()).FullName,
                    @"GoodsLogistics.Api\wwwroot\sql-scripts\04 - Insert Cities 12.sql");
                var sql12 = File.ReadAllText(filePath12);
                Database.ExecuteSqlRaw(sql12);
            }
        }
    }
}
