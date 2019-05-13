using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0513 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataToDuty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DutyPicture = table.Column<string>(maxLength: 100, nullable: false),
                    DutyStatus = table.Column<byte>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireUnitUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDuty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToDutyProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DutyId = table.Column<int>(nullable: false),
                    ProblemVoice = table.Column<string>(maxLength: 100, nullable: true),
                    ProblemPicture = table.Column<string>(maxLength: 100, nullable: true),
                    ProblemRemark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDutyProblem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToDuty");

            migrationBuilder.DropTable(
                name: "DataToDutyProblem");
        }
    }
}
