using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_For_Mvc_Project.Migrations
{
    public partial class Plant_subcategory_change_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Plants_PlantId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_PlantId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Subcategories");

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SubcategoryId",
                table: "Plants",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Subcategories_SubcategoryId",
                table: "Plants",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Subcategories_SubcategoryId",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_SubcategoryId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Plants");

            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Subcategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_PlantId",
                table: "Subcategories",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Plants_PlantId",
                table: "Subcategories",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
