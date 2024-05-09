using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_back.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "452c4c0a-3c46-49e1-8f73-8eed29b38c42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94b6244e-7aea-40c6-a845-79d4dca1e743");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af153748-7009-4698-af3b-448b17fc07fb");

            migrationBuilder.AlterColumn<string>(
                name: "UserTaskID",
                table: "Control_UserTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ControlID",
                table: "Control_Task",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

           

            migrationBuilder.AddForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task",
                column: "ControlID",
                principalTable: "Control",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks",
                column: "UserTaskID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "890e6dc1-41a0-4424-85dd-80ded0b06352");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5fa104f-23b1-491b-b02b-b1253f603538");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e927dc81-648e-44f9-bcef-dff54e978d05");

            migrationBuilder.AlterColumn<string>(
                name: "UserTaskID",
                table: "Control_UserTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ControlID",
                table: "Control_Task",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            

            migrationBuilder.AddForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task",
                column: "ControlID",
                principalTable: "Control",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks",
                column: "UserTaskID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
