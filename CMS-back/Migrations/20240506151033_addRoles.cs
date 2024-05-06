using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class addRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f8cc8ad-e98e-48a7-ab19-56a24a6f9221", null, "HeadControl", "HeadControl" },
                    { "9cc35d10-a489-4d65-8524-30d2445f893c", null, "MemberControl", "MemberControl" },
                    { "e1482f1c-859c-43a7-986c-5701caae63c7", null, "AdminFaculty", "AdminFaculty" },
                    { "e4b85f19-14ac-470f-8759-9c96ad78d476", null, "AdminUniversity", "AdminUniversity" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f8cc8ad-e98e-48a7-ab19-56a24a6f9221");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cc35d10-a489-4d65-8524-30d2445f893c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1482f1c-859c-43a7-986c-5701caae63c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4b85f19-14ac-470f-8759-9c96ad78d476");
        }
    }
}
