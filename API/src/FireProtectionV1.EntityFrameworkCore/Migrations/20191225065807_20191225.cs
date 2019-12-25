using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20191225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceSn",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "PatrolType",
                table: "DataToPatrolDetail");

            migrationBuilder.RenameColumn(
                name: "FireUnitUserId",
                table: "DataToPatrol",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Patrol",
                table: "FireUnit",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "PatrolStatus",
                table: "DataToPatrolDetail",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<int>(
                name: "ArchitectureId",
                table: "DataToPatrolDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "DataToPatrolDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "DataToPatrolDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PatrolStatus",
                table: "DataToPatrol",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<int>(
                name: "PatrolType",
                table: "DataToPatrol",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserBelongUnitId",
                table: "DataToPatrol",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchitectureId",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "DataToPatrolDetail");

            migrationBuilder.DropColumn(
                name: "PatrolType",
                table: "DataToPatrol");

            migrationBuilder.DropColumn(
                name: "UserBelongUnitId",
                table: "DataToPatrol");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DataToPatrol",
                newName: "FireUnitUserId");

            migrationBuilder.AlterColumn<byte>(
                name: "Patrol",
                table: "FireUnit",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "PatrolStatus",
                table: "DataToPatrolDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "DeviceSn",
                table: "DataToPatrolDetail",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "PatrolType",
                table: "DataToPatrolDetail",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<byte>(
                name: "PatrolStatus",
                table: "DataToPatrol",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
