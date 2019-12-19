using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _1219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    LegalPerson = table.Column<string>(nullable: true),
                    LegalPersonPhone = table.Column<string>(nullable: true),
                    FiremanNum = table.Column<int>(nullable: false),
                    WorkerNum = table.Column<int>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    ContractName = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPhone = table.Column<string>(maxLength: 20, nullable: true),
                    InvitationCode = table.Column<string>(maxLength: 10, nullable: true),
                    SafeUnitId = table.Column<int>(nullable: false),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Patrol = table.Column<byte>(nullable: false),
                    FireDeptId = table.Column<int>(nullable: false),
                    FireDeptContractName = table.Column<string>(nullable: true),
                    FireDeptContractPhone = table.Column<string>(nullable: true),
                    ZP_Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hydrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sn = table.Column<string>(nullable: false),
                    AreaId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DumpEnergy = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hydrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiniFireStation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    FireUnitId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 20, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 11, nullable: true),
                    StationUserId = table.Column<int>(nullable: false),
                    PersonNum = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: true),
                    Lng = table.Column<decimal>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    PhotoBase64 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniFireStation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireUnit");

            migrationBuilder.DropTable(
                name: "Hydrant");

            migrationBuilder.DropTable(
                name: "MiniFireStation");
        }
    }
}
