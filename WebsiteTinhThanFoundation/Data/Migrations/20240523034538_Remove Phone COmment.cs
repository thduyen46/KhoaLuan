using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteTinhThanFoundation.Migrations
{
    public partial class RemovePhoneCOmment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "BlogArticleComments");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "BlogArticleComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
