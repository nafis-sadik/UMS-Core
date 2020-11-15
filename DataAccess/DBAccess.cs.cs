using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Models.DB;

namespace DataAccess
{
    public class DBAccess: DbContext
    {
        public DbSet<UMS_AUTHQUE> UMS_AUTHQUE { get; set; }
        public DbSet<UMS_APPCONFIG> UMS_APPCONFIG { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")).Build();

            optionsBuilder.UseOracle(configuration.GetConnectionString("OracleConnecionString"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UMS_AUTHQUE>();
            builder.Entity<UMS_APPCONFIG>();

            base.OnModelCreating(builder);
        }
    }
}
