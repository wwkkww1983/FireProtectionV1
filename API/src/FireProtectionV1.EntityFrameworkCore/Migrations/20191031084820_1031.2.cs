using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _10312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VoiceLength",
                table: "HydrantAlarm",
                nullable: false,
                oldClrType: typeof(ushort));

            migrationBuilder.AlterColumn<int>(
                name: "VoiceLength",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                oldClrType: typeof(ushort));

            migrationBuilder.AlterColumn<int>(
                name: "VoiceLength",
                table: "DataToDutyProblem",
                nullable: false,
                oldClrType: typeof(ushort));

            migrationBuilder.AlterColumn<int>(
                name: "VoiceLength",
                table: "AlarmCheck",
                nullable: false,
                oldClrType: typeof(ushort));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ushort>(
                name: "VoiceLength",
                table: "HydrantAlarm",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<ushort>(
                name: "VoiceLength",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<ushort>(
                name: "VoiceLength",
                table: "DataToDutyProblem",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<ushort>(
                name: "VoiceLength",
                table: "AlarmCheck",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
