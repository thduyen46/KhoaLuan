using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteTinhThanFoundation.Migrations
{
    public partial class FixWrongAddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a19e491a-02b5-4f8b-8aec-ac2becb88ecc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a19e491a-02b5-4f8b-8aec-ac2becb88eca", null, "Administrator", "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048", 0, "b19f1b24-5ac9-4c8d-9b7c-5e5d5f5cfb1e", "admin@tiemkiet.vn", true, "Admin", true, null, "admin@tiemkiet.vn", "admin", "AQAAAAEAACcQAAAAECAsUeOByw0jsD4x7X0K9WQdxWV/RrvPBnHITnRzdbrhHKzmf35BZDPXJBcVjp5FIQ==", "0923425148", true, "ZD5UZJQK6Q5W6N7O6RBRF6DB2Q2G2AIJ", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a19e491a-02b5-4f8b-8aec-ac2becb88eca", "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a19e491a-02b5-4f8b-8aec-ac2becb88eca", "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a19e491a-02b5-4f8b-8aec-ac2becb88eca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a3e8ce56-31af-4d35-b7fc-efc8a3d0c048");

        }
    }
}
