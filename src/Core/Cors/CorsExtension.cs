using EasySharp.Core.Cors.Option;
using EasySharp.Core.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasySharp.Core.Cors
{
    public static class CorsExtension
    {
        /// <summary>
        /// Application Configuration
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsOption(this IServiceCollection services)
        {
            

            if (EasySharpServices.IsInitialized == false)
            {
                EasySharpServices.Services = services;
                EasySharpServices.Initialize();
            }

            Configuration = EasySharpServices.Builder();

            var options = new CorsOptions();
            Configuration.GetSection(nameof(CorsOptions)).Bind(options);

            //if Links=null, set links array to empty array
            var linksOption = options.Links ?? new string[] { };

            var policyName = options.Name;

            if (options.Enabled)
            {
                string[] clientUrls = linksOption.ToArray();

                services.AddCors(opt =>
                {
                    opt.AddPolicy(policyName,
                        fbuilder => fbuilder.WithOrigins(clientUrls)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
                });
            }

            return services;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsOption(this IApplicationBuilder app)
        {
            Configuration = EasySharpServices.Builder();

            var options = new CorsOptions();
            Configuration.GetSection(nameof(CorsOptions)).Bind(options);

            if (options.Enabled)
            {
                app.UseCors(options.Name);
            }

            return app;
        }
    }
}
