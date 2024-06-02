using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class nasser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44827753-a18b-46cb-9823-a3d42dab2ff8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fa306af-1675-417d-ae68-3b0598d1b65b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdec3b41-a936-4b5c-b54e-ed3f6a54c8df");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d14c421-38ad-4fa6-a8f9-b800dd0a607b", null, "Staff", "Staff" },
                    { "5ea1ba4f-93f3-4c1c-b922-d09f2aad3309", null, "AdminUniversity", "AdminUniversity" },
                    { "c3ca1e36-5d2b-4105-ba71-4f1c7584102f", null, "AdminFaculty", "AdminFaculty" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d14c421-38ad-4fa6-a8f9-b800dd0a607b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ea1ba4f-93f3-4c1c-b922-d09f2aad3309");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3ca1e36-5d2b-4105-ba71-4f1c7584102f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44827753-a18b-46cb-9823-a3d42dab2ff8", null, "AdminFaculty", "AdminFaculty" },
                    { "9fa306af-1675-417d-ae68-3b0598d1b65b", null, "AdminUniversity", "AdminUniversity" },
                    { "bdec3b41-a936-4b5c-b54e-ed3f6a54c8df", null, "Staff", "Staff" }
                });
        }
    }
}
