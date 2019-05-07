using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class alarmall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceUnitInfo");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "FireUnit",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SafeUnitId",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AlarmToElectric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrentData = table.Column<decimal>(nullable: false),
                    SafeRange = table.Column<string>(maxLength: 50, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    CollectDeviceId = table.Column<int>(nullable: false)
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
                    AlarmTitle = table.Column<string>(maxLength: 50, nullable: false),
                    AlarmRemark = table.Column<string>(maxLength: 200, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    CollectDeviceId = table.Column<int>(nullable: false),
                    TerminalDeviceSn = table.Column<string>(maxLength: 10, nullable: false)
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
                name: "SafeUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafeUnit", x => x.Id);
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
                name: "SafeUnit");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "SafeUnitId",
                table: "FireUnit");

            migrationBuilder.CreateTable(
                name: "MaintenanceUnitInfo",
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
                    table.PrimaryKey("PK_MaintenanceUnitInfo", x => x.Id);
                });
        }
    }
}
