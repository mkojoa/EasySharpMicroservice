using EasySharp.Cache.Option;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasySharp.Cache.Store.Redis
{
    public static class RedisExtensions
    {
        /// <summary>
        /// Adds IRedisCacheService to IServiceCollection.
        /// </summary>
        public static IServiceCollection RedisCache(this IServiceCollection services, RedisOptions option)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = option.Connection;
                options.InstanceName = option.InstanceName;
            });

            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            return services;
        }
    }
}
