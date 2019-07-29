﻿// <auto-generated />
using System;
using FireProtectionV1.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FireProtectionV1.Migrations
{
    [DbContext(typeof(FireProtectionV1DbContext))]
    partial class FireProtectionV1DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.EquipmentNo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("EquiNo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("FireSystemId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("EquipmentNo");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireDept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("FireDept");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("FireSystem");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("AreaId");

                    b.Property<string>("ContractName")
                        .HasMaxLength(50);

                    b.Property<string>("ContractPhone")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("InvitationCode")
                        .HasMaxLength(10);

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(9,6)");

                    b.Property<decimal>("Lng")
                        .HasColumnType("decimal(9,6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Patrol");

                    b.Property<int>("SafeUnitId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.ToTable("FireUnit");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireUnitAttention", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireDeptUserId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("FireUnitAttention");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireUnitPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("FirePlan");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("FireUnitPlan");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.FireUntiSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireSystemId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("FireUntiSystem");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.SafeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContractName")
                        .HasMaxLength(50);

                    b.Property<string>("ContractPhone")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("InvitationCode")
                        .HasMaxLength(10);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SafeUnit");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.AlarmCheck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlarmDataId");

                    b.Property<byte>("CheckState");

                    b.Property<string>("Content")
                        .HasMaxLength(300);

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("FireSysType");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("UserId");

                    b.Property<string>("VioceUrl")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("AlarmCheck");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.AlarmToElectric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlarmLimit")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<double>("Analog");

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.ToTable("AlarmToElectric");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.AlarmToFire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("AlarmToFire");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.AlarmToGas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CollectDeviceId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<decimal>("CurrentData");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SafeRange")
                        .HasMaxLength(50);

                    b.Property<string>("TerminalDeviceSn")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("AlarmToGas");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.BreakDown", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DataId");

                    b.Property<int>("FireUnitId");

                    b.Property<byte>("HandleStatus");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<DateTime>("SolutionTime");

                    b.Property<byte>("SolutionWay");

                    b.Property<byte>("Source");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("BreakDown");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToDuty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("DutyPicture")
                        .HasMaxLength(100);

                    b.Property<string>("DutyRemark");

                    b.Property<byte>("DutyStatus");

                    b.Property<int>("FireUnitId");

                    b.Property<int>("FireUnitUserId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("DataToDuty");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToDutyProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DutyId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProblemPicture")
                        .HasMaxLength(100);

                    b.Property<string>("ProblemRemark")
                        .HasMaxLength(200);

                    b.Property<int>("ProblemRemarkType");

                    b.Property<string>("ProblemVoice")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("DataToDutyProblem");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToPatrol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireUnitId");

                    b.Property<int>("FireUnitUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<byte>("PatrolStatus");

                    b.HasKey("Id");

                    b.ToTable("DataToPatrol");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToPatrolDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("DeviceSn")
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PatrolAddress");

                    b.Property<int>("PatrolId");

                    b.Property<byte>("PatrolStatus");

                    b.Property<byte>("PatrolType");

                    b.HasKey("Id");

                    b.ToTable("DataToPatrolDetail");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToPatrolDetailFireSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireSystemID");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("PatrolDetailId");

                    b.HasKey("Id");

                    b.ToTable("DataToPatrolDetailFireSystem");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DataToPatrolDetailProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("PatrolDetailId");

                    b.Property<string>("ProblemPicture")
                        .HasMaxLength(100);

                    b.Property<string>("ProblemRemark")
                        .HasMaxLength(500);

                    b.Property<int>("ProblemRemarkType");

                    b.Property<string>("ProblemVoice")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("DataToPatrolDetailProblem");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.Detector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorTypeId");

                    b.Property<byte>("FireSysType");

                    b.Property<int>("FireUnitId");

                    b.Property<int>("GatewayId");

                    b.Property<string>("Identify")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Location")
                        .HasMaxLength(100);

                    b.Property<string>("Origin")
                        .HasMaxLength(50);

                    b.Property<string>("State")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Detector");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.DetectorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("GBType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("DetectorType");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.Fault", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<string>("FaultRemark")
                        .HasMaxLength(200);

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<byte>("ProcessState");

                    b.HasKey("Id");

                    b.ToTable("Fault");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.Gateway", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("FireSysType");

                    b.Property<int>("FireUnitId");

                    b.Property<string>("Identify")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Origin")
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<DateTime>("StatusChangeTime");

                    b.HasKey("Id");

                    b.ToTable("Gateway");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.PhotosPathSave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DataId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("PhotosPathSave");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.RecordAnalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Analog");

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("RecordAnalog");
                });

            modelBuilder.Entity("FireProtectionV1.FireWorking.Model.RecordOnline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<bool>("IsDeleted");

                    b.Property<sbyte>("State");

                    b.HasKey("Id");

                    b.ToTable("RecordOnline");
                });

            modelBuilder.Entity("FireProtectionV1.HydrantCore.Model.Hydrant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int>("AreaId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<decimal>("DumpEnergy");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(9,6)");

                    b.Property<decimal>("Lng")
                        .HasColumnType("decimal(9,6)");

                    b.Property<string>("Sn")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Hydrant");
                });

            modelBuilder.Entity("FireProtectionV1.HydrantCore.Model.HydrantAlarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("HandleStatus");

                    b.Property<string>("HandleUser");

                    b.Property<int>("HydrantId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProblemRemark")
                        .HasMaxLength(200);

                    b.Property<byte>("ProblemRemarkType");

                    b.Property<bool>("ReadFlag");

                    b.Property<DateTime>("SoultionTime");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("HydrantAlarm");
                });

            modelBuilder.Entity("FireProtectionV1.HydrantCore.Model.HydrantPressure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("HydrantId");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("Pressure");

                    b.HasKey("Id");

                    b.ToTable("HydrantPressure");
                });

            modelBuilder.Entity("FireProtectionV1.Infrastructure.Model.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AreaCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("AreaPath")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<byte>("Grade");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("FireProtectionV1.Infrastructure.Model.FireUnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("FireUnitType");
                });

            modelBuilder.Entity("FireProtectionV1.MiniFireStationCore.Model.MiniFireStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ContactName");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Lat")
                        .HasColumnType("decimal(9,6)");

                    b.Property<int>("Level");

                    b.Property<decimal>("Lng")
                        .HasColumnType("decimal(9,6)");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PersonNum");

                    b.HasKey("Id");

                    b.ToTable("MiniFireStation");
                });

            modelBuilder.Entity("FireProtectionV1.SettingCore.Model.FireSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("MaxValue");

                    b.Property<double>("MinValue");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FireSetting");
                });

            modelBuilder.Entity("FireProtectionV1.StreetGridCore.Model.StreetGridEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("EventType");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Status");

                    b.Property<int>("StreetGridUserId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("StreetGridEvent");
                });

            modelBuilder.Entity("FireProtectionV1.StreetGridCore.Model.StreetGridEventRemark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Remark");

                    b.Property<int>("StreetGridEventId");

                    b.HasKey("Id");

                    b.ToTable("StreetGridEventRemark");
                });

            modelBuilder.Entity("FireProtectionV1.StreetGridCore.Model.StreetGridUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Community");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("GridName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("StreetGridUser");
                });

            modelBuilder.Entity("FireProtectionV1.SupervisionCore.Model.Supervision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CheckResult");

                    b.Property<string>("CheckUser");

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DocumentDeadline");

                    b.Property<int>("DocumentInspection");

                    b.Property<int>("DocumentMajor");

                    b.Property<int>("DocumentPunish");

                    b.Property<int>("DocumentReview");

                    b.Property<int>("DocumentSite");

                    b.Property<int>("FireDeptUserId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Remark");

                    b.HasKey("Id");

                    b.ToTable("Supervision");
                });

            modelBuilder.Entity("FireProtectionV1.SupervisionCore.Model.SupervisionDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsOK");

                    b.Property<int>("SupervisionId");

                    b.Property<int>("SupervisionItemId");

                    b.HasKey("Id");

                    b.ToTable("SupervisionDetail");
                });

            modelBuilder.Entity("FireProtectionV1.SupervisionCore.Model.SupervisionDetailRemark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Remark");

                    b.Property<int>("SupervisionDetailId");

                    b.HasKey("Id");

                    b.ToTable("SupervisionDetailRemark");
                });

            modelBuilder.Entity("FireProtectionV1.SupervisionCore.Model.SupervisionItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("SupervisionItem");
                });

            modelBuilder.Entity("FireProtectionV1.SupervisionCore.Model.SupervisionPhotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int>("SupervisionId");

                    b.HasKey("Id");

                    b.ToTable("SupervisionPhotos");
                });

            modelBuilder.Entity("FireProtectionV1.User.Model.FireDeptUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireDeptId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("FireDeptUser");
                });

            modelBuilder.Entity("FireProtectionV1.User.Model.FireUnitUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireUnitInfoID");

                    b.Property<bool>("GuideFlage");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("FireUnitUser");
                });

            modelBuilder.Entity("FireProtectionV1.User.Model.FireUnitUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountID");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.ToTable("FireUnitUserRole");
                });

            modelBuilder.Entity("FireProtectionV1.User.Model.HydrantUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("GuideFlage");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("HydrantUser");
                });

            modelBuilder.Entity("FireProtectionV1.User.Model.HydrantUserArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountID");

                    b.Property<int>("AreaID");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("HydrantUserArea");
                });

            modelBuilder.Entity("FireProtectionV1.VersionCore.Model.Suggest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("suggest");

                    b.HasKey("Id");

                    b.ToTable("Suggest");
                });
#pragma warning restore 612, 618
        }
    }
}
