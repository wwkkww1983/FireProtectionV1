using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20191212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireWaterDevice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    DeviceAddress = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Gateway_Type = table.Column<string>(nullable: true),
                    Gateway_Sn = table.Column<string>(nullable: true),
                    Gateway_Location = table.Column<string>(nullable: true),
                    Gateway_NetComm = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    MonitorType = table.Column<int>(nullable: false),
                    HeightThreshold = table.Column<string>(nullable: true),
                    PressThreshold = table.Column<string>(nullable: true),
                    EnableCloudAlarm = table.Column<bool>(nullable: false),
                    CurrentValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireWaterDevice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireWaterDevice");
        }
    }
}
