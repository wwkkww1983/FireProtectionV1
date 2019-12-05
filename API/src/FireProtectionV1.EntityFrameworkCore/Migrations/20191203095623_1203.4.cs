using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _12034 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnableAlarmSwitch",
                table: "FireAlarmDevice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableFaultSwitch",
                table: "FireAlarmDevice",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnableAlarmSwitch",
                table: "FireAlarmDevice");

            migrationBuilder.DropColumn(
                name: "EnableFaultSwitch",
                table: "FireAlarmDevice");
        }
    }
}
