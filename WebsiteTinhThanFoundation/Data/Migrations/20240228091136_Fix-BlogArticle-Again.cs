using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteTinhThanFoundation.Migrations
{
    public partial class FixBlogArticleAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogImage",
                table: "BlogArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HagTags",
                table: "BlogArticles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogImage",
                table: "BlogArticles");

            migrationBuilder.DropColumn(
                name: "HagTags",
                table: "BlogArticles");
        }
    }
}
