using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Models.DB;
using Microsoft.Extensions.Logging;
using Dotnet_Core_Scaffolding_Oracle.Models;

namespace DataAccess
{
    public class DBAccess : ModelContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")).Build();

            optionsBuilder.UseOracle(configuration.GetConnectionString("OracleConnecionString"), options => options.UseOracleSQLCompatibility("11"));
        }
    }
}
