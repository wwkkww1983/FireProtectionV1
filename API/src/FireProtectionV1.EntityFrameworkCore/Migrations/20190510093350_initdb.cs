﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmToElectric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DetectorId = table.Column<int>(nullable: false),
                    CurrentData = table.Column<decimal>(nullable: false),
                    SafeRange = table.Column<string>(maxLength: 50, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmToElectric", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmToFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DetectorId = table.Column<int>(nullable: false),
                    AlarmTitle = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmRemark = table.Column<string>(maxLength: 200, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmToFire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmToGas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrentData = table.Column<decimal>(nullable: false),
                    SafeRange = table.Column<string>(maxLength: 50, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    CollectDeviceId = table.Column<int>(nullable: false),
                    TerminalDeviceSn = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmToGas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AreaCode = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    AreaPath = table.Column<string>(maxLength: 50, nullable: false),
                    Grade = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Detector",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    DetectorTypeId = table.Column<int>(nullable: false),
                    FireSysType = table.Column<int>(nullable: false),
                    GatewayId = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    AlarmTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectorType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fault",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FaultTitle = table.Column<string>(maxLength: 50, nullable: false),
                    FaultRemark = table.Column<string>(maxLength: 200, nullable: true),
                    ProcessState = table.Column<byte>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false),
                    DeviceType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fault", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireDept",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AreaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireDept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireDeptUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FireDeptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireDeptUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MinValue = table.Column<double>(nullable: false),
                    MaxValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true),
                    InvitationCode = table.Column<string>(maxLength: 10, nullable: true),
                    SafeUnitId = table.Column<int>(nullable: false),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    FireUnitInfoID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitAccountRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountID = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitAccountRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gateway",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusChangeTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateway", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hydrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sn = table.Column<string>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hydrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HydrantAlarm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HydrantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantAlarm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HydrantPressure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HydrantId = table.Column<int>(nullable: false),
                    Pressure = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantPressure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireStation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ContactName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    PersonNum = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireStation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SafeUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    InvitationCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafeUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreetGrid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Street = table.Column<string>(nullable: false),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGrid", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreetGridEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    EventType = table.Column<string>(nullable: true),
                    StreetGridId = table.Column<int>(nullable: false),
                    Community = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGridEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreetGridEventRemark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StreetGridEventId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGridEventRemark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supervision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    CheckUser = table.Column<string>(nullable: true),
                    CheckResult = table.Column<int>(nullable: false),
                    FireDeptUserId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    DocumentSite = table.Column<int>(nullable: false),
                    DocumentDeadline = table.Column<int>(nullable: false),
                    DocumentMajor = table.Column<int>(nullable: false),
                    DocumentReview = table.Column<int>(nullable: false),
                    DocumentInspection = table.Column<int>(nullable: false),
                    DocumentPunish = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervision", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SupervisionId = table.Column<int>(nullable: false),
                    SupervisionItemId = table.Column<int>(nullable: false),
                    IsOK = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisionDetailRemark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SupervisionDetailId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionDetailRemark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupervisionItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmToElectric");

            migrationBuilder.DropTable(
                name: "AlarmToFire");

            migrationBuilder.DropTable(
                name: "AlarmToGas");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Detector");

            migrationBuilder.DropTable(
                name: "DetectorType");

            migrationBuilder.DropTable(
                name: "Fault");

            migrationBuilder.DropTable(
                name: "FireDept");

            migrationBuilder.DropTable(
                name: "FireDeptUser");

            migrationBuilder.DropTable(
                name: "FireSetting");

            migrationBuilder.DropTable(
                name: "FireUnit");

            migrationBuilder.DropTable(
                name: "FireUnitAccount");

            migrationBuilder.DropTable(
                name: "FireUnitAccountRole");

            migrationBuilder.DropTable(
                name: "FireUnitType");

            migrationBuilder.DropTable(
                name: "Gateway");

            migrationBuilder.DropTable(
                name: "Hydrant");

            migrationBuilder.DropTable(
                name: "HydrantAlarm");

            migrationBuilder.DropTable(
                name: "HydrantPressure");

            migrationBuilder.DropTable(
                name: "MiniFireStation");

            migrationBuilder.DropTable(
                name: "SafeUnit");

            migrationBuilder.DropTable(
                name: "StreetGrid");

            migrationBuilder.DropTable(
                name: "StreetGridEvent");

            migrationBuilder.DropTable(
                name: "StreetGridEventRemark");

            migrationBuilder.DropTable(
                name: "Supervision");

            migrationBuilder.DropTable(
                name: "SupervisionDetail");

            migrationBuilder.DropTable(
                name: "SupervisionDetailRemark");

            migrationBuilder.DropTable(
                name: "SupervisionItem");
        }
    }
}
