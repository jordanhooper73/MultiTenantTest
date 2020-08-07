using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MultiTenantTest
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<TestDbContext>();

            string tenantConnection = $"{configuration.GetConnectionString("TenantDatabasePre")}Database=localTestDb;{configuration.GetConnectionString("TenantDatabasePost")}";

            return new TestDbContext(new DbContextOptions<TestDbContext>(), tenantConnection);
        }
    }
}
