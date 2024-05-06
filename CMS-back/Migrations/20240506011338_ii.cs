using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class ii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlUsers_Control_ControlID",
                table: "ControlUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ControlID",
                table: "ControlUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlUsers_Control_ControlID",
                table: "ControlUsers",
                column: "ControlID",
                principalTable: "Control",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlUsers_Control_ControlID",
                table: "ControlUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ControlID",
                table: "ControlUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlUsers_Control_ControlID",
                table: "ControlUsers",
                column: "ControlID",
                principalTable: "Control",
                principalColumn: "Id");
        }
    }
}
