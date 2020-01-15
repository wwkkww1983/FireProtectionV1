using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20200115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnableSMS",
                table: "FireElectricDevice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableAlarmSMS",
                table: "FireAlarmDevice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableFaultSMS",
                table: "FireAlarmDevice",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableSMS",
                table: "FireElectricDevice");

            migrationBuilder.DropColumn(
                name: "EnableAlarmSMS",
                table: "FireAlarmDevice");

            migrationBuilder.DropColumn(
                name: "EnableFaultSMS",
                table: "FireAlarmDevice");
        }
    }
}
