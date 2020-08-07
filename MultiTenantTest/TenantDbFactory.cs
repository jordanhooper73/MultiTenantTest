using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiTenantTest
{
    public interface ITenantDbFactory
    {
        TestDbContext GetTenantDatabase(string databaseName);
    }

    public class TenantDbFactory : ITenantDbFactory
    {
        private readonly IConfiguration _configuration;

        public TenantDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TestDbContext GetTenantDatabase(string databaseName)
        {
            // get TenantOptions
            string tenantConnection = $"{_configuration.GetConnectionString("TenantDatabasePre")}Database={databaseName};{_configuration.GetConnectionString("TenantDatabasePost")}";

            var context = new TestDbContext(new DbContextOptions<TestDbContext>(), tenantConnection);

            context.Database.Migrate();

            return context;
        }
    }
}
