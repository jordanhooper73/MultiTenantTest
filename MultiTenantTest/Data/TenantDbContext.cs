using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MultiTenantTest
{
    public class TenantDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<TestObject> TestObjects { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

    public class TestObject
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public DateTime Added { get; set; }
    }
}
