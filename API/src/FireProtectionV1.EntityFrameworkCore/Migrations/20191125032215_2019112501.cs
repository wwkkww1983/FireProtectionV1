using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _2019112501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireUnitArchitecture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AboveNum = table.Column<int>(nullable: false),
                    BelowNum = table.Column<int>(nullable: false),
                    BuildYear = table.Column<int>(nullable: false),
                    Area = table.Column<decimal>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    Outward_Picture = table.Column<string>(nullable: true),
                    FireUnitId = table.Column<int>(nullable: false),
                    FireDevice_LTJ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_LTJ_Detail = table.Column<string>(nullable: true),
                    FireDevice_HJ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_HJ_Detail = table.Column<string>(nullable: true),
                    FireDevice_MH_Exist = table.Column<bool>(nullable: false),
                    FireDevice_MH_Detail = table.Column<string>(nullable: true),
                    FireDevice_TFPY_Exist = table.Column<bool>(nullable: false),
                    FireDevice_TFPY_Detail = table.Column<string>(nullable: true),
                    FireDevice_XHS_Exist = table.Column<bool>(nullable: false),
                    FireDevice_XHS_Detail = table.Column<string>(nullable: true),
                    FireDevice_XFSY_Exist = table.Column<bool>(nullable: false),
                    FireDevice_XFSY_Detail = table.Column<string>(nullable: true),
                    FireDevice_FHM_Exist = table.Column<bool>(nullable: false),
                    FireDevice_FHM_Detail = table.Column<string>(nullable: true),
                    FireDevice_FHJL_Exist = table.Column<bool>(nullable: false),
                    FireDevice_FHJL_Detail = table.Column<string>(nullable: true),
                    FireDevice_MHQ_Exist = table.Column<bool>(nullable: false),
                    FireDevice_MHQ_Detail = table.Column<string>(nullable: true),
                    FireDevice_YJZM_Exist = table.Column<bool>(nullable: false),
                    FireDevice_YJZM_Detail = table.Column<string>(nullable: true),
                    FireDevice_SSZS_Exist = table.Column<bool>(nullable: false),
                    FireDevice_SSZS_Detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitArchitecture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireUnitArchitectureFloor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Floor_Picture = table.Column<string>(nullable: true),
                    ArchitectureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnitArchitectureFloor", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireUnitArchitecture");

            migrationBuilder.DropTable(
                name: "FireUnitArchitectureFloor");
        }
    }
}
