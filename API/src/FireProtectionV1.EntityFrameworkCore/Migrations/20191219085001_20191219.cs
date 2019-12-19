using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20191219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CheckState",
                table: "AlarmToFire",
                nullable: false,
                oldClrType: typeof(byte));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "CheckState",
                table: "AlarmToFire",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
