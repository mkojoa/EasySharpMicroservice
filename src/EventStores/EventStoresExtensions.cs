using EasySharp.Core.Helpers;
using EasySharp.EfCore.Option;
using EasySharp.EventStores.Aggregate;
using EasySharp.EventStores.Repository;
using EasySharp.EventStores.Stores.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasySharp.EventStores
{
    public static class EventStoresExtensions
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddEventStore<TAggregate>(this IServiceCollection services) where TAggregate : IAggregate
        {
            Configuration = EasySharpServices.Builder();

            var options = new EventStoresOptions();
            Configuration.GetSection(nameof(EventStoresOptions)).Bind(options);
            services.Configure<EventStoresOptions>(Configuration.GetSection(nameof(EventStoresOptions)));

            var dbContextOptions = new EfCoreOptions();
            Configuration.GetSection(nameof(EfCoreOptions)).Bind(dbContextOptions);

            if (options.Enable == false)
            {
                return services;
            }

            switch (options.EventStoreType.ToLowerInvariant())
            {
                case "efcore":
                case "ef":
                    services.AddEfCoreEventStore(opts =>
                        opts.UseSqlServer(
                            dbContextOptions.ConnectionString
                        ));
                    break;
                default:
                    throw new Exception($"Event store type '{options.EventStoreType}' is not supported");
            }

            services.AddScoped<IRepository<TAggregate>, Repository<TAggregate>>();
            services.AddScoped<IEventStore, EventStore>();

            return services;
        }
    }
}
