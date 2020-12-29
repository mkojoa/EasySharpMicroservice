using EasySharp.Core.Helpers;
using EasySharp.EfCore.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.EfCore
{
    public static class Extensions
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddEfCore<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            if (EasySharpServices.IsInitialized == false)
            {
                EasySharpServices.Services = services;
                EasySharpServices.Initialize();
            }

            Configuration = EasySharpServices.Builder();

            var option = new EfCoreOptions();
            Configuration.GetSection(nameof(EfCoreOptions)).Bind(option);

            services.AddDbContext<TContext>(
                options =>
                options.UseSqlServer(
                    option.ConnectionString
                ));
            
            return services;
        }
    }
}
