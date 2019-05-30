using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0530 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sn",
                table: "Gateway");

            migrationBuilder.DropColumn(
                name: "AlarmLocation",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "DetectorType",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "AlarmLocation",
                table: "AlarmToElectric");

            migrationBuilder.DropColumn(
                name: "DetectorType",
                table: "AlarmToElectric");

            migrationBuilder.RenameColumn(
                name: "Sn",
                table: "Detector",
                newName: "Identify");

            migrationBuilder.AddColumn<byte>(
                name: "FireSysType",
                table: "Gateway",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Identify",
                table: "Gateway",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FireSysType",
                table: "Gateway");

            migrationBuilder.DropColumn(
                name: "Identify",
                table: "Gateway");

            migrationBuilder.RenameColumn(
                name: "Identify",
                table: "Detector",
                newName: "Sn");

            migrationBuilder.AddColumn<string>(
                name: "Sn",
                table: "Gateway",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlarmLocation",
                table: "AlarmToFire",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DetectorType",
                table: "AlarmToFire",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AlarmLocation",
                table: "AlarmToElectric",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DetectorType",
                table: "AlarmToElectric",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
