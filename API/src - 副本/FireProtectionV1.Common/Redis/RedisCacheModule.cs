using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using FireProtectionV1.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireProtectionV1.Common.Redis
{
    /// <summary>
    /// AbpRedis缓存注入模块
    /// </summary>
    [DependsOn(typeof(AbpRedisCacheModule))]
    public class RedisCacheModule : AbpModule
    {
        public override void PreInitialize()
        {
            base.PreInitialize();
            bool.TryParse(ConfigHelper.Configuration["Redis:Enable"], out bool redisEnable);
            if (redisEnable)
            {
                Configuration.Caching.UseRedis(options =>
                {
                    options.ConnectionString = ConfigHelper.Configuration["Redis:Connection"];
                    if (string.IsNullOrEmpty(options.ConnectionString))
                    {
                        throw new ArgumentException("未指定Redis连接");
                    }

                    int.TryParse(ConfigHelper.Configuration["Redis:DbId"], out int dbId);
                    options.DatabaseId = dbId;
                });
            }

            // 设置所有缓存过期时间
            Configuration.Caching.ConfigureAll(config =>
            {
                config.DefaultSlidingExpireTime = TimeSpan.FromHours(1);
            });

            //设置某个缓存的默认过期时间 根据 "CacheName" 来区分
            //Configuration.Caching.Configure("CacheName", cache =>
            //{
            //    cache.DefaultAbsoluteExpireTime = TimeSpan.FromMinutes(5);
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RedisCacheModule).GetAssembly());
        }

        public override void PostInitialize()
        {

        }
    }
}
