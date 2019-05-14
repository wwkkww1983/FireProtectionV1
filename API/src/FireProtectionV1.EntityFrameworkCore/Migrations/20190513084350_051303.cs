using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _051303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "MiniFireStation",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "MiniFireStation",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "Hydrant",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "Hydrant",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "FireUnit",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "FireUnit",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DetectorType",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "MiniFireStation",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "MiniFireStation",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "Hydrant",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "Hydrant",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lng",
                table: "FireUnit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "FireUnit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DetectorType",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
