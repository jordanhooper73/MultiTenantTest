using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MultiTenantTest
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        // Required to allow creation of migrations of dbContexts with nonparameterless constructors
        public TenantDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TenantDbContext>();

            string tenantConnection = $"{configuration.GetConnectionString("TenantDatabasePre")}Database=localTestDb;{configuration.GetConnectionString("TenantDatabasePost")}";

            return new TenantDbContext(new DbContextOptions<TenantDbContext>(), tenantConnection);
        }
    }
}
