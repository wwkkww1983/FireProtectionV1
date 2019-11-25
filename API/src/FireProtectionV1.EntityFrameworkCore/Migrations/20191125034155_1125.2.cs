using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FiremanNum",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FiremanTest",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LegalPerson",
                table: "FireUnit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalPersonPhone",
                table: "FireUnit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerNum",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiremanNum",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "FiremanTest",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "LegalPerson",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "LegalPersonPhone",
                table: "FireUnit");

            migrationBuilder.DropColumn(
                name: "WorkerNum",
                table: "FireUnit");
        }
    }
}
