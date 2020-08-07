using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.MiddlewareSolution;

namespace MultiTenantTest.Controllers
{
    [ApiController]
    [Route("api/{tenant}/[controller]")]
    public class TenantController : BaseController
    {
        public TenantController(IHttpContextAccessor httpContext, ITenantDbFactory dbFactory): base(httpContext, dbFactory) { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (Context == null)
                return BadRequest("Could not find Requested Tenant");

            TestObject[] objects = await Context.TestObjects.ToArrayAsync();

            return Ok(objects);
        }

        [HttpPost]
        public async Task<string> Post([FromBody] TestObject test)
        {
            test.Added = DateTime.UtcNow;
            test.ID = Guid.NewGuid();

            await Context.AddAsync(test);

            await Context.SaveChangesAsync();

            return $"Added '{test.Name}' to {Tenant.Name}'s db {Tenant.DbName}";
        }
    }
}
