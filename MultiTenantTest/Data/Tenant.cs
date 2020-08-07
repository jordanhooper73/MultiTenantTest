using System;

namespace MultiTenantTest.MiddlewareSolution
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DbName { get; set; }
    }
}