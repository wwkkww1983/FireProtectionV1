using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2019122202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gateway_Type",
                table: "FireWaterDevice",
                newName: "Gateway_Model");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gateway_Model",
                table: "FireWaterDevice",
                newName: "Gateway_Type");
        }
    }
}
