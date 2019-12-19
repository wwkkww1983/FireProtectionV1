using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _1211 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NetComm",
                table: "FireElectricDevice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NetComm",
                table: "FireAlarmDevice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetComm",
                table: "FireElectricDevice");

            migrationBuilder.DropColumn(
                name: "NetComm",
                table: "FireAlarmDevice");
        }
    }
}
