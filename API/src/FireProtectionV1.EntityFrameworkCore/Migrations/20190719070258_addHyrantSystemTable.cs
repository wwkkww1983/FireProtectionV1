using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class addHyrantSystemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "State",
                table: "RecordOnline",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<byte>(
                name: "HandleStatus",
                table: "HydrantAlarm",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "HandleUser",
                table: "HydrantAlarm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemRemark",
                table: "HydrantAlarm",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ProblemRemarkType",
                table: "HydrantAlarm",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<bool>(
                name: "ReadFlag",
                table: "HydrantAlarm",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "HydrantUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Account = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GuideFlage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HydrantUserArea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountID = table.Column<int>(nullable: false),
                    AreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HydrantUserArea", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HydrantUser");

            migrationBuilder.DropTable(
                name: "HydrantUserArea");

            migrationBuilder.DropColumn(
                name: "HandleStatus",
                table: "HydrantAlarm");

            migrationBuilder.DropColumn(
                name: "HandleUser",
                table: "HydrantAlarm");

            migrationBuilder.DropColumn(
                name: "ProblemRemark",
                table: "HydrantAlarm");

            migrationBuilder.DropColumn(
                name: "ProblemRemarkType",
                table: "HydrantAlarm");

            migrationBuilder.DropColumn(
                name: "ReadFlag",
                table: "HydrantAlarm");

            migrationBuilder.AlterColumn<byte>(
                name: "State",
                table: "RecordOnline",
                nullable: false,
                oldClrType: typeof(sbyte));
        }
    }
}
