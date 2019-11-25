using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11251 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FireDeptContractName",
                table: "FireUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FireDeptContractPhone",
                table: "FireUnit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FireDeptId",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZP_Picture",
                table: "FireUnit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FireDeptContractName",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "FireDeptContractPhone",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "FireDeptId",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "ZP_Picture",
                table: "FireUnit");
        }
    }
}
