﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using LGDShop.API.Filter;
using LGDShop.API.Options;
using LGDShop.DataAccess;
using LGDShop.DataAccess.Data;
using LGDShop.Services.EntityServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StsServerIdentity;
using StsServerIdentity.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace LGDShop.API
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
            services.AddDbContext<ShopDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        // auth server base endpoint (will use to search for disco doc)
                        options.Authority = "http://localhost:5000";
                        options.ApiName = IdentityServerConfig.ApiName; // required audience of access tokens
                        options.RequireHttpsMetadata = false; // dev only!
                    });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "龙广电台商城 - API", Version = "v1" });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    TokenUrl = "http://localhost:5000/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                          { ApiScopes.General, "Access general operations" }
                    }
                });

                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    { "oauth2", new[] { "readAccess", "writeAccess" } }
                //});


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var baseDirectory = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options => options.AddPolicy("SpaOnly", b =>
                        b.WithOrigins("http://localhost:4300",
                                      "https://localhost:4300")
                        .AllowAnyMethod()
                        .AllowAnyHeader()));


            var assembly = Assembly.GetAssembly(typeof(Startup));
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assembly))
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;  //to prevent reference loops from happening
                    options.SerializerSettings.Formatting = Formatting.Indented;    //For pretty print Swagger JSON
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //app services
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCors("SpaOnly");


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(o =>
            {
                o.RouteTemplate = "docs/{documentName}/docs.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(o =>
            {
                //crediential 
                o.OAuthClientId(IdentityServerConfig.ApiSwaggerClientID);
                o.OAuthClientSecret(IdentityServerConfig.ApiSwaggerClientSecret); //Leaving it blank doesn't work
                o.OAuthAppName(SwaggerConfig.OAuthAppName);

                o.DocumentTitle = "Swagger UI - 龙广交通商城";
                o.SwaggerEndpoint("/docs/v1/docs.json", "龙广交通商城 - API");
                o.RoutePrefix = "docs";
                o.DisplayOperationId();
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
