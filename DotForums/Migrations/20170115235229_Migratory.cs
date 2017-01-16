using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotForums.Migrations
{
    public partial class Migratory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvatarModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarModel", x => x.ID);
                });

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
                    Name = table.Column<string>(nullable: true)
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
                    AvatarID = table.Column<ulong>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Email = table.Column<string>(nullable: false),
                    GroupID = table.Column<ulong>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Posts = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    lastLogin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.UniqueConstraint("AK_Users_Username_Email", x => new { x.Username, x.Email });
                    table.ForeignKey(
                        name: "FK_Users_AvatarModel_AvatarID",
                        column: x => x.AvatarID,
                        principalTable: "AvatarModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorID = table.Column<ulong>(nullable: false),
                    CategoryModelID = table.Column<ulong>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Name = table.Column<string>(nullable: true),
                    ParentID = table.Column<ulong>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Threads_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threads_Categories_CategoryModelID",
                        column: x => x.CategoryModelID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Threads_Threads_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Threads",
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
                name: "IX_Threads_AuthorID",
                table: "Threads",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CategoryModelID",
                table: "Threads",
                column: "CategoryModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ParentID",
                table: "Threads",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarID",
                table: "Users",
                column: "AvatarID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupID",
                table: "Users",
                column: "GroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventModel");

            migrationBuilder.DropTable(
                name: "PermissionModel");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AvatarModel");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
