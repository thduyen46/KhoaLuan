using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteTinhThanFoundation.Migrations
{
    public partial class AddROle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a19e491a-02b5-4f8b-8aec-ac2becb77eca", null, "Staff", "Staff" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a19e491a-02b5-4f8b-8aec-ac2becb78eca", null, "Volunteer", "Volunteer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a19e491a-02b5-4f8b-8aec-ac2becb77eca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a19e491a-02b5-4f8b-8aec-ac2becb78eca");

        }
    }
}
