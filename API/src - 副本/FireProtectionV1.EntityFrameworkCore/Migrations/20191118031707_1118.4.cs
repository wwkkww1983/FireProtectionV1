using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11184 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeadPhotoBase64",
                table: "MiniFireStationJobUser",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationUserId",
                table: "MiniFireStation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadPhotoBase64",
                table: "MiniFireStationJobUser");

            migrationBuilder.DropColumn(
                name: "StationUserId",
                table: "MiniFireStation");
        }
    }
}
