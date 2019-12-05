using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _12042 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phase",
                table: "FireElectricDevice",
                newName: "PhaseType");

            migrationBuilder.AddColumn<string>(
                name: "PhaseJson",
                table: "FireElectricDevice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhaseJson",
                table: "FireElectricDevice");

            migrationBuilder.RenameColumn(
                name: "PhaseType",
                table: "FireElectricDevice",
                newName: "Phase");
        }
    }
}
