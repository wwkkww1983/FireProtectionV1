using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11193 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "MiniFireEquipment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "MiniFireAction",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "MiniFireAction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "MiniFireAction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "MiniFireEquipment");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "MiniFireAction");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "MiniFireAction");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "MiniFireAction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
