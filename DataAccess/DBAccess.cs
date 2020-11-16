using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Models.DB;

namespace DataAccess
{
    public class DBAccess : DbContext
    {
        public DbSet<UMS_AUTHQUE> UMS_AUTHQUE { get; set; }
        //public DbSet<UMS_APPCONFIG> UMS_APPCONFIG { get; set; }
        public DbSet<UMS_PASS> UMS_PASS { get; set; }
        public DbSet<UMS_USERINFO> UMS_USERINFO { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")).Build();

            optionsBuilder.UseOracle(configuration.GetConnectionString("OracleConnecionString"), options => options.UseOracleSQLCompatibility("11"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UMS_AUTHQUE>();
            //builder.Entity<UMS_APPCONFIG>();
            builder.Entity<UMS_PASS>();
            builder.Entity<UMS_USERINFO>();

            base.OnModelCreating(builder);
        }
    }
}
