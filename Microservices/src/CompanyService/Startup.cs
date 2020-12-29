using CompanyService.Domain;
using CompanyService.Domain.Events;
using EasySharp.Cache;
using EasySharp.Consul;
using EasySharp.Core;
using EasySharp.Core.Cors;
using EasySharp.EfCore;
using EasySharp.EventStores.Stores.EfCore;
using EasySharp.MessageBrokers;
using EasySharp.Outbox;
using EasySharp.Pagination;
using EasySharp.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CompanyService
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
            services
               .AddEasySharp(typeof(Startup), typeof(CompanyDomainContext))
               .AddEfCore<CompanyDomainContext>()
               .AddDocs()
               .AddConsul(Configuration)
               .AddCorsOption()
               .AddCacheable()
               .AddApiPagination()
               .AddMessageBroker()
               .AddOutbox();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // UpdateDatabase(app);

            app
               .UseEasySharp()
               .UseDocs()
               .UseConsul(lifetime)
               .UseSubscribeEvent<CompanyCreatedEvent>();
        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EfCoreEventStoreContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}