using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0529 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlarmRemark",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "SafeRange",
                table: "AlarmToElectric");

            migrationBuilder.RenameColumn(
                name: "AlarmTitle",
                table: "AlarmToFire",
                newName: "DetectorType");

            migrationBuilder.RenameColumn(
                name: "CurrentData",
                table: "AlarmToElectric",
                newName: "Analog");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gateway",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Gateway",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sn",
                table: "Gateway",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Detector",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sn",
                table: "Detector",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlarmLocation",
                table: "AlarmToFire",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AlarmLimit",
                table: "AlarmToElectric",
                maxLength: 20,
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

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "AlarmToElectric",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Gateway");

            migrationBuilder.DropColumn(
                name: "Sn",
                table: "Gateway");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Detector");

            migrationBuilder.DropColumn(
                name: "Sn",
                table: "Detector");

            migrationBuilder.DropColumn(
                name: "AlarmLocation",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "AlarmLimit",
                table: "AlarmToElectric");

            migrationBuilder.DropColumn(
                name: "AlarmLocation",
                table: "AlarmToElectric");

            migrationBuilder.DropColumn(
                name: "DetectorType",
                table: "AlarmToElectric");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "AlarmToElectric");

            migrationBuilder.RenameColumn(
                name: "DetectorType",
                table: "AlarmToFire",
                newName: "AlarmTitle");

            migrationBuilder.RenameColumn(
                name: "Analog",
                table: "AlarmToElectric",
                newName: "CurrentData");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gateway",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlarmRemark",
                table: "AlarmToFire",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SafeRange",
                table: "AlarmToElectric",
                maxLength: 50,
                nullable: true);
        }
    }
}
