using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class updatePhotosPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotosPath",
                table: "PhotosPathSave");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "PhotosPathSave",
                newName: "DataId");

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "PhotosPathSave",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "PhotosPathSave",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "PhotosPathSave");

            migrationBuilder.RenameColumn(
                name: "DataId",
                table: "PhotosPathSave",
                newName: "TableId");

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "PhotosPathSave",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "PhotosPath",
                table: "PhotosPathSave",
                nullable: false,
                defaultValue: "");
        }
    }
}
