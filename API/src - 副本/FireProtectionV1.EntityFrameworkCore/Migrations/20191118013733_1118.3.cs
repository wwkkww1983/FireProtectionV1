using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11183 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniFireStationJobUsers",
                table: "MiniFireStationJobUsers");

            migrationBuilder.RenameTable(
                name: "MiniFireStationJobUsers",
                newName: "MiniFireStationJobUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniFireStationJobUser",
                table: "MiniFireStationJobUser",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniFireStationJobUser",
                table: "MiniFireStationJobUser");

            migrationBuilder.RenameTable(
                name: "MiniFireStationJobUser",
                newName: "MiniFireStationJobUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniFireStationJobUsers",
                table: "MiniFireStationJobUsers",
                column: "Id");
        }
    }
}
