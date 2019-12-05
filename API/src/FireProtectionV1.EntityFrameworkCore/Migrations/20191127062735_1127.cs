using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _1127 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FireUnitArchitectureFloorId",
                table: "Detector",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GatewayId",
                table: "AlarmToFire",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GatewayId",
                table: "AlarmToElectric",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FireAlarmDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    GatewayId = table.Column<int>(nullable: false),
                    DeviceType = table.Column<string>(nullable: true),
                    DeviceSn = table.Column<string>(nullable: true),
                    FireUnitArchitectureId = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Protocol = table.Column<string>(nullable: true),
                    NetDetectorNum = table.Column<int>(nullable: false),
                    EnableAlarm = table.Column<bool>(nullable: false),
                    EnableFault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireAlarmDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireElectricDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GatewayId = table.Column<int>(nullable: false),
                    DeviceType = table.Column<string>(nullable: true),
                    DeviceSn = table.Column<string>(nullable: true),
                    FireUnitArchitectureId = table.Column<int>(nullable: false),
                    FireUnitArchitectureFloorId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    ExistTemperature = table.Column<bool>(nullable: false),
                    TemperatureThreshold = table.Column<string>(nullable: true),
                    ExistAmpere = table.Column<bool>(nullable: false),
                    AmpereThreshold = table.Column<string>(nullable: true),
                    EnableEndAlarm = table.Column<bool>(nullable: false),
                    EnableCloudAlarm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireElectricDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireOrtherDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireAlarmDevice");

            migrationBuilder.DropTable(
                name: "FireElectricDevice");

            migrationBuilder.DropTable(
                name: "FireOrtherDevice");

            migrationBuilder.DropColumn(
                name: "FireUnitArchitectureFloorId",
                table: "Detector");

            migrationBuilder.DropColumn(
                name: "GatewayId",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "GatewayId",
                table: "AlarmToElectric");
        }
    }
}
