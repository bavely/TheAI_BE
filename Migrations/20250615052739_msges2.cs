using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace THEAI_BE.Migrations
{
    /// <inheritdoc />
    public partial class msges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThreadId",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
