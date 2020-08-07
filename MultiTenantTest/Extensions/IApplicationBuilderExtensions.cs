using Microsoft.AspNetCore.Builder;

namespace MultiTenantTest.MiddlewareSolution
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder) => builder.UseMiddleware<TenantMiddleware<Tenant>>();
    }
}
