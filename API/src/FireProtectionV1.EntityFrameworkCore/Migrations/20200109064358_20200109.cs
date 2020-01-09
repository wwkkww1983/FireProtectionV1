using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20200109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToPatrolDetailFireSystem");

            migrationBuilder.DropTable(
                name: "DataToPatrolDetailProblem");

            migrationBuilder.DropColumn(
                name: "FaultRemark",
                table: "Fault");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "AlarmToFire",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "AlarmToElectric",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "AlarmToElectric",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "AlarmToFire");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "AlarmToElectric");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AlarmToElectric");

            migrationBuilder.AddColumn<string>(
                name: "FaultRemark",
                table: "Fault",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DataToPatrolDetailFireSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FireSystemID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PatrolDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetailFireSystem", x => x.Id);
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
                    ProblemRemark = table.Column<string>(maxLength: 500, nullable: true),
                    ProblemRemarkType = table.Column<byte>(nullable: false),
                    VoiceLength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToPatrolDetailProblem", x => x.Id);
                });
        }
    }
}
