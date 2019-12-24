using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2019122201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireAlarmDeviceModel");

            migrationBuilder.DropTable(
                name: "FireElectricDeviceModel");

            migrationBuilder.DropTable(
                name: "FireWaterDeviceType");

            migrationBuilder.DropColumn(
                name: "HeightThreshold",
                table: "FireWaterDevice");

            migrationBuilder.DropColumn(
                name: "PressThreshold",
                table: "FireWaterDevice");

            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "FireWaterDevice",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CurrentValue",
                table: "FireWaterDevice",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaxThreshold",
                table: "FireWaterDevice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinThreshold",
                table: "FireWaterDevice",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "FireWaterRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireWaterDeviceId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireWaterRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireWaterRecord");

            migrationBuilder.DropColumn(
                name: "MaxThreshold",
                table: "FireWaterDevice");

            migrationBuilder.DropColumn(
                name: "MinThreshold",
                table: "FireWaterDevice");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "FireWaterDevice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CurrentValue",
                table: "FireWaterDevice",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "HeightThreshold",
                table: "FireWaterDevice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PressThreshold",
                table: "FireWaterDevice",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FireAlarmDeviceModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireAlarmDeviceModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireElectricDeviceModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireElectricDeviceModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireWaterDeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireWaterDeviceType", x => x.Id);
                });
        }
    }
}
