using Microsoft.AspNetCore.Http;

namespace MultiTenantTest.MiddlewareSolution
{
    public static class HttpContextExtensions
    {
        public static Tenant GetTenant(this HttpContext context)
        {
            return context.GetTenant<Tenant>();
        }

        // Tenant added to context by middleware - this pulls it out
        public static T GetTenant<T>(this HttpContext context) where T : Tenant
        {
            return context.Items.ContainsKey(Constants.HttpContextTenantKey) ?
                context.Items[Constants.HttpContextTenantKey] as T :
                null;
        }
    }
}
