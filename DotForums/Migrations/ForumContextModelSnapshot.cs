using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotForums.Models;

namespace DotForums.Migrations
{
    [DbContext(typeof(ForumContext))]
    partial class ForumContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("DotForums.Models.CategoryModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong?>("CategoryModelID");

                    b.Property<ulong>("Count");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("CategoryModelID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DotForums.Models.EventModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<ulong?>("UserModelID");

                    b.HasKey("ID");

                    b.HasIndex("UserModelID");

                    b.ToTable("EventModel");
                });

            modelBuilder.Entity("DotForums.Models.GroupModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<string>("Name");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DotForums.Models.PermissionModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong?>("CategoryModelID");

                    b.Property<ulong?>("GroupID");

                    b.Property<string>("Name");

                    b.Property<int>("Permission");

                    b.Property<ulong?>("ThreadModelID");

                    b.HasKey("ID");

                    b.HasIndex("CategoryModelID");

                    b.HasIndex("GroupID");

                    b.HasIndex("ThreadModelID");

                    b.ToTable("PermissionModel");
                });

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.Property<ulong>("ID");

                    b.Property<ulong?>("AuthorID");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.Property<ulong>("ID");

                    b.Property<ulong?>("CategoryModelID");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("CategoryModelID");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("DotForums.Models.UserGroupModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupID");

                    b.Property<ulong?>("GroupID1");

                    b.Property<string>("Name");

                    b.Property<int>("UserID");

                    b.Property<ulong?>("UserID1");

                    b.HasKey("ID");

                    b.HasIndex("GroupID1");

                    b.HasIndex("UserID1");

                    b.ToTable("UserGroupModel");
                });

            modelBuilder.Entity("DotForums.Models.UserModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<DateTime>("Seen")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Sqlite:DefaultValueSql", "CURRENT_TIMESTAMP");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("Username", "Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DotForums.Models.CategoryModel", b =>
                {
                    b.HasOne("DotForums.Models.CategoryModel")
                        .WithMany("Children")
                        .HasForeignKey("CategoryModelID");
                });

            modelBuilder.Entity("DotForums.Models.EventModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel")
                        .WithMany("Events")
                        .HasForeignKey("UserModelID");
                });

            modelBuilder.Entity("DotForums.Models.PermissionModel", b =>
                {
                    b.HasOne("DotForums.Models.CategoryModel")
                        .WithMany("Permissions")
                        .HasForeignKey("CategoryModelID");

                    b.HasOne("DotForums.Models.GroupModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID");

                    b.HasOne("DotForums.Models.ThreadModel")
                        .WithMany("Permissions")
                        .HasForeignKey("ThreadModelID");
                });

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorID");

                    b.HasOne("DotForums.Models.ThreadModel", "Parent")
                        .WithMany("Posts")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.HasOne("DotForums.Models.CategoryModel")
                        .WithMany("Threads")
                        .HasForeignKey("CategoryModelID");

                    b.HasOne("DotForums.Models.UserModel", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.UserGroupModel", b =>
                {
                    b.HasOne("DotForums.Models.GroupModel", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupID1");

                    b.HasOne("DotForums.Models.UserModel", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserID1");
                });
        }
    }
}
