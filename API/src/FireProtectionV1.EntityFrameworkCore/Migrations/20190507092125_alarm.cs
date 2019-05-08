using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class alarm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GovernmentInfo");

            migrationBuilder.DropColumn(
                name: "TerminalDeviceSn",
                table: "AlarmToFire");

            migrationBuilder.RenameColumn(
                name: "CollectDeviceId",
                table: "AlarmToFire",
                newName: "DetectorId");

            migrationBuilder.RenameColumn(
                name: "CollectDeviceId",
                table: "AlarmToElectric",
                newName: "DetectorId");

            migrationBuilder.CreateTable(
                name: "ControllerElectric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sn = table.Column<string>(maxLength: 20, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    NetworkState = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerElectric", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControllerFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sn = table.Column<string>(maxLength: 20, nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    NetworkState = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerFire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectorElectric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    ControllerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorElectric", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectorFire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    ControllerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorFire", x => x.Id);
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
                    Contact = table.Column<string>(nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerElectric");

            migrationBuilder.DropTable(
                name: "ControllerFire");

            migrationBuilder.DropTable(
                name: "DetectorElectric");

            migrationBuilder.DropTable(
                name: "DetectorFire");

            migrationBuilder.DropTable(
                name: "MiniFireStation");

            migrationBuilder.RenameColumn(
                name: "DetectorId",
                table: "AlarmToFire",
                newName: "CollectDeviceId");

            migrationBuilder.RenameColumn(
                name: "DetectorId",
                table: "AlarmToElectric",
                newName: "CollectDeviceId");

            migrationBuilder.AddColumn<string>(
                name: "TerminalDeviceSn",
                table: "AlarmToFire",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GovernmentInfo",
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
                    table.PrimaryKey("PK_GovernmentInfo", x => x.Id);
                });
        }
    }
}
