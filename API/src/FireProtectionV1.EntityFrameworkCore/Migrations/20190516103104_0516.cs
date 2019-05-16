using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0516 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "AlarmTime",
                table: "Detector");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Fault",
                newName: "DetectorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FireUnitType",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetectorId",
                table: "Fault",
                newName: "DeviceId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FireUnitType",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<byte>(
                name: "DeviceType",
                table: "Fault",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AlarmTime",
                table: "Detector",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
