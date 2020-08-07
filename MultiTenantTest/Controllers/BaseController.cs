using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.MiddlewareSolution;

namespace MultiTenantTest.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Tenant Tenant { get; }

        protected TenantDbContext Context { get; }

        public BaseController(IHttpContextAccessor httpContext, ITenantDbFactory dbFactory)
        {
            Tenant = httpContext.HttpContext.GetTenant();

            if (Tenant != null)
                Context = dbFactory.GetTenantDatabase(Tenant.DbName);

        }
    }
}