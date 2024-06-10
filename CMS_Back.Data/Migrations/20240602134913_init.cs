using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS_Back.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACAD_YEAR",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACAD_YEAR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assess",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaculityType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaculityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student_STATUS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Student_Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_STATUS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Study_Method",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study_Method", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScientificDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTPExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FaculityEmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FaculityLeaderID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaculityTypeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserLeaderID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculity_AspNetUsers_UserLeaderID",
                        column: x => x.UserLeaderID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faculity_FaculityType_FaculityTypeID",
                        column: x => x.FaculityTypeID,
                        principalTable: "FaculityType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BYLAW",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyMethodID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BYLAW", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BYLAW_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BYLAW_Study_Method_StudyMethodID",
                        column: x => x.StudyMethodID,
                        principalTable: "Study_Method",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Control",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faculity_Phase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculity_Node = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculity_Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ACAD_YEAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreatorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_AspNetUsers_UserCreatorID",
                        column: x => x.UserCreatorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Control_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculity_Node",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculity_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculity_Node_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faculity_Node_Faculity_Node_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Faculity_Node",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Faculity_Phase",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculity_Phase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculity_Phase_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Faculity_Semester",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculity_Semester", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculity_Semester_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotionalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FaculityID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Faculity_FaculityID",
                        column: x => x.FaculityID,
                        principalTable: "Faculity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "conrol_Addresse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conrol_Addresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conrol_Addresse_Control_ControlID",
                        column: x => x.ControlID,
                        principalTable: "Control",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Control_Note",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WriteByID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_Note_AspNetUsers_WriteByID",
                        column: x => x.WriteByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Control_Note_Control_ControlID",
                        column: x => x.ControlID,
                        principalTable: "Control",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Control_Task",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDone = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateByID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_Task_AspNetUsers_CreateByID",
                        column: x => x.CreateByID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Control_Task_Control_ControlID",
                        column: x => x.ControlID,
                        principalTable: "Control",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ControlUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlUsers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ControlUsers_Control_ControlID",
                        column: x => x.ControlID,
                        principalTable: "Control",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FaculityNodeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_Faculity_Node_FaculityNodeID",
                        column: x => x.FaculityNodeID,
                        principalTable: "Faculity_Node",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FaculityHierarycal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhaseID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BAYLAWID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BYLAWId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaculityHierarycal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaculityHierarycal_BYLAW_BYLAWId",
                        column: x => x.BYLAWId,
                        principalTable: "BYLAW",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaculityHierarycal_Faculity_Phase_PhaseID",
                        column: x => x.PhaseID,
                        principalTable: "Faculity_Phase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaculityHierarycal_Faculity_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Faculity_Semester",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Control_UserTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Control_TaskID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserTaskID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control_UserTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_UserTasks_AspNetUsers_UserTaskID",
                        column: x => x.UserTaskID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Control_UserTasks_Control_Task_Control_TaskID",
                        column: x => x.Control_TaskID,
                        principalTable: "Control_Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSemester",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GPA = table.Column<double>(type: "float", nullable: true),
                    TOTAL = table.Column<double>(type: "float", nullable: true),
                    IsPass = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<double>(type: "float", nullable: true),
                    StudentStatusID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FaculityNodeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StudentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AcadYearID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FaculityHierarcalID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSemester", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSemester_ACAD_YEAR_AcadYearID",
                        column: x => x.AcadYearID,
                        principalTable: "ACAD_YEAR",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSemester_FaculityHierarycal_FaculityHierarcalID",
                        column: x => x.FaculityHierarcalID,
                        principalTable: "FaculityHierarycal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSemester_Faculity_Node_FaculityNodeID",
                        column: x => x.FaculityNodeID,
                        principalTable: "Faculity_Node",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSemester_Student_STATUS_StudentStatusID",
                        column: x => x.StudentStatusID,
                        principalTable: "Student_STATUS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSemester_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDone = table.Column<int>(type: "int", nullable: true),
                    IsReview = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit_Hours = table.Column<int>(type: "int", nullable: true),
                    FaculityNodeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FaculityHierarycalID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_FaculityHierarycal_FaculityHierarycalID",
                        column: x => x.FaculityHierarycalID,
                        principalTable: "FaculityHierarycal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subject_Faculity_Node_FaculityNodeID",
                        column: x => x.FaculityNodeID,
                        principalTable: "Faculity_Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlSubject",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Student_SemesterSubjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Degree = table.Column<double>(type: "float", nullable: true),
                    IsPass = table.Column<int>(type: "int", nullable: true),
                    StudentSemesterID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_SemesterSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_SemesterSubjects_StudentSemester_StudentSemesterID",
                        column: x => x.StudentSemesterID,
                        principalTable: "StudentSemester",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_SemesterSubjects_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subject_Committee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommitteeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject_Committee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Committee_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subject_Committee_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectAssesse",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MAX_Degree = table.Column<double>(type: "float", nullable: true),
                    MIN_Degree = table.Column<double>(type: "float", nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssessID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAssesse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectAssesse_Assess_AssessID",
                        column: x => x.AssessID,
                        principalTable: "Assess",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectAssesse_Subject_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c490e48-07ab-492a-8287-c5a831b61560", null, "AdminUniversity", "AdminUniversity" },
                    { "39c86721-71a4-4c6a-a952-2baf73f3b424", null, "AdminFaculity", "AdminFaculity" },
                    { "b755e362-7c9e-44f4-a1fa-4d97e1c3d6bc", null, "Staff", "Staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FaculityEmployeeID",
                table: "AspNetUsers",
                column: "FaculityEmployeeID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BYLAW_FaculityID",
                table: "BYLAW",
                column: "FaculityID");

            migrationBuilder.CreateIndex(
                name: "IX_BYLAW_StudyMethodID",
                table: "BYLAW",
                column: "StudyMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_FaculityNodeID",
                table: "Committees",
                column: "FaculityNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_conrol_Addresse_ControlID",
                table: "conrol_Addresse",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_FaculityID",
                table: "Control",
                column: "FaculityID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_UserCreatorID",
                table: "Control",
                column: "UserCreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Note_ControlID",
                table: "Control_Note",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Note_WriteByID",
                table: "Control_Note",
                column: "WriteByID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Task_ControlID",
                table: "Control_Task",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_Task_CreateByID",
                table: "Control_Task",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_UserTasks_Control_TaskID",
                table: "Control_UserTasks",
                column: "Control_TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Control_UserTasks_UserTaskID",
                table: "Control_UserTasks",
                column: "UserTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlSubject_ControlID",
                table: "ControlSubject",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlSubject_SubjectID",
                table: "ControlSubject",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlUsers_ControlID",
                table: "ControlUsers",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlUsers_UserID",
                table: "ControlUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculity_FaculityTypeID",
                table: "Faculity",
                column: "FaculityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculity_UserLeaderID",
                table: "Faculity",
                column: "UserLeaderID",
                unique: true,
                filter: "[UserLeaderID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faculity_Node_FaculityNodeID",
                table: "Faculity_Node",
                column: "FaculityNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculity_Node_ParentID",
                table: "Faculity_Node",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Faculity_Phase_FaculityID",
                table: "Faculity_Phase",
                column: "FaculityID");


            migrationBuilder.CreateIndex(
                name: "IX_Faculity_Semester_FaculityID",
                table: "Faculity_Semester",
                column: "FaculityID");

            migrationBuilder.CreateIndex(
                name: "IX_FaculityHierarycal_BYLAWId",
                table: "FaculityHierarycal",
                column: "BYLAWId");

            migrationBuilder.CreateIndex(
                name: "IX_FaculityHierarycal_PhaseID",
                table: "FaculityHierarycal",
                column: "PhaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FaculityHierarycal_SemesterID",
                table: "FaculityHierarycal",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_FaculityID",
                table: "Staff",
                column: "FaculityID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FaculityID",
                table: "Student",
                column: "FaculityID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SemesterSubjects_StudentSemesterID",
                table: "Student_SemesterSubjects",
                column: "StudentSemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SemesterSubjects_SubjectID",
                table: "Student_SemesterSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemester_AcadYearID",
                table: "StudentSemester",
                column: "AcadYearID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemester_FaculityHierarcalID",
                table: "StudentSemester",
                column: "FaculityHierarcalID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemester_FaculityNodeID",
                table: "StudentSemester",
                column: "FaculityNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemester_StudentID",
                table: "StudentSemester",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemester_StudentStatusID",
                table: "StudentSemester",
                column: "StudentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_FaculityHierarycalID",
                table: "Subject",
                column: "FaculityHierarycalID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_FaculityNodeID",
                table: "Subject",
                column: "FaculityNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Committee_CommitteeID",
                table: "Subject_Committee",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Committee_SubjectID",
                table: "Subject_Committee",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssesse_AssessID",
                table: "SubjectAssesse",
                column: "AssessID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAssesse_SubjectID",
                table: "SubjectAssesse",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculity_FaculityEmployeeID",
                table: "AspNetUsers",
                column: "FaculityEmployeeID",
                principalTable: "Faculity",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculity_AspNetUsers_UserLeaderID",
                table: "Faculity");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "conrol_Addresse");

            migrationBuilder.DropTable(
                name: "Control_Note");

            migrationBuilder.DropTable(
                name: "Control_UserTasks");

            migrationBuilder.DropTable(
                name: "ControlSubject");

            migrationBuilder.DropTable(
                name: "ControlUsers");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Student_SemesterSubjects");

            migrationBuilder.DropTable(
                name: "Subject_Committee");

            migrationBuilder.DropTable(
                name: "SubjectAssesse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Control_Task");

            migrationBuilder.DropTable(
                name: "StudentSemester");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "Assess");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Control");

            migrationBuilder.DropTable(
                name: "ACAD_YEAR");

            migrationBuilder.DropTable(
                name: "Student_STATUS");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "FaculityHierarycal");

            migrationBuilder.DropTable(
                name: "Faculity_Node");

            migrationBuilder.DropTable(
                name: "BYLAW");

            migrationBuilder.DropTable(
                name: "Faculity_Phase");

            migrationBuilder.DropTable(
                name: "Faculity_Semester");

            migrationBuilder.DropTable(
                name: "Study_Method");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Faculity");

            migrationBuilder.DropTable(
                name: "FaculityType");
        }
    }
}
