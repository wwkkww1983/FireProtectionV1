using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class addtableguidetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GuideFlage",
                table: "FireUnitUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Patrol",
                table: "FireUnit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FireSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUntiSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireSystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUntiSystem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireSystem");

            migrationBuilder.DropTable(
                name: "FireUntiSystem");

            migrationBuilder.DropColumn(
                name: "GuideFlage",
                table: "FireUnitUser");

            migrationBuilder.DropColumn(
                name: "Patrol",
                table: "FireUnit");
        }
    }
}
