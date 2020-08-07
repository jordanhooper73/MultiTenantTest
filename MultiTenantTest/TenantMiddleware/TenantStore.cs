using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantTest.MiddlewareSolution
{
    public interface ITenantStore
    {
        Task<Tenant> GetTenantAsync(string identifier);
    }

    public class InMemoryTenantStore : ITenantStore
    {
        // In memory version of a master db service
        public async Task<Tenant> GetTenantAsync(string identifier)
        {
            Tenant tenant = new[]
            {
                new Tenant{ Id = Guid.NewGuid(), Name = "Test", DbName = "TestDb" },
                new Tenant{ Id = Guid.NewGuid(), Name = "client1", DbName = "Client1Db" },
                new Tenant{ Id = Guid.NewGuid(), Name = "client2", DbName = "Client2Db" }
            }.SingleOrDefault(t => t.Name == identifier.ToLowerInvariant());

            return await Task.FromResult(tenant);
        }
    }

    public class MasterDbContext : DbContext, ITenantStore
    {
        public DbSet<Tenant> Tenants { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }

        public Task<Tenant> GetTenantAsync(string identifier) => Tenants.FirstOrDefaultAsync(x => x.Name == identifier);
    }
}
