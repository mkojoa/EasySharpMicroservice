using EasySharp.Cache.Helpers;
using EasySharp.Core.Helpers;
using EasySharp.Swagger.Helpers;
using EasySharp.Swagger.Option;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace EasySharp.Swagger
{
    public static class Extensions
    {
        public static IConfigurationRoot Configuration { get; set; }
        private const string RegistryName = "docs.swagger";

        private static ConcurrentDictionary<string, bool> _registry = new ConcurrentDictionary<string, bool>();

        public static IServiceCollection AddDocs(this IServiceCollection services)
        {
            if (EasySharpServices.IsInitialized == false)
            {
                EasySharpServices.Services = services;
                EasySharpServices.Initialize();
            }

            Configuration = EasySharpServices.Builder();

            var options = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(options);

            var dt = _registry.TryAdd(RegistryName, true);

            if (!options.Enabled || !dt)
            {
                return services;
            }

            services.AddSingleton(options);

            Uri termsOfService = new Uri(options.TermsOfService);
            Uri contactUrl = new Uri(options.SecurityOptions.Contact.Url);
            Uri licenseUrl = new Uri(options.SecurityOptions.License.Url);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo
                {
                    Version = options.Name,
                    Title = options.Title,
                    Description = options.Description,
                    TermsOfService = termsOfService,
                    Contact = new OpenApiContact
                    {
                        Name = options.SecurityOptions.Contact.Name,
                        Email = options.SecurityOptions.Contact.Email,
                        Url = contactUrl,
                    },
                    License = new OpenApiLicense
                    {
                        Name = options.SecurityOptions.License.Name,
                        Url = licenseUrl
                    }
                });

                c.CustomSchemaIds(type => type.ToString());

                if (options.SecurityOptions.XmlDoc)
                {

                    // curret project name not libraryName
                    var commentsFileName = $"{Assembly.GetEntryAssembly().GetName().Name}.XML";

                    var commentsFile = Path.Combine(System.AppContext.BaseDirectory, commentsFileName);

                    //FileHelpers.CreateFileIfDoesNotExist(commentsFile);

                    c.IncludeXmlComments(commentsFile);
                }

                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(options.SecurityOptions.AuthorityURL),
                                TokenUrl = new Uri(options.SecurityOptions.TokenUrl),
                                RefreshUrl = new Uri(options.SecurityOptions.TokenUrl),
                                Scopes = new Dictionary<string, string>
                                    {
                                        { options.SecurityOptions.Scope.Name, options.SecurityOptions.Scope.Description },
                                    }
                            }
                        }
                    });

                    c.OperationFilter<AuthorizeCheckOperationFilter>(options);
                }
            });

            if (options.IncludeSecurity)
            {
                services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                        .AddIdentityServerAuthentication(x =>
                        {
                            x.Authority = options.SecurityOptions.Authority;
                            x.ApiName = options.SecurityOptions.ApiName;
                            x.SupportedTokens = SupportedTokens.Both;
                            x.ApiSecret = options.SecurityOptions.ClientSecret;
                            x.RequireHttpsMetadata = bool.Parse(options.SecurityOptions.RequireHttpsMetadata);

                            x.SaveToken = true;
                            x.EnableCaching = true;
                            x.CacheDuration = TimeSpan.FromMinutes(10);

                        });
            }

            return services;
        }

        public static IApplicationBuilder UseDocs(this IApplicationBuilder app)
        {
            Configuration = EasySharpServices.Builder();

            var options = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(options);

            if (!options.Enabled)
            {
                return app;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            app.UseStaticFiles()
               .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");


            return options.ReDocEnabled
                ? app.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;

                    if (options.SecurityOptions.Folder == "")
                    {
                        c.SpecUrl = $"{options.Name}/swagger.json";
                    }
                    else
                    {
                        c.SpecUrl = $"{options.SecurityOptions.Folder}/{options.Name}/swagger.json";

                    }
                })
                : app.UseSwaggerUI(c =>
                {
                    if (options.SecurityOptions.Folder == "")
                    {
                        c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    }
                    else
                    {
                        c.SwaggerEndpoint($"/{options.SecurityOptions.Folder}/{routePrefix}/{options.Name}/swagger.json", options.Title);

                    }

                    c.RoutePrefix = routePrefix;

                    c.EnableDeepLinking();

                    // Additional OAuth settings
                    c.OAuthClientId(options.SecurityOptions.ApiId);
                    c.OAuthClientSecret(options.SecurityOptions.ClientSecret);
                    c.OAuthAppName(options.Description);
                    c.OAuthScopeSeparator(" ");
                    c.OAuthUsePkce();
                });
        }
    }
}
