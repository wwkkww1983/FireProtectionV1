using Abp.EntityFrameworkCore;
using FireProtectionV1.Account.Model;
using FireProtectionV1.Alarm.Model;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace FireProtectionV1.EntityFrameworkCore
{
    public class FireProtectionV1DbContext : AbpDbContext
    {
        public DbSet<FireUnit> FireUnit { get; set; }
        public DbSet<GovernmentInfo> GovernmentInfo { get; set; }
        public DbSet<SafeUnit> SafeUnit { get; set; }
        public DbSet<FireUnitAccount> FireUnitAccount { get; set; }
        public DbSet<FireUnitAccountRole> FireUnitAccountRole { get; set; }
        public DbSet<FireDeptUser> FireDeptUser { get; set; }
        public DbSet<FireDept> FireDept { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<FireUnitType> FireUnitType { get; set; }
        public DbSet<AlarmToGas> AlarmToGas { get; set; }
        public DbSet<AlarmToFire> AlarmToFire { get; set; }
        public DbSet<AlarmToElectric> AlarmToElectric { get; set; }

        public FireProtectionV1DbContext(DbContextOptions<FireProtectionV1DbContext> options) 
            : base(options)
        {

        }
    }
}
