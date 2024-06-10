using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_Back.Data.Migrations
{
    /// <inheritdoc />
    public partial class _22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Faculity_Node_Faculity_FaculityNodeID",
            //    table: "Faculity_Node");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "1c490e48-07ab-492a-8287-c5a831b61560");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "39c86721-71a4-4c6a-a952-2baf73f3b424");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "b755e362-7c9e-44f4-a1fa-4d97e1c3d6bc");


            //migrationBuilder.RenameColumn(
            //    name: "FaculityNodeID",
            //    table: "Faculity_Node",
            //    newName: "FaculityID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Faculity_Node_FaculityNodeID",
            //    table: "Faculity_Node",
            //    newName: "IX_Faculity_Node_FaculityID");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "adad47be-7ba4-46ad-a984-a2bf9864578e", null, "AdminFaculity", "AdminFaculity" },
            //        { "c0ff1e9c-f2f9-4020-8fb6-ab0f8321b2fe", null, "AdminUniversity", "AdminUniversity" },
            //        { "dc037ece-22af-4be4-af37-d54bc5b34013", null, "Staff", "Staff" }
            //    });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Faculity_Node_Faculity_FaculityID",
            //    table: "Faculity_Node",
            //    column: "FaculityID",
            //    principalTable: "Faculity",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //   name: "FK_Faculity_Phase_FaculityID",
            //   table: "Faculity_Phase",
            //   column: "FaculityID",
            //   principalTable: "Faculity",
            //   principalColumn: "Id",
            //   onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
                name: "FK_Control_Note_ControlID",
                table: "Control_Note",
                column: "ControlID",
                principalTable: "Control",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Faculity_Node_Faculity_FaculityID",
            //    table: "Faculity_Node");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "adad47be-7ba4-46ad-a984-a2bf9864578e");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "c0ff1e9c-f2f9-4020-8fb6-ab0f8321b2fe");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "dc037ece-22af-4be4-af37-d54bc5b34013");


            //migrationBuilder.DropColumn(
            //    name: "IsDone",
            //    table: "Control_UserTasks");

            //migrationBuilder.RenameColumn(
            //    name: "FaculityID",
            //    table: "Faculity_Node",
            //    newName: "FaculityNodeID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Faculity_Node_FaculityID",
            //    table: "Faculity_Node",
            //    newName: "IX_Faculity_Node_FaculityNodeID");


            //migrationBuilder.AddForeignKey(
            //    name: "FK_Faculity_Node_Faculity_FaculityNodeID",
            //    table: "Faculity_Node",
            //    column: "FaculityNodeID",
            //    principalTable: "Faculity",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
