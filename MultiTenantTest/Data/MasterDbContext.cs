using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantTest.MiddlewareSolution
{
    public class MasterDbContext : DbContext, ITenantStore
    {
        public DbSet<Tenant> Tenants { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }

        public Task<Tenant> GetTenantAsync(string identifier) => Tenants.FirstOrDefaultAsync(x => x.Name == identifier);
    }
}
