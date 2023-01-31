using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_For_Mvc_Project.Migrations
{
    public partial class Blog_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogNameInFileSystem",
                table: "BlogVideos",
                newName: "VideoNameInFileSystem");

            migrationBuilder.RenameColumn(
                name: "BlogName",
                table: "BlogVideos",
                newName: "VideoName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoNameInFileSystem",
                table: "BlogVideos",
                newName: "BlogNameInFileSystem");

            migrationBuilder.RenameColumn(
                name: "VideoName",
                table: "BlogVideos",
                newName: "BlogName");
        }
    }
}
