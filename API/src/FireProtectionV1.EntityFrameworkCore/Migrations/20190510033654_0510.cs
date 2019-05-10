using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0510 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerElectric");

            migrationBuilder.DropTable(
                name: "ControllerFire");

            migrationBuilder.DropTable(
                name: "DetectorElectric");

            migrationBuilder.DropTable(
                name: "DetectorFire");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "SafeUnit",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "MiniFireStation",
                newName: "ContactName");

            migrationBuilder.AddColumn<string>(
                name: "InvitationCode",
                table: "SafeUnit",
                maxLength: 10,
                nullable: true);

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
                    FireUnitId = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detector");

            migrationBuilder.DropTable(
                name: "DetectorType");

            migrationBuilder.DropTable(
                name: "Fault");

            migrationBuilder.DropTable(
                name: "Gateway");

            migrationBuilder.DropColumn(
                name: "InvitationCode",
                table: "SafeUnit");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "SafeUnit",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "MiniFireStation",
                newName: "Contact");

            migrationBuilder.CreateTable(
                name: "ControllerElectric",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NetworkState = table.Column<string>(maxLength: 10, nullable: true),
                    Sn = table.Column<string>(maxLength: 20, nullable: true)
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
                    FireUnitId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NetworkState = table.Column<string>(maxLength: 10, nullable: true),
                    Sn = table.Column<string>(maxLength: 20, nullable: true)
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
                    ControllerId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
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
                    ControllerId = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorFire", x => x.Id);
                });
        }
    }
}
