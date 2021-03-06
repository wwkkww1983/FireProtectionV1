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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using FireProtectionV1.Web.SwaggerExt;
using FireProtectionV1.Common.Enum;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Serialization;
using UEditor.Core;

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
            services.AddUEditorService("ueditor.json");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddJsonOptions(options =>
            {
                //配置tojson格式配置 DefaultContractResolver 为和后台属性名保持一致（即：后台属性名为OrderName，前端js获得属性名也为OrderName）
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //修改为CamelCasePropertyNamesContractResolver，为js的驼峰格式，即abp默认格式（即：后台属性名为OrderName，前端js获得属性名为orderName）
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            //添加认证Cookie信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/login");
                options.AccessDeniedPath = new PathString("/denied");
            });

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI            
            services.AddSwaggerGen(options =>
            {
                Info info= new Info { Title = "FireProtectionV1 WebApi", Version = "v1"
                    //,Description= "FireDept（消防部门）\r\nFireDeptUser（消防部门用户）\r\nFireUnit（防火单位）\r\nFireUnitUser（防火单位用户）"
                };
                options.SwaggerDoc("v1", info);
                options.OperationFilter<SwaggerFileUploadFilter>();
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

                //options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.UseReferencedDefinitionsForEnums();

                options.SwaggerGeneratorOptions.ParameterFilters.AddEnumParameterFilter();
                options.SwaggerGeneratorOptions.DocumentFilters.AddEnumDocumentFilters(typeof(FireProtectionV1CoreModule).Assembly,typeof(NormalStatus).Assembly);
                
            });
            //services.AddHttpContextAccessor();
            //Configure Abp and Dependency Injection
            services.AddSession();
            ///配置文件大小限制
            //services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 60000000;
            //});

            //配置跨域处理，允许所有来源：
            services.AddCors(options =>
            options.AddPolicy("自定义",
            p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials())
            );
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
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "upload")),
                RequestPath = "/upload",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=36000");
                }
            });
            app.UseStaticFiles();
            app.UseSession();
            //验证中间件
            app.UseAuthentication();
            app.UseCors("自定义");
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

            //文件地址隐藏 
            if (!Directory.Exists(@"App_Data/Files"))//判断是否存在
            {
                Directory.CreateDirectory(@"App_Data/Files");//创建新路径
            }
            app.UseStaticFiles(new StaticFileOptions()
            {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"App_Data/Files")),
                ServeUnknownFileTypes = true,
                RequestPath = new PathString("/Src")
            });
        
    }
    }
}
