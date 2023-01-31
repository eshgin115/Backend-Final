using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_For_Mvc_Project.Migrations
{
    public partial class AboutComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutComponents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AboutComponents",
                columns: new[] { "Id", "Content" },
                values: new object[] { 1, "Define Content" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutComponents");
        }
    }
}
