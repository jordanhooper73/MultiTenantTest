using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace MultiTenantTest.MiddlewareSolution
{
    public interface ITenantResolutionStrategy
    {
        Task<string> GetTenantIdentifierAsync();
    }

    public class HostResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HostResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTenantIdentifierAsync()
        {
            return await Task.FromResult(_httpContextAccessor.HttpContext.Request.Host.Host);
        }
    }

    public class PathResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PathResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTenantIdentifierAsync()
        {
            return await Task.FromResult(GetTenantID(_httpContextAccessor.HttpContext.Request.Path));
        }

        // todo: for demo purposes using the tenantName but adjust this to use the ID of the tenant instance
        public string GetTenantID(string request)
        {
            string[] parts = request.Split('/', System.StringSplitOptions.RemoveEmptyEntries);

            int index = parts.IndexOf("api") + 1;

            // return Guid.Parse(parts[index]);
            return parts[index];
        }
    }
}
