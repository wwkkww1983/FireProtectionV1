using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0819 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Notify119",
                table: "AlarmCheck",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "NotifyMiniaturefire",
                table: "AlarmCheck",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "NotifyWorker",
                table: "AlarmCheck",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notify119",
                table: "AlarmCheck");

            migrationBuilder.DropColumn(
                name: "NotifyMiniaturefire",
                table: "AlarmCheck");

            migrationBuilder.DropColumn(
                name: "NotifyWorker",
                table: "AlarmCheck");
        }
    }
}
