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
                name: "FK_Control_UserTasks_Control_Task_Control_TaskID",
                table: "Control_UserTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Control_UserTasks_Control_Task_Control_TaskID",
                table: "Control_UserTasks",
                column: "Control_TaskID",
                principalTable: "Control_Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Control_UserTasks_Control_Task_Control_TaskID",
                table: "Control_UserTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Control_UserTasks_Control_Task_Control_TaskID",
                table: "Control_UserTasks",
                column: "Control_TaskID",
                principalTable: "Control_Task",
                principalColumn: "Id");
        }
    }
}
