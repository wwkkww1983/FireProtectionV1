using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2019112503 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkerNum",
                table: "FireUnit",
                newName: "FiremanTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FiremanTest",
                table: "FireUnit",
                newName: "WorkerNum");
        }
    }
}
