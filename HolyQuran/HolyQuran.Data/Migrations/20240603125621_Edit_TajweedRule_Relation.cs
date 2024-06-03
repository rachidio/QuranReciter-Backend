using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyQuran.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_TajweedRule_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_TajweedRules_TajweedRuleId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TajweedRuleId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "TajweedRuleId",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "TajweedRuleId",
                table: "SpecificNotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecificNotes_TajweedRuleId",
                table: "SpecificNotes",
                column: "TajweedRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificNotes_TajweedRules_TajweedRuleId",
                table: "SpecificNotes",
                column: "TajweedRuleId",
                principalTable: "TajweedRules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificNotes_TajweedRules_TajweedRuleId",
                table: "SpecificNotes");

            migrationBuilder.DropIndex(
                name: "IX_SpecificNotes_TajweedRuleId",
                table: "SpecificNotes");

            migrationBuilder.DropColumn(
                name: "TajweedRuleId",
                table: "SpecificNotes");

            migrationBuilder.AddColumn<int>(
                name: "TajweedRuleId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TajweedRuleId",
                table: "Notes",
                column: "TajweedRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_TajweedRules_TajweedRuleId",
                table: "Notes",
                column: "TajweedRuleId",
                principalTable: "TajweedRules",
                principalColumn: "Id");
        }
    }
}
