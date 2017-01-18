using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotForums.Migrations
{
    public partial class m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryModelID = table.Column<ulong>(nullable: true),
                    Count = table.Column<ulong>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryModelID",
                        column: x => x.CategoryModelID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Seen = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    UserModelID = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventModel_Users_UserModelID",
                        column: x => x.UserModelID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false),
                    CategoryModelID = table.Column<ulong>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Threads_Categories_CategoryModelID",
                        column: x => x.CategoryModelID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Threads_Users_ID",
                        column: x => x.ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<int>(nullable: false),
                    GroupID1 = table.Column<ulong>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    UserID1 = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserGroupModel_Groups_GroupID1",
                        column: x => x.GroupID1,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroupModel_Users_UserID1",
                        column: x => x.UserID1,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryModelID = table.Column<ulong>(nullable: true),
                    GroupID = table.Column<ulong>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Permission = table.Column<int>(nullable: false),
                    ThreadModelID = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionModel_Categories_CategoryModelID",
                        column: x => x.CategoryModelID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionModel_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionModel_Threads_ThreadModelID",
                        column: x => x.ThreadModelID,
                        principalTable: "Threads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false),
                    AuthorID = table.Column<ulong>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Threads_ID",
                        column: x => x.ID,
                        principalTable: "Threads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryModelID",
                table: "Categories",
                column: "CategoryModelID");

            migrationBuilder.CreateIndex(
                name: "IX_EventModel_UserModelID",
                table: "EventModel",
                column: "UserModelID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionModel_CategoryModelID",
                table: "PermissionModel",
                column: "CategoryModelID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionModel_GroupID",
                table: "PermissionModel",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionModel_ThreadModelID",
                table: "PermissionModel",
                column: "ThreadModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CategoryModelID",
                table: "Threads",
                column: "CategoryModelID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupModel_GroupID1",
                table: "UserGroupModel",
                column: "GroupID1");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupModel_UserID1",
                table: "UserGroupModel",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username_Email",
                table: "Users",
                columns: new[] { "Username", "Email" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventModel");

            migrationBuilder.DropTable(
                name: "PermissionModel");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserGroupModel");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
