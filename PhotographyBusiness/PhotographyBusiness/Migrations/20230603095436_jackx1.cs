using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotographyBusiness.Migrations
{
    /// <inheritdoc />
    public partial class jackx1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPhotos_Order_OrderId",
                table: "OrderPhotos");

            migrationBuilder.DropIndex(
                name: "IX_OrderPhotos_OrderId",
                table: "OrderPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderPhotos");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BookingId",
                table: "Orders",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bookings_BookingId",
                table: "Orders",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bookings_BookingId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BookingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPhotos_OrderId",
                table: "OrderPhotos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPhotos_Order_OrderId",
                table: "OrderPhotos",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
