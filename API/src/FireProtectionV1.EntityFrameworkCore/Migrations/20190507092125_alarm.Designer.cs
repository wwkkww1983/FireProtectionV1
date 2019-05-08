﻿// <auto-generated />
using System;
using FireProtectionV1.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FireProtectionV1.Migrations
{
    [DbContext(typeof(FireProtectionV1DbContext))]
    [Migration("20190507092125_alarm")]
    partial class alarm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FireProtectionV1.Account.Model.FireDeptUser", b =>
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

            modelBuilder.Entity("FireProtectionV1.Account.Model.FireUnitUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireUnitInfoID");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("FireUnitAccount");
                });

            modelBuilder.Entity("FireProtectionV1.Account.Model.FireUnitUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountID");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.ToTable("FireUnitAccountRole");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.AlarmToElectric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<decimal>("CurrentData");

                    b.Property<int>("DetectorId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("SafeRange")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("AlarmToElectric");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.AlarmToFire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlarmRemark")
                        .HasMaxLength(200);

                    b.Property<string>("AlarmTitle")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("DetectorId");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("AlarmToFire");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.AlarmToGas", b =>
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

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.ControllerElectric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NetworkState")
                        .HasMaxLength(10);

                    b.Property<string>("Sn")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("ControllerElectric");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.ControllerFire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<int>("FireUnitId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("NetworkState")
                        .HasMaxLength(10);

                    b.Property<string>("Sn")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("ControllerFire");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.DetectorElectric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ControllerId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("DetectorElectric");
                });

            modelBuilder.Entity("FireProtectionV1.Alarm.Model.DetectorFire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ControllerId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("DetectorFire");
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

                    b.Property<decimal>("Lat");

                    b.Property<decimal>("Lng");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SafeUnitId");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.ToTable("FireUnit");
                });

            modelBuilder.Entity("FireProtectionV1.Enterprise.Model.SafeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaId");

                    b.Property<string>("ContractName")
                        .HasMaxLength(50);

                    b.Property<string>("ContractPhone")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SafeUnit");
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
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("FireUnitType");
                });

            modelBuilder.Entity("FireProtectionV1.MiniFireStationCore.Model.MiniFireStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Contact");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreationTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Lat");

                    b.Property<int>("Level");

                    b.Property<decimal>("Lng");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PersonNum");

                    b.HasKey("Id");

                    b.ToTable("MiniFireStation");
                });
#pragma warning restore 612, 618
        }
    }
}
