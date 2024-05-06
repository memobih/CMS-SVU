using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "JobType",
                table: "ControlUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22873f68-65e6-4c5f-bd6f-07e31b8d3c01", null, "AdminUniversity", "AdminUniversity" },
                    { "3fb511f7-0d87-4607-81d3-cb3e7128a614", null, "Staff", "Staff" },
                    { "5b99ba84-59ce-456c-a0d2-8ac0cb86f979", null, "AdminFaculty", "AdminFaculty" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22873f68-65e6-4c5f-bd6f-07e31b8d3c01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fb511f7-0d87-4607-81d3-cb3e7128a614");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b99ba84-59ce-456c-a0d2-8ac0cb86f979");

            migrationBuilder.AlterColumn<int>(
                name: "JobType",
                table: "ControlUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
