using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteTinhThanFoundation.Migrations
{
    public partial class FixAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048",
                columns: new[] { "Email", "NormalizedEmail" },
                values: new object[] { "admin@locallhost.vn", "admin@locallhost.vn" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048",
                columns: new[] { "Email", "NormalizedEmail" },
                values: new object[] { "admin@tiemkiet.vn", "admin@tiemkiet.vn" });
        }
    }
}
