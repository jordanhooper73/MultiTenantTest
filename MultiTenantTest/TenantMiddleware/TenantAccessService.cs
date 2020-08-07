using System.Threading.Tasks;

namespace MultiTenantTest.MiddlewareSolution
{
    public class TenantAccessService
    {
        private readonly ITenantResolutionStrategy _tenantResolutionStrategy;
        private readonly ITenantStore _tenantStore;

        public TenantAccessService(ITenantResolutionStrategy tenantResolutionStrategy, ITenantStore tenantStore)
        {
            _tenantResolutionStrategy = tenantResolutionStrategy;
            _tenantStore = tenantStore;
        }

        public async Task<Tenant> GetTenantAsync()
        {
            string tenantIdentifier = await _tenantResolutionStrategy.GetTenantIdentifierAsync();

            return await _tenantStore.GetTenantAsync(tenantIdentifier);
        }
    }
}