using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.Extensions;
using FireProtectionV1.Configuration;
using FireProtectionV1.EntityFrameworkCore;
using FireProtectionV1.EntityFrameworkCore.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace FireProtectionV1.Migrator
{
    public class MigrateExecuter : ITransientDependency  
    {
        public IConfiguration Configuration { get; set; }

        public IUnitOfWorkManager _unitOfWorkManager { get; set; }

        public IDbContextResolver _dbContextResolver { get; set; }

        public IIocResolver iocResolver { set; get; }

        public MigrateExecuter()
        {
            
        }

        public bool Run(bool skipConnVerification)
        {
            var hostConnStr = ConnectionStringHelper.GetConnectionString();
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                Console.WriteLine("未指定数据库连接字符串");
                return false;
            }

            Console.WriteLine("数据库连接信息: " + hostConnStr);
            if (!skipConnVerification)
            {
                Console.WriteLine("是否迁移数据库至新版本? (Y/N): ");
                var command = Console.ReadLine();
                if (!command.IsIn("Y", "y"))
                {
                    Console.WriteLine("迁移取消.");
                    return false;
                }
            }

            Console.WriteLine("开始数据库迁移...");

            try
            {
                using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
                {
                    var dbContext = _dbContextResolver.Resolve<FireProtectionV1DbContext>(hostConnStr, null);
                    dbContext.Database.Migrate();

                    SeedHelper.SeedHostDb(dbContext);
                    _unitOfWorkManager.Current.SaveChanges();

                    //commonDbContext.Dispose();
                    dbContext.Dispose();
                    uow.Complete();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("数据迁移发生错误:");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Canceled migrations.");
                return false;
            }

            Console.WriteLine("数据迁移完成.");
            Console.WriteLine("--------------------------------------------------------");



            return true;
        }
    }
}
