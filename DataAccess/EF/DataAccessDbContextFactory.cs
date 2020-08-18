using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataAccess.EF
{
    public class DataAccessDbContextFactory : IDesignTimeDbContextFactory<DataAccessDbContext>
    {
        public DataAccessDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();
            var connString = config.GetConnectionString("ConnectionString");
            var optionsBuilder = new DbContextOptionsBuilder<DataAccessDbContext>();
            optionsBuilder.UseSqlServer(connString);

            return new DataAccessDbContext(optionsBuilder.Options);
        }
    }
}
