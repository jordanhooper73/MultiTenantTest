using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiTenantTest.MiddlewareSolution;

namespace MultiTenantTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEntityFrameworkNpgsql().AddDbContext<MasterDbContext>(o => o.UseNpgsql(Configuration.GetConnectionString("MasterDatabase")));

            services.AddMultiTenancy()
                .WithResolutionStrategy<PathResolutionStrategy>()
                .WithStore<MasterDbContext>();

            ConfigureDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMultiTenancy();

            EnsureMigrated(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureDependencies(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services.AddTransient<ITenantDbFactory, TenantDbFactory>();
        }

        private void EnsureMigrated(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                DbContextOptions<MasterDbContext> masterDbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<MasterDbContext>>();

                using (var context = new MasterDbContext(masterDbContextOptions))
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
