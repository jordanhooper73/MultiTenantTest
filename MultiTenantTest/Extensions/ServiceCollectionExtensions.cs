using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantTest.MiddlewareSolution
{
    public static class ServiceCollectionExtensions
    {
        public static TenantBuilder AddMultiTenancy(this IServiceCollection services) => new TenantBuilder(services);
    }
}
