using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2019112501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "FireUnitUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "FireUnitUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationNumber",
                table: "FireUnitUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QualificationValidity",
                table: "FireUnitUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "FireUnitUser");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "FireUnitUser");

            migrationBuilder.DropColumn(
                name: "QualificationNumber",
                table: "FireUnitUser");

            migrationBuilder.DropColumn(
                name: "QualificationValidity",
                table: "FireUnitUser");
        }
    }
}
