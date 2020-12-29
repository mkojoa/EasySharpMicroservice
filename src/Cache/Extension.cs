using EasySharp.Cache.Option;
using EasySharp.Cache.Store.LocalStorage;
using EasySharp.Cache.Store.Redis;
using EasySharp.Core.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Cache
{
    public static class Extension
    {
        public static IConfigurationRoot Configuration { get; set; }


        public static IServiceCollection AddCacheable(this IServiceCollection services)
        {
            if (EasySharpServices.IsInitialized == false)
            {
                EasySharpServices.Services = services;
                EasySharpServices.Initialize();
            }

            Configuration = EasySharpServices.Builder();

            var options = new Cacheable();
            Configuration.GetSection(nameof(Cacheable)).Bind(options);

            var redisOptions = options.Redis ?? new RedisOptions(); 
            var localStorageOptions = options.LocalStorage ?? new LocalStorageOptions();

            if (redisOptions.Enable == true)
            {
                services.RedisCache(redisOptions);
            }

            if (localStorageOptions.Enable == true)
            {
                services.AddLocalStorage(Configuration);
            }

            return services;
        }
    }
}
