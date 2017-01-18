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
                name: "ImageModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserInformationModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AvatarID = table.Column<ulong>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformationModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserInformationModel_ImageModel_AvatarID",
                        column: x => x.AvatarID,
                        principalTable: "ImageModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IP",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserInformationModelID = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IP", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IP_UserInformationModel_UserInformationModelID",
                        column: x => x.UserInformationModelID,
                        principalTable: "UserInformationModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    ProfileID = table.Column<ulong>(nullable: false),
                    Seen = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserInformationModel_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "UserInformationModel",
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
                    CategoryID = table.Column<ulong>(nullable: false),
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
                        name: "FK_Threads_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Threads_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupModel",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupID = table.Column<ulong>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserID = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupModel", x => x.ID);
                    table.UniqueConstraint("AK_UserGroupModel_GroupID_UserID", x => new { x.GroupID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserGroupModel_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupModel_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    ThreadID = table.Column<ulong>(nullable: true)
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
                        name: "FK_PermissionModel_Threads_ThreadID",
                        column: x => x.ThreadID,
                        principalTable: "Threads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorID = table.Column<ulong>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentID = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Threads_ParentID",
                        column: x => x.ParentID,
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
                name: "IX_PermissionModel_ThreadID",
                table: "PermissionModel",
                column: "ThreadID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentID",
                table: "Posts",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_AuthorID",
                table: "Threads",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CategoryID",
                table: "Threads",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupModel_UserID",
                table: "UserGroupModel",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformationModel_AvatarID",
                table: "UserInformationModel",
                column: "AvatarID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IP_UserInformationModelID",
                table: "IP",
                column: "UserInformationModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileID",
                table: "Users",
                column: "ProfileID");

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
                name: "IP");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "UserInformationModel");

            migrationBuilder.DropTable(
                name: "ImageModel");
        }
    }
}
