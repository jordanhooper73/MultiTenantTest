using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MultiTenantTest.MiddlewareSolution
{
    internal class TenantMiddleware<T> where T : Tenant
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Works out the Tenant and adds them to the HttpContext
        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(Constants.HttpContextTenantKey))
            {
                var tenantService = context.RequestServices.GetService(typeof(TenantAccessService)) as TenantAccessService;

                Tenant tenant = await tenantService.GetTenantAsync();

                if (tenant != null)
                    context.Items.Add(Constants.HttpContextTenantKey, tenant);
            }

            if (_next != null)
                await _next(context);
        }
    }

    public static class Constants
    {
        public static string HttpContextTenantKey => "Tenants";
    }
}
