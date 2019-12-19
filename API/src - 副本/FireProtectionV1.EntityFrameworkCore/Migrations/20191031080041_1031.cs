using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _1031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProblemPicture",
                table: "DataToPatrolDetailProblem");

            migrationBuilder.DropColumn(
                name: "ProblemVoice",
                table: "DataToPatrolDetailProblem");

            migrationBuilder.DropColumn(
                name: "ProblemPicture",
                table: "DataToDutyProblem");

            migrationBuilder.DropColumn(
                name: "ProblemVoice",
                table: "DataToDutyProblem");

            migrationBuilder.AddColumn<ushort>(
                name: "VoiceLength",
                table: "HydrantAlarm",
                nullable: false,
                defaultValue: (ushort)0);

            migrationBuilder.AlterColumn<byte>(
                name: "ProblemRemarkType",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<ushort>(
                name: "VoiceLength",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                defaultValue: (ushort)0);

            migrationBuilder.AddColumn<ushort>(
                name: "VoiceLength",
                table: "DataToDutyProblem",
                nullable: false,
                defaultValue: (ushort)0);

            migrationBuilder.AddColumn<ushort>(
                name: "VoiceLength",
                table: "AlarmCheck",
                nullable: false,
                defaultValue: (ushort)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoiceLength",
                table: "HydrantAlarm");

            migrationBuilder.DropColumn(
                name: "VoiceLength",
                table: "DataToPatrolDetailProblem");

            migrationBuilder.DropColumn(
                name: "VoiceLength",
                table: "DataToDutyProblem");

            migrationBuilder.DropColumn(
                name: "VoiceLength",
                table: "AlarmCheck");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemRemarkType",
                table: "DataToPatrolDetailProblem",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<string>(
                name: "ProblemPicture",
                table: "DataToPatrolDetailProblem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemVoice",
                table: "DataToPatrolDetailProblem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemPicture",
                table: "DataToDutyProblem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProblemVoice",
                table: "DataToDutyProblem",
                maxLength: 100,
                nullable: true);
        }
    }
}
