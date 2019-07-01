using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0628 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vioce",
                table: "AlarmCheck",
                newName: "VioceUrl");

            migrationBuilder.RenameColumn(
                name: "Picturs",
                table: "AlarmCheck",
                newName: "PicturUrls");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AlarmCheck",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AlarmCheck");

            migrationBuilder.RenameColumn(
                name: "VioceUrl",
                table: "AlarmCheck",
                newName: "Vioce");

            migrationBuilder.RenameColumn(
                name: "PicturUrls",
                table: "AlarmCheck",
                newName: "Picturs");
        }
    }
}
