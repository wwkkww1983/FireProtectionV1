using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20191221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataToDutyProblem");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "DutyPicture",
                table: "DataToDuty");

            migrationBuilder.DropColumn(
                name: "DutyRemark",
                table: "DataToDuty");

            migrationBuilder.DropColumn(
                name: "DutyStatus",
                table: "DataToDuty");

            migrationBuilder.DropColumn(
                name: "SafeCompleteTime",
                table: "BreakDown");

            migrationBuilder.RenameColumn(
                name: "FireUnitInfoID",
                table: "FireUnitUser",
                newName: "FireUnitID");

            migrationBuilder.RenameColumn(
                name: "FireUnitUserId",
                table: "DataToDuty",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "BreakDown",
                newName: "SolutionRemark");

            migrationBuilder.RenameColumn(
                name: "DoUserId",
                table: "BreakDown",
                newName: "VoiceLength");

            migrationBuilder.AddColumn<DateTime>(
                name: "SolutionTime",
                table: "Fault",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DataToDuty",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProblemRemark",
                table: "BreakDown",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemVoiceUrl",
                table: "BreakDown",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolutionTime",
                table: "Fault");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DataToDuty");

            migrationBuilder.DropColumn(
                name: "ProblemRemark",
                table: "BreakDown");

            migrationBuilder.DropColumn(
                name: "ProblemVoiceUrl",
                table: "BreakDown");

            migrationBuilder.RenameColumn(
                name: "FireUnitID",
                table: "FireUnitUser",
                newName: "FireUnitInfoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DataToDuty",
                newName: "FireUnitUserId");

            migrationBuilder.RenameColumn(
                name: "VoiceLength",
                table: "BreakDown",
                newName: "DoUserId");

            migrationBuilder.RenameColumn(
                name: "SolutionRemark",
                table: "BreakDown",
                newName: "Remark");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Fault",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DutyPicture",
                table: "DataToDuty",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DutyRemark",
                table: "DataToDuty",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "DutyStatus",
                table: "DataToDuty",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SafeCompleteTime",
                table: "BreakDown",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DataToDutyProblem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DutyId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProblemRemark = table.Column<string>(maxLength: 200, nullable: true),
                    ProblemRemarkType = table.Column<int>(nullable: false),
                    VoiceLength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataToDutyProblem", x => x.Id);
                });
        }
    }
}
