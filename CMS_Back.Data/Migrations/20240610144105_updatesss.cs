using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_Back.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BYLAW_Faculity_FaculityID",
                table: "BYLAW");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_conrol_Addresse_Control_ControlID",
            //    table: "Control_Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlUsers_AspNetUsers_UserID",
                table: "ControlUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_SemesterSubjects_StudentSemester_StudentSemesterID",
                table: "Student_SemesterSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_SemesterSubjects_Subject_SubjectID",
                table: "Student_SemesterSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Committee_Committees_CommitteeID",
                table: "Subject_Committee");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Committee_Subject_SubjectID",
                table: "Subject_Committee");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssesse_Assess_AssessID",
                table: "SubjectAssesse");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssesse_Subject_SubjectID",
                table: "SubjectAssesse");

            migrationBuilder.DropTable(
                name: "ControlSubject");


            migrationBuilder.DropIndex(
                name: "IX_Student_SemesterSubjects_SubjectID",
                table: "Student_SemesterSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ControlUsers_UserID",
                table: "ControlUsers");


            migrationBuilder.DropIndex(
                name: "IX_Control_UserTasks_UserTaskID",
                table: "Control_UserTasks");


            migrationBuilder.DropIndex(
                name: "IX_SubjectAssesse_SubjectID",
                table: "SubjectAssesse");


            migrationBuilder.DropIndex(
                name: "IX_Subject_Committee_SubjectID",
                table: "Subject_Committee");


            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3639bbbf-4345-4375-a788-e67c6bab0fba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a1ff073-6872-455c-98c5-a7c948f19c2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c46af3f-cfbd-46f7-8b34-789456dfb7c6");

            migrationBuilder.RenameTable(
                name: "SubjectAssesse",
                newName: "SubjectAssess");

            migrationBuilder.RenameTable(
                name: "Subject_Committee",
                newName: "Subject_Committees");

            //migrationBuilder.RenameTable(
            //    name: "Control_Address",
            //    newName: "Control_Address");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssesse_AssessID",
                table: "SubjectAssess",
                newName: "IX_SubjectAssess_AssessID");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_Committee_CommitteeID",
                table: "Subject_Committees",
                newName: "IX_Subject_Committees_CommitteeID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_conrol_Addresse_ControlID",
            //    table: "Control_Address",
            //    newName: "IX_Control_Address_ControlID");

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "Student_SemesterSubjects",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(200)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "StudentSemesterID",
            //    table: "Student_SemesterSubjects",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserID",
            //    table: "ControlUsers",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserTaskID",
            //    table: "Control_UserTasks",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Control_TaskID",
            //    table: "Control_UserTasks",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreationDate",
                table: "Control_Task",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ControlID",
                table: "Control_Task",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "WriteDate",
                table: "Control_Note",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Start_Date",
                table: "Control",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "End_Date",
                table: "Control",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "FaculityID",
            //    table: "BYLAW",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "SubjectAssess",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "AssessID",
            //    table: "SubjectAssess",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "Subject_Committees",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "CommitteeID",
            //    table: "Subject_Committees",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Address",
            //    table: "Control_Address",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Student_SemesterSubjects",
            //    table: "Student_SemesterSubjects",
            //    columns: new[] { "SubjectID", "StudentSemesterID" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_ControlUsers",
            //    table: "ControlUsers",
            //    columns: new[] { "UserID", "ControlID" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Control_UserTasks",
            //    table: "Control_UserTasks",
            //    columns: new[] { "UserTaskID", "Control_TaskID" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_SubjectAssess",
            //    table: "SubjectAssess",
            //    columns: new[] { "SubjectID", "AssessID" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Subject_Committees",
            //    table: "Subject_Committees",
            //    columns: new[] { "SubjectID", "CommitteeID" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Control_Address",
            //    table: "Control_Address",
            //    columns: new[] { "Address", "ControlID" });

            //migrationBuilder.CreateTable(
            //    name: "ControlSubjects",
            //    columns: table => new
            //    {
            //        ControlID = table.Column<string>(type: "nvarchar(200)", nullable: false),
            //        SubjectID = table.Column<string>(type: "nvarchar(200)", nullable: false),
            //        IsDone = table.Column<int>(type: "int", nullable: true),
            //        IsReview = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ControlSubjects", x => new { x.SubjectID, x.ControlID });
            //        table.ForeignKey(
            //            name: "FK_ControlSubjects_Control_ControlID",
            //            column: x => x.ControlID,
            //            principalTable: "Control",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ControlSubjects_Subject_SubjectID",
            //            column: x => x.SubjectID,
            //            principalTable: "Subject",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "0cef7fe4-0365-44ac-a22d-41fcecced6b6", null, "AdminFaculity", "AdminFaculity" },
            //        { "6444a70d-13a5-4bf3-a65d-6b98281d3ff0", null, "Staff", "Staff" },
            //        { "c964ad85-2b2d-41c3-a191-0406c6363435", null, "AdminUniversity", "AdminUniversity" }
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ControlSubjects_ControlID",
            //    table: "ControlSubjects",
            //    column: "ControlID");

            migrationBuilder.AddForeignKey(
                name: "FK_BYLAW_Faculity_FaculityID",
                table: "BYLAW",
                column: "FaculityID",
                principalTable: "Faculity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Control_Address_Control_ControlID",
            //    table: "Control_Address",
            //    column: "ControlID",
            //    principalTable: "Control",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ControlUsers_AspNetUsers_UserID",
                table: "ControlUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SemesterSubjects_StudentSemester_StudentSemesterID",
                table: "Student_SemesterSubjects",
                column: "StudentSemesterID",
                principalTable: "StudentSemester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SemesterSubjects_Subject_SubjectID",
                table: "Student_SemesterSubjects",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Committees_Committees_CommitteeID",
                table: "Subject_Committees",
                column: "CommitteeID",
                principalTable: "Committees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Committees_Subject_SubjectID",
                table: "Subject_Committees",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssess_Assess_AssessID",
                table: "SubjectAssess",
                column: "AssessID",
                principalTable: "Assess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssess_Subject_SubjectID",
                table: "SubjectAssess",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BYLAW_Faculity_FaculityID",
                table: "BYLAW");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Control_Address_Control_ControlID",
            //    table: "Control_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_Task_Control_ControlID",
                table: "Control_Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                table: "Control_UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlUsers_AspNetUsers_UserID",
                table: "ControlUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_SemesterSubjects_StudentSemester_StudentSemesterID",
                table: "Student_SemesterSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_SemesterSubjects_Subject_SubjectID",
                table: "Student_SemesterSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Committees_Committees_CommitteeID",
                table: "Subject_Committees");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Committees_Subject_SubjectID",
                table: "Subject_Committees");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssess_Assess_AssessID",
                table: "SubjectAssess");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectAssess_Subject_SubjectID",
                table: "SubjectAssess");

            migrationBuilder.DropTable(
                name: "ControlSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControlUsers",
                table: "ControlUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Control_UserTasks",
                table: "Control_UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectAssess",
                table: "SubjectAssess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject_Committees",
                table: "Subject_Committees");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Control_Address",
            //    table: "Control_Address");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cef7fe4-0365-44ac-a22d-41fcecced6b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6444a70d-13a5-4bf3-a65d-6b98281d3ff0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c964ad85-2b2d-41c3-a191-0406c6363435");

            migrationBuilder.RenameTable(
                name: "SubjectAssess",
                newName: "SubjectAssesse");

            migrationBuilder.RenameTable(
                name: "Subject_Committees",
                newName: "Subject_Committee");

            //migrationBuilder.RenameTable(
            //    name: "conrol_Addresse",
            //    newName: "Control_Address");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectAssess_AssessID",
                table: "SubjectAssesse",
                newName: "IX_SubjectAssesse_AssessID");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_Committees_CommitteeID",
                table: "Subject_Committee",
                newName: "IX_Subject_Committee_CommitteeID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Control_Address_ControlID",
            //    table: "conrol_Addresse",
            //    newName: "IX_conrol_Addresse_ControlID");

            //migrationBuilder.AlterColumn<string>(
            //    name: "StudentSemesterID",
            //    table: "Student_SemesterSubjects",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "Student_SemesterSubjects",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "Student_SemesterSubjects",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserID",
            //    table: "ControlUsers",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "ControlUsers",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Control_TaskID",
            //    table: "Control_UserTasks",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserTaskID",
            //    table: "Control_UserTasks",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "Control_UserTasks",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Control_Task",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "ControlID",
            //    table: "Control_Task",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WriteDate",
                table: "Control_Note",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Start_Date",
                table: "Control",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End_Date",
                table: "Control",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "FaculityID",
            //    table: "BYLAW",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "AssessID",
            //    table: "SubjectAssesse",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "SubjectAssesse",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "SubjectAssesse",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "CommitteeID",
            //    table: "Subject_Committee",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "SubjectID",
            //    table: "Subject_Committee",
            //    type: "nvarchar(200)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "Subject_Committee",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Address",
            //    table: "Control_Addresses",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AddColumn<string>(
            //    name: "Id",
            //    table: "Control_Addresses",
            //    type: "nvarchar(200)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student_SemesterSubjects",
                table: "Student_SemesterSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControlUsers",
                table: "ControlUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Control_UserTasks",
                table: "Control_UserTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectAssesse",
                table: "SubjectAssesse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject_Committee",
                table: "Subject_Committee",
                column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_conrol_Addresse",
            //    table: "Control_Address",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "ControlSubject",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    IsDone = table.Column<int>(type: "int", nullable: true),
                    IsReview = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlSubject_Control_ControlID",
                        column: x => x.ControlID,
                        principalTable: "Control",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlSubject_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3639bbbf-4345-4375-a788-e67c6bab0fba", null, "AdminFaculity", "AdminFaculity" },
                    { "3a1ff073-6872-455c-98c5-a7c948f19c2b", null, "AdminUniversity", "AdminUniversity" },
                    { "8c46af3f-cfbd-46f7-8b34-789456dfb7c6", null, "Staff", "Staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_SemesterSubjects_SubjectID",
                table: "Student_SemesterSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlUsers_UserID",
                table: "ControlUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_UserTasks_UserTaskID",
                table: "Control_UserTasks",
                column: "UserTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssesse_SubjectID",
                table: "SubjectAssesse",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Committee_SubjectID",
                table: "Subject_Committee",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlSubject_ControlID",
                table: "ControlSubject",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlSubject_SubjectID",
                table: "ControlSubject",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_BYLAW_Faculity_FaculityID",
                table: "BYLAW",
                column: "FaculityID",
                principalTable: "Faculity",
                principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Control_Addresses_Control_ControlID",
            //    table: "Control_Address",
            //    column: "ControlID",
            //    principalTable: "Control",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ControlUsers_AspNetUsers_UserID",
                table: "ControlUsers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SemesterSubjects_StudentSemester_StudentSemesterID",
                table: "Student_SemesterSubjects",
                column: "StudentSemesterID",
                principalTable: "StudentSemester",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SemesterSubjects_Subject_SubjectID",
                table: "Student_SemesterSubjects",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Committee_Committees_CommitteeID",
                table: "Subject_Committee",
                column: "CommitteeID",
                principalTable: "Committees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Committee_Subject_SubjectID",
                table: "Subject_Committee",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssesse_Assess_AssessID",
                table: "SubjectAssesse",
                column: "AssessID",
                principalTable: "Assess",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectAssesse_Subject_SubjectID",
                table: "SubjectAssesse",
                column: "SubjectID",
                principalTable: "Subject",
                principalColumn: "Id");
        }
    }
}
