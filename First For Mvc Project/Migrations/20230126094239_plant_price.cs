using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia.Migrations
{
    public partial class plant_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Plants_BookId",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BasketProducts",
                newName: "PlantId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BookId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_PlantId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Plants",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Plants_PlantId",
                table: "BasketProducts",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Plants_PlantId",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "PlantId",
                table: "BasketProducts",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_PlantId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Plants_BookId",
                table: "BasketProducts",
                column: "BookId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
