using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20200401 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FireAlarmSource",
                table: "AlarmToFire",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IndependentDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DetectorSn = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    PowerNum = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    EnableAlarmSMS = table.Column<bool>(nullable: false),
                    EnableFaultSMS = table.Column<bool>(nullable: false),
                    SMSPhones = table.Column<string>(nullable: true),
                    LastFaultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndependentDevice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndependentDevice");

            migrationBuilder.DropColumn(
                name: "FireAlarmSource",
                table: "AlarmToFire");
        }
    }
}
