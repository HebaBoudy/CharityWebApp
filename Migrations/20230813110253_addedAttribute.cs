using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppTutorial.Migrations
{
    /// <inheritdoc />
    public partial class addedAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Campaign",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Campaign");
        }
    }
}
