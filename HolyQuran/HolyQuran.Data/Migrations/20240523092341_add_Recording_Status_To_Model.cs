using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolyQuran.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_Recording_Status_To_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordingStatus",
                table: "Recordings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordingStatus",
                table: "Recordings");
        }
    }
}
