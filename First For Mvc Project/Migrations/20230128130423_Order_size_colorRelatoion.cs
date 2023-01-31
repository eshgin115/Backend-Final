using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia.Migrations
{
    public partial class Order_size_colorRelatoion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ColorId",
                table: "OrderProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_SizeId",
                table: "OrderProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Colors_ColorId",
                table: "OrderProducts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Sizes_SizeId",
                table: "OrderProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Colors_ColorId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Sizes_SizeId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ColorId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_SizeId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "OrderProducts");
        }
    }
}
