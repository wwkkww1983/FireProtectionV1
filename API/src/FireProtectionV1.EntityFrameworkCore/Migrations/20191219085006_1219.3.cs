using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _12193 : Migration
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
                    FireElectricDeviceId = table.Column<int>(nullable: false),
                    Sign = table.Column<string>(nullable: false),
                    Analog = table.Column<double>(nullable: false),
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
                    FireAlarmDeviceId = table.Column<int>(nullable: false),
                    FireAlarmDetectorId = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    CheckState = table.Column<byte>(nullable: false),
                    CheckContent = table.Column<string>(maxLength: 300, nullable: true),
                    CheckVoiceUrl = table.Column<string>(maxLength: 100, nullable: true),
                    CheckVoiceLength = table.Column<int>(nullable: false),
                    CheckUserId = table.Column<int>(nullable: false),
                    CheckTime = table.Column<DateTime>(nullable: true),
                    NotifyWorker = table.Column<bool>(nullable: false),
                    NotifyMiniStation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmToFire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppType = table.Column<byte>(nullable: false),
                    AppPath = table.Column<string>(nullable: true),
                    VersionNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersion", x => x.Id);
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
                name: "BreakDown",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DoUserId = table.Column<int>(nullable: false),
                    Source = table.Column<int>(nullable: false),
                    HandleStatus = table.Column<int>(nullable: false),
                    DataId = table.Column<int>(nullable: false),
                    SolutionTime = table.Column<DateTime>(nullable: false),
                    SolutionWay = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    DispatchTime = table.Column<DateTime>(nullable: false),
                    SafeCompleteTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakDown", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToDuty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DutyPicture = table.Column<string>(maxLength: 100, nullable: true),
                    DutyStatus = table.Column<byte>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireUnitUserId = table.Column<int>(nullable: false),
                    DutyRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDuty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToDutyProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DutyId = table.Column<int>(nullable: false),
                    ProblemRemark = table.Column<string>(maxLength: 200, nullable: true),
                    VoiceLength = table.Column<int>(nullable: false),
                    ProblemRemarkType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDutyProblem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireUnitUserId = table.Column<int>(nullable: false),
                    PatrolStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolId = table.Column<int>(nullable: false),
                    DeviceSn = table.Column<string>(maxLength: 20, nullable: true),
                    PatrolStatus = table.Column<byte>(nullable: false),
                    PatrolType = table.Column<byte>(nullable: false),
                    PatrolAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetailFireSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolDetailId = table.Column<int>(nullable: false),
                    FireSystemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetailFireSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetailProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolDetailId = table.Column<int>(nullable: false),
                    ProblemRemark = table.Column<string>(maxLength: 500, nullable: true),
                    VoiceLength = table.Column<int>(nullable: false),
                    ProblemRemarkType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetailProblem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectorType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GBType = table.Column<byte>(nullable: false),
                    ApplyForTSJ = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentNo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    EquiNo = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    FireSystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentNo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fault",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireAlarmDeviceId = table.Column<int>(nullable: false),
                    FireAlarmDetectorId = table.Column<int>(nullable: false),
                    FaultRemark = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fault", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireAlarmDetector",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Identify = table.Column<string>(maxLength: 50, nullable: true),
                    DetectorTypeId = table.Column<int>(nullable: false),
                    FireAlarmDeviceId = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<int>(nullable: false),
                    FireUnitArchitectureFloorId = table.Column<int>(nullable: false),
                    FaultNum = table.Column<int>(nullable: false),
                    LastFaultId = table.Column<int>(nullable: false),
                    FullLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireAlarmDetector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireAlarmDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    DeviceModel = table.Column<string>(nullable: true),
                    DeviceSn = table.Column<string>(nullable: true),
                    NetComm = table.Column<string>(nullable: true),
                    FireUnitArchitectureId = table.Column<int>(nullable: false),
                    Protocol = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    NetDetectorNum = table.Column<int>(nullable: false),
                    EnableAlarmCloud = table.Column<bool>(nullable: false),
                    EnableAlarmSwitch = table.Column<bool>(nullable: false),
                    EnableFaultCloud = table.Column<bool>(nullable: false),
                    EnableFaultSwitch = table.Column<bool>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireAlarmDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireAlarmDeviceModel",
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
                    table.PrimaryKey("PK_FireAlarmDeviceModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireAlarmDeviceProtocol",
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
                    table.PrimaryKey("PK_FireAlarmDeviceProtocol", x => x.Id);
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
                name: "FireElectricDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    DeviceModel = table.Column<string>(maxLength: 20, nullable: true),
                    DeviceSn = table.Column<string>(maxLength: 20, nullable: true),
                    FireUnitArchitectureId = table.Column<int>(nullable: false),
                    FireUnitArchitectureFloorId = table.Column<int>(nullable: false),
                    NetComm = table.Column<string>(nullable: true),
                    DataRate = table.Column<string>(nullable: true),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<int>(nullable: false),
                    ExistAmpere = table.Column<bool>(nullable: false),
                    ExistTemperature = table.Column<bool>(nullable: false),
                    EnableEndAlarm = table.Column<bool>(nullable: false),
                    EnableCloudAlarm = table.Column<bool>(nullable: false),
                    EnableAlarmSwitch = table.Column<bool>(nullable: false),
                    MinAmpere = table.Column<int>(nullable: false),
                    MaxAmpere = table.Column<int>(nullable: false),
                    PhaseType = table.Column<int>(nullable: false),
                    MinL = table.Column<int>(nullable: false),
                    MaxL = table.Column<int>(nullable: false),
                    MinN = table.Column<int>(nullable: false),
                    MaxN = table.Column<int>(nullable: false),
                    MinL1 = table.Column<int>(nullable: false),
                    MaxL1 = table.Column<int>(nullable: false),
                    MinL2 = table.Column<int>(nullable: false),
                    MaxL2 = table.Column<int>(nullable: false),
                    MinL3 = table.Column<int>(nullable: false),
                    MaxL3 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireElectricDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireElectricDeviceModel",
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
                    table.PrimaryKey("PK_FireElectricDeviceModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireElectricRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireElectricDeviceId = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    Sign = table.Column<string>(nullable: true),
                    Analog = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireElectricRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireOrtherDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireSystemId = table.Column<int>(nullable: false),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    DeviceSn = table.Column<string>(nullable: true),
                    FireUnitArchitectureId = table.Column<int>(nullable: false),
                    FireUnitArchitectureFloorId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireOrtherDevice", x => x.Id);
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
                name: "FireSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitArchitecture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FireUnitUserId = table.Column<int>(nullable: false),
                    AboveNum = table.Column<int>(nullable: false),
                    BelowNum = table.Column<int>(nullable: false),
                    BuildYear = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    Outward_Picture = table.Column<string>(nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireDevice_LTJ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_LTJ_Detail = table.Column<string>(nullable: true),
                    FireDevice_HJ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_HJ_Detail = table.Column<string>(nullable: true),
                    FireDevice_MH_Exist = table.Column<bool>(nullable: false),
                    FireDevice_MH_Detail = table.Column<string>(nullable: true),
                    FireDevice_TFPY_Exist = table.Column<bool>(nullable: false),
                    FireDevice_TFPY_Detail = table.Column<string>(nullable: true),
                    FireDevice_XHS_Exist = table.Column<bool>(nullable: false),
                    FireDevice_XHS_Detail = table.Column<string>(nullable: true),
                    FireDevice_XFSY_Exist = table.Column<bool>(nullable: false),
                    FireDevice_XFSY_Detail = table.Column<string>(nullable: true),
                    FireDevice_FHM_Exist = table.Column<bool>(nullable: false),
                    FireDevice_FHM_Detail = table.Column<string>(nullable: true),
                    FireDevice_FHJL_Exist = table.Column<bool>(nullable: false),
                    FireDevice_FHJL_Detail = table.Column<string>(nullable: true),
                    FireDevice_MHQ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_MHQ_Detail = table.Column<string>(nullable: true),
                    FireDevice_YJZM_Exist = table.Column<bool>(nullable: false),
                    FireDevice_YJZM_Detail = table.Column<string>(nullable: true),
                    FireDevice_SSZS_Exist = table.Column<bool>(nullable: false),
                    FireDevice_SSZS_Detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitArchitecture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitArchitectureFloor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Floor_Picture = table.Column<string>(nullable: true),
                    ArchitectureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitArchitectureFloor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitAttention",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireDeptUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitAttention", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitPlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FirePlan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    QualificationNumber = table.Column<string>(nullable: true),
                    QualificationValidity = table.Column<DateTime>(nullable: false),
                    FireUnitInfoID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitUserRole",
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
                    table.PrimaryKey("PK_FireUnitUserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUntiSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireSystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUntiSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireWaterDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    DeviceAddress = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Gateway_Type = table.Column<string>(nullable: true),
                    Gateway_Sn = table.Column<string>(nullable: true),
                    Gateway_Location = table.Column<string>(nullable: true),
                    Gateway_NetComm = table.Column<string>(nullable: true),
                    Gateway_DataRate = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    MonitorType = table.Column<int>(nullable: false),
                    HeightThreshold = table.Column<string>(nullable: true),
                    PressThreshold = table.Column<string>(nullable: true),
                    EnableCloudAlarm = table.Column<bool>(nullable: false),
                    CurrentValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireWaterDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireWaterDeviceType",
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
                    table.PrimaryKey("PK_FireWaterDeviceType", x => x.Id);
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
                    Title = table.Column<string>(nullable: true),
                    ReadFlag = table.Column<bool>(nullable: false),
                    HandleStatus = table.Column<byte>(nullable: false),
                    ProblemRemarkType = table.Column<byte>(nullable: false),
                    ProblemRemark = table.Column<string>(maxLength: 200, nullable: true),
                    VoiceLength = table.Column<int>(nullable: false),
                    HandleUser = table.Column<string>(nullable: true),
                    SoultionTime = table.Column<DateTime>(nullable: false)
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
                name: "HydrantUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GuideFlage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HydrantUserArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountID = table.Column<int>(nullable: false),
                    AreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantUserArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MiniFireStationId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    MiniFireActionTypeId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    AttendUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireActionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MiniFireStationId = table.Column<int>(nullable: false),
                    DefineId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireEquipmentDefine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireEquipmentDefine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireStationJobUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MiniFireStationId = table.Column<int>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 20, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 11, nullable: true),
                    Job = table.Column<string>(maxLength: 10, nullable: true),
                    HeadPhotoBase64 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireStationJobUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotosPathSave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TableName = table.Column<string>(maxLength: 20, nullable: false),
                    DataId = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosPathSave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordOnline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DetectorId = table.Column<int>(nullable: false),
                    State = table.Column<sbyte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordOnline", x => x.Id);
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
                name: "SafeUnitUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SafeUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafeUnitUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SafeUnitUserFireUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SafeUnitUserId = table.Column<int>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafeUnitUserFireUnit", x => x.Id);
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
                    StreetGridUserId = table.Column<int>(nullable: false),
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
                name: "StreetGridUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    GridName = table.Column<string>(maxLength: 50, nullable: false),
                    Street = table.Column<string>(nullable: false),
                    Community = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGridUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suggest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    suggest = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggest", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "SupervisionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SupervisionId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionPhotos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmToElectric");

            migrationBuilder.DropTable(
                name: "AlarmToFire");

            migrationBuilder.DropTable(
                name: "AppVersion");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "BreakDown");

            migrationBuilder.DropTable(
                name: "DataToDuty");

            migrationBuilder.DropTable(
                name: "DataToDutyProblem");

            migrationBuilder.DropTable(
                name: "DataToPatrol");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetail");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetailFireSystem");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetailProblem");

            migrationBuilder.DropTable(
                name: "DetectorType");

            migrationBuilder.DropTable(
                name: "EquipmentNo");

            migrationBuilder.DropTable(
                name: "Fault");

            migrationBuilder.DropTable(
                name: "FireAlarmDetector");

            migrationBuilder.DropTable(
                name: "FireAlarmDevice");

            migrationBuilder.DropTable(
                name: "FireAlarmDeviceModel");

            migrationBuilder.DropTable(
                name: "FireAlarmDeviceProtocol");

            migrationBuilder.DropTable(
                name: "FireDept");

            migrationBuilder.DropTable(
                name: "FireDeptUser");

            migrationBuilder.DropTable(
                name: "FireElectricDevice");

            migrationBuilder.DropTable(
                name: "FireElectricDeviceModel");

            migrationBuilder.DropTable(
                name: "FireElectricRecord");

            migrationBuilder.DropTable(
                name: "FireOrtherDevice");

            migrationBuilder.DropTable(
                name: "FireSetting");

            migrationBuilder.DropTable(
                name: "FireSystem");

            migrationBuilder.DropTable(
                name: "FireUnitArchitecture");

            migrationBuilder.DropTable(
                name: "FireUnitArchitectureFloor");

            migrationBuilder.DropTable(
                name: "FireUnitAttention");

            migrationBuilder.DropTable(
                name: "FireUnitPlan");

            migrationBuilder.DropTable(
                name: "FireUnitType");

            migrationBuilder.DropTable(
                name: "FireUnitUser");

            migrationBuilder.DropTable(
                name: "FireUnitUserRole");

            migrationBuilder.DropTable(
                name: "FireUntiSystem");

            migrationBuilder.DropTable(
                name: "FireWaterDevice");

            migrationBuilder.DropTable(
                name: "FireWaterDeviceType");

            migrationBuilder.DropTable(
                name: "HydrantAlarm");

            migrationBuilder.DropTable(
                name: "HydrantPressure");

            migrationBuilder.DropTable(
                name: "HydrantUser");

            migrationBuilder.DropTable(
                name: "HydrantUserArea");

            migrationBuilder.DropTable(
                name: "MiniFireAction");

            migrationBuilder.DropTable(
                name: "MiniFireActionType");

            migrationBuilder.DropTable(
                name: "MiniFireEquipment");

            migrationBuilder.DropTable(
                name: "MiniFireEquipmentDefine");

            migrationBuilder.DropTable(
                name: "MiniFireStationJobUser");

            migrationBuilder.DropTable(
                name: "PhotosPathSave");

            migrationBuilder.DropTable(
                name: "RecordOnline");

            migrationBuilder.DropTable(
                name: "SafeUnit");

            migrationBuilder.DropTable(
                name: "SafeUnitUser");

            migrationBuilder.DropTable(
                name: "SafeUnitUserFireUnit");

            migrationBuilder.DropTable(
                name: "StreetGridEvent");

            migrationBuilder.DropTable(
                name: "StreetGridEventRemark");

            migrationBuilder.DropTable(
                name: "StreetGridUser");

            migrationBuilder.DropTable(
                name: "Suggest");

            migrationBuilder.DropTable(
                name: "Supervision");

            migrationBuilder.DropTable(
                name: "SupervisionDetail");

            migrationBuilder.DropTable(
                name: "SupervisionDetailRemark");

            migrationBuilder.DropTable(
                name: "SupervisionItem");

            migrationBuilder.DropTable(
                name: "SupervisionPhotos");
        }
    }
}
