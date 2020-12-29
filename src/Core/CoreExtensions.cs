using EasySharp.Core.AntiXss;
using EasySharp.Core.Behavior;
using EasySharp.Core.Commands;
using EasySharp.Core.Cors;
using EasySharp.Core.Events;
using EasySharp.Core.Exceptions;
using EasySharp.Core.Helpers;
using EasySharp.Core.Queries;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace EasySharp.Core
{
    public static class CoreExtensions
    {

        /// <summary>
        /// Application Configuration
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddEasySharp(this IServiceCollection services, params Type[] types)
        {
            var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(assembly);
            }

            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            EasySharpServices.Services = services;

            if (EasySharpServices.IsInitialized == false)
                EasySharpServices.Initialize();
            
            Configuration = EasySharpServices.Builder();

            services.AddOptions();

            services
                .AddMvc(opt => {
                    opt.Filters.Add<ExceptionFilter>();
                    opt.EnableEndpointRouting = false;
                })
                .AddNewtonsoftJson(jopt => jopt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblies(assemblies); }); 


            return services;
        }

        public static IApplicationBuilder UseEasySharp(this IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseAntiXssMiddleware(Configuration);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCorsOption();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
