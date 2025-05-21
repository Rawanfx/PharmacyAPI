using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Order_orderId",
                table: "ProductOrders");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "ProductOrders",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "ProductOrders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "ProductOrders",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProductOrders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_orderId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_OrderId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Order_OrderId",
                table: "ProductOrders",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Order_OrderId",
                table: "ProductOrders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ProductOrders",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ProductOrders",
                newName: "orderId");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ProductOrders",
                newName: "count");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductOrders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_orderId");

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "ProductOrders",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Order_orderId",
                table: "ProductOrders",
                column: "orderId",
                principalTable: "Order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
