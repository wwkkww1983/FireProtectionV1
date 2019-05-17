using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0517 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreetGrid");

            migrationBuilder.DropColumn(
                name: "Community",
                table: "StreetGridEvent");

            migrationBuilder.RenameColumn(
                name: "StreetGridId",
                table: "StreetGridEvent",
                newName: "StreetGridUserId");

            migrationBuilder.AddColumn<byte[]>(
                name: "AttentionFireUnitIds",
                table: "FireDeptUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StreetGridUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    GridName = table.Column<string>(maxLength: 50, nullable: false),
                    Street = table.Column<string>(nullable: false),
                    Community = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGridUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreetGridUser");

            migrationBuilder.DropColumn(
                name: "AttentionFireUnitIds",
                table: "FireDeptUser");

            migrationBuilder.RenameColumn(
                name: "StreetGridUserId",
                table: "StreetGridEvent",
                newName: "StreetGridId");

            migrationBuilder.AddColumn<string>(
                name: "Community",
                table: "StreetGridEvent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StreetGrid",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Street = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetGrid", x => x.Id);
                });
        }
    }
}
