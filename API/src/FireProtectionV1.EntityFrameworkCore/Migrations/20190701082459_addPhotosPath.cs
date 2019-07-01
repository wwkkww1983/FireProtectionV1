using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class addPhotosPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToDutyPhotos");

            migrationBuilder.DropTable(
                name: "DataToDutyProblemPhotos");

            migrationBuilder.AddColumn<int>(
                name: "ProblemRemarkType",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatrolAddress",
                table: "DataToPatrolDetail",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "PatrolType",
                table: "DataToPatrolDetail",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "DutyRemark",
                table: "DataToDuty",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhotosPathSave",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TableName = table.Column<string>(nullable: false),
                    TableId = table.Column<int>(nullable: false),
                    PhotosPath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosPathSave", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotosPathSave");

            migrationBuilder.DropColumn(
                name: "ProblemRemarkType",
                table: "DataToPatrolDetailProblem");

            migrationBuilder.DropColumn(
                name: "PatrolAddress",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "PatrolType",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "DutyRemark",
                table: "DataToDuty");

            migrationBuilder.CreateTable(
                name: "DataToDutyPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DutyId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PhotosPath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDutyPhotos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataToDutyProblemPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DutyProblemId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PhotosPath = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDutyProblemPhotos", x => x.Id);
                });
        }
    }
}
