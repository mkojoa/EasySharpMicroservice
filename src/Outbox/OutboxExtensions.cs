using EasySharp.Core.Helpers;
using EasySharp.EfCore.Option;
using EasySharp.Outbox.Stores.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasySharp.Outbox
{
    public static class OutboxExtensions
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddOutbox(this IServiceCollection services)
        {
            Configuration = EasySharpServices.Builder();

            var options = new OutboxOptions();
            Configuration.GetSection(nameof(OutboxOptions)).Bind(options);
            services.Configure<OutboxOptions>(Configuration.GetSection(nameof(OutboxOptions)));

            var dbContextOptions = new EfCoreOptions();
            Configuration.GetSection(nameof(EfCoreOptions)).Bind(dbContextOptions);

            if (options.Enable == false)
            {
                return services;
            }

            switch (options.OutboxType.ToLowerInvariant())
            {
                case "efcore":
                case "ef":
                    services.AddEfCoreOutboxStore(opts =>
                        opts.UseSqlServer(
                            dbContextOptions.ConnectionString
                        ));
                    break;
                default:
                    throw new Exception($"Outbox type '{options.OutboxType}' is not supported");
            }

            services.AddScoped<IOutboxListener, OutboxListener>();
            services.AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
