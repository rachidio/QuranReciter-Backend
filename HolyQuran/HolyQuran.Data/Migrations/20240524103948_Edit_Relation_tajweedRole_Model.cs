using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyQuran.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Relation_tajweedRole_Model : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "TajweedRuleId",
                table: "Notes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_TajweedRules_TajweedRuleId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TajweedRuleId",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "TajweedRuleId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TajweedRuleId",
                table: "Notes",
                column: "TajweedRuleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_TajweedRules_TajweedRuleId",
                table: "Notes",
                column: "TajweedRuleId",
                principalTable: "TajweedRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
