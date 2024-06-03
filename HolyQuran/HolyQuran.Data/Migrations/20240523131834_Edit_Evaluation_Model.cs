using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyQuran.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Evaluation_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "Evaluations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
