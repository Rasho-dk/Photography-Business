using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographyBusiness.Migrations
{
    /// <inheritdoc />
    public partial class Condition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Condition",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Orders");
        }
    }
}
