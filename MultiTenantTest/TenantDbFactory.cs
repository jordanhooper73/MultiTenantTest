using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiTenantTest
{
    public interface ITenantDbFactory
    {
        TenantDbContext GetTenantDatabase(string databaseName);
    }

    public class TenantDbFactory : ITenantDbFactory
    {
        private readonly IConfiguration _configuration;

        public TenantDbFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TenantDbContext GetTenantDatabase(string databaseName)
        {
            string tenantConnection = $"{_configuration.GetConnectionString("TenantDatabasePre")}Database={databaseName};{_configuration.GetConnectionString("TenantDatabasePost")}";

            var context = new TenantDbContext(new DbContextOptions<TenantDbContext>(), tenantConnection);

            context.Database.Migrate();

            return context;
        }
    }
}
