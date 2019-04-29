﻿using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using FireProtectionV1.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using FireProtectionV1.Configuration;

namespace FireProtectionV1.Web.Startup
{
    public class Startup
    {
        //private readonly IConfigurationRoot _appConfiguration;
        //public Startup(IHostingEnvironment env)
        //{
        //    _appConfiguration = env.GetAppConfiguration();
        //}

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddAbpDbContext<FireProtectionV1DbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "FireProtectionV1 WebApi", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                var basePath = Path.GetDirectoryName(typeof(Startup).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "FireProtectionV1.Application.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
                var xmlDto = Path.Combine(basePath, "FireProtectionV1.Common.xml");
                if (File.Exists(xmlDto))
                {
                    options.IncludeXmlComments(xmlDto);
                }
                var xmlCommon = Path.Combine(basePath, "FireProtectionV1.Core.xml");
                if (File.Exists(xmlCommon))
                {
                    options.IncludeXmlComments(xmlCommon);
                }
                //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //var commentsFileName = "FireProtectionV1.Application.xml";
                //var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                //options.IncludeXmlComments(commentsFile);
            });
            //services.AddHttpContextAccessor();
            //Configure Abp and Dependency Injection
            services.AddSession();
            return services.AddAbp<FireProtectionV1WebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint            
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)            
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(ConfigHelper.Configuration["App:ServerRootAddress"] + "/swagger/v1/swagger.json", "FireProtectionV1 API");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("FireProtectionV1.Web.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger
        }
    }
}
