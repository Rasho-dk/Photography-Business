using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographyBusiness.Migrations
{
    /// <inheritdoc />
    public partial class jackx2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderPhotos_OrderId",
                table: "OrderPhotos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPhotos_Orders_OrderId",
                table: "OrderPhotos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPhotos_Orders_OrderId",
                table: "OrderPhotos");

            migrationBuilder.DropIndex(
                name: "IX_OrderPhotos_OrderId",
                table: "OrderPhotos");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderPhotos");
        }
    }
}
