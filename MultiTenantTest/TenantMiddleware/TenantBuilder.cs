using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantTest.MiddlewareSolution
{
    public class TenantBuilder
    {
        private readonly IServiceCollection _services;

        public TenantBuilder(IServiceCollection services)
        {
            services.AddScoped<TenantAccessService>();
            _services = services;
        }

        public TenantBuilder WithResolutionStrategy<T>(ServiceLifetime lifetime = ServiceLifetime.Transient) where T : class, ITenantResolutionStrategy
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(T), lifetime));
            return this;
        }

        public TenantBuilder WithStore<T>(ServiceLifetime lifetime = ServiceLifetime.Transient) where T : class, ITenantStore
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore), typeof(T), lifetime));
            return this;
        }
    }
}
