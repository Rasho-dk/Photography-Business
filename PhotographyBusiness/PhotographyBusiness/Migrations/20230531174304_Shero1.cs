using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographyBusiness.Migrations
{
    /// <inheritdoc />
    public partial class Shero1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderPhotos");
        }
    }
}
