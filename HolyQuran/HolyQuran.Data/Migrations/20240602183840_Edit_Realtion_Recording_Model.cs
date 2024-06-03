using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyQuran.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Realtion_Recording_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_EvaluationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_RecordingId",
                table: "Evaluations");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_EvaluationId",
                table: "Notes",
                column: "EvaluationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_RecordingId",
                table: "Evaluations",
                column: "RecordingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_EvaluationId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_RecordingId",
                table: "Evaluations");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_EvaluationId",
                table: "Notes",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_RecordingId",
                table: "Evaluations",
                column: "RecordingId");
        }
    }
}
