using Abp.EntityFrameworkCore;
using FireProtectionV1.FireWorking.Model;
using FireProtectionV1.Enterprise.Model;
using FireProtectionV1.Infrastructure.Model;
using FireProtectionV1.MiniFireStationCore.Model;
using FireProtectionV1.User.Model;
using Microsoft.EntityFrameworkCore;
using FireProtectionV1.SupervisionCore.Model;
using FireProtectionV1.StreetGridCore.Model;
using FireProtectionV1.SettingCore.Model;
using FireProtectionV1.HydrantCore.Model;

namespace FireProtectionV1.EntityFrameworkCore
{
    public class FireProtectionV1DbContext : AbpDbContext
    {
        public DbSet<DataToPatrol> DataToPatrol { get; set; }
        public DbSet<DataToPatrolDetail> DataToPatrolDetail { get; set; }
        public DbSet<DataToPatrolDetailProblem> DataToPatrolDetailProblem { get; set; }
        public DbSet<DataToDuty> DataToDuty { get; set; }
        public DbSet<DataToDutyProblem> DataToDutyProblem { get; set; }
        public DbSet<Fault> Fault { get; set; }
        public DbSet<DetectorType> DetectorType { get; set; }
        public DbSet<Detector> Detector { get; set; }
        public DbSet<Gateway> Gateway { get; set; }
        public DbSet<AlarmToGas> AlarmToGas { get; set; }
        public DbSet<AlarmToFire> AlarmToFire { get; set; }
        public DbSet<AlarmToElectric> AlarmToElectric { get; set; }
        public DbSet<FireUnit> FireUnit { get; set; }
        public DbSet<SafeUnit> SafeUnit { get; set; }
        public DbSet<FireUnitUser> FireUnitUser { get; set; }
        public DbSet<FireUnitUserRole> FireUnitUserRole { get; set; }
        public DbSet<FireDeptUser> FireDeptUser { get; set; }
        public DbSet<FireDept> FireDept { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<FireUnitType> FireUnitType { get; set; }
        public DbSet<MiniFireStation> MiniFireStation { get; set; }
        public DbSet<Supervision> Supervision { get; set; }
        public DbSet<SupervisionItem> SupervisionItem { get; set; }
        public DbSet<SupervisionDetail> SupervisionDetail { get; set; }
        public DbSet<SupervisionDetailRemark> SupervisionDetailRemark { get; set; }
        public DbSet<StreetGrid> StreetGrid { get; set; }
        public DbSet<StreetGridEvent> StreetGridEvent { get; set; }
        public DbSet<StreetGridEventRemark> StreetGridEventRemark { get; set; }
        public DbSet<FireSetting> FireSetting { get; set; }
        public DbSet<Hydrant> Hydrant { get; set; }
        public DbSet<HydrantAlarm> HydrantAlarm { get; set; }
        public DbSet<HydrantPressure> HydrantPressure { get; set; }

        public FireProtectionV1DbContext(DbContextOptions<FireProtectionV1DbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 设置经纬度的精度
            modelBuilder.Entity<FireUnit>().Property(item=>item.Lng).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<FireUnit>().Property(item => item.Lat).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<MiniFireStation>().Property(item => item.Lng).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<MiniFireStation>().Property(item => item.Lat).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<Hydrant>().Property(item => item.Lng).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<Hydrant>().Property(item => item.Lat).HasColumnType("decimal(9,6)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
