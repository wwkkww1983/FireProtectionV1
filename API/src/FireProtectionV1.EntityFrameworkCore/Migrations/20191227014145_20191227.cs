using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20191227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceType",
                table: "FireOrtherDevice",
                newName: "DeviceModel");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "FireOrtherDevice",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceModel",
                table: "FireOrtherDevice",
                newName: "DeviceType");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "FireOrtherDevice",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
