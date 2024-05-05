using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_FaculityHierarycal_FaculityHierarycalID",
                table: "Subject");

            migrationBuilder.AlterColumn<string>(
                name: "FaculityHierarycalID",
                table: "Subject",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_FaculityHierarycal_FaculityHierarycalID",
                table: "Subject",
                column: "FaculityHierarycalID",
                principalTable: "FaculityHierarycal",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_FaculityHierarycal_FaculityHierarycalID",
                table: "Subject");

            migrationBuilder.AlterColumn<string>(
                name: "FaculityHierarycalID",
                table: "Subject",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_FaculityHierarycal_FaculityHierarycalID",
                table: "Subject",
                column: "FaculityHierarycalID",
                principalTable: "FaculityHierarycal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
