using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _053101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FireUnitAccountRole",
                table: "FireUnitAccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FireUnitAccount",
                table: "FireUnitAccount");

            migrationBuilder.RenameTable(
                name: "FireUnitAccountRole",
                newName: "FireUnitUserRole");

            migrationBuilder.RenameTable(
                name: "FireUnitAccount",
                newName: "FireUnitUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FireUnitUserRole",
                table: "FireUnitUserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FireUnitUser",
                table: "FireUnitUser",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DataToPatrol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireUnitUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolId = table.Column<int>(nullable: false),
                    DeviceSn = table.Column<string>(maxLength: 20, nullable: true),
                    PatrolStatus = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetailProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolDetailId = table.Column<int>(nullable: false),
                    ProblemVoice = table.Column<string>(maxLength: 100, nullable: true),
                    ProblemPicture = table.Column<string>(maxLength: 100, nullable: true),
                    ProblemRemark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetailProblem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToPatrol");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetail");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetailProblem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FireUnitUserRole",
                table: "FireUnitUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FireUnitUser",
                table: "FireUnitUser");

            migrationBuilder.RenameTable(
                name: "FireUnitUserRole",
                newName: "FireUnitAccountRole");

            migrationBuilder.RenameTable(
                name: "FireUnitUser",
                newName: "FireUnitAccount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FireUnitAccountRole",
                table: "FireUnitAccountRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FireUnitAccount",
                table: "FireUnitAccount",
                column: "Id");
        }
    }
}
