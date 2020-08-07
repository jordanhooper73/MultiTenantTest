using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        public string GetTenantID(string request)
        {
            string[] parts = request.Split('/', System.StringSplitOptions.RemoveEmptyEntries);

            return parts[1];
        }
    }
}
