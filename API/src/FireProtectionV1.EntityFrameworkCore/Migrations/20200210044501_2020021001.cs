using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2020021001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "AlarmToElectric",
                newName: "IsFireUnitRead");

            migrationBuilder.AddColumn<bool>(
                name: "IsEngineerRead",
                table: "AlarmToElectric",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEngineerRead",
                table: "AlarmToElectric");

            migrationBuilder.RenameColumn(
                name: "IsFireUnitRead",
                table: "AlarmToElectric",
                newName: "IsRead");
        }
    }
}
