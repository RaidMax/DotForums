using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotForums.Models;

namespace DotForums.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20170118214234_m")]
    partial class m
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("DotForums.Models.ImageModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<string>("Name");

                    b.Property<string>("URL");

                    b.HasKey("ID");

                    b.ToTable("ImageModel");
                });

            modelBuilder.Entity("DotForums.Models.PermissionModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong?>("CategoryModelID");

                    b.Property<ulong?>("GroupID");

                    b.Property<string>("Name");

                    b.Property<int>("Permission");

                    b.Property<ulong?>("ThreadID");

                    b.HasKey("ID");

                    b.HasIndex("CategoryModelID");

                    b.HasIndex("GroupID");

                    b.HasIndex("ThreadID");

                    b.ToTable("PermissionModel");
                });

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("AuthorID");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Name");

                    b.Property<ulong>("ParentID");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ParentID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("AuthorID");

                    b.Property<ulong>("CategoryID");

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

                    b.HasIndex("AuthorID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("DotForums.Models.UserGroupModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("GroupID");

                    b.Property<string>("Name");

                    b.Property<ulong>("UserID");

                    b.HasKey("ID");

                    b.HasAlternateKey("GroupID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserGroupModel");
                });

            modelBuilder.Entity("DotForums.Models.UserInformationModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("AvatarID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("AvatarID")
                        .IsUnique();

                    b.ToTable("UserInformationModel");
                });

            modelBuilder.Entity("DotForums.Models.UserInformationModel+IP", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Time");

                    b.Property<ulong?>("UserInformationModelID");

                    b.HasKey("ID");

                    b.HasIndex("UserInformationModelID");

                    b.ToTable("IP");
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

                    b.Property<ulong>("ProfileID");

                    b.Property<DateTime>("Seen")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Sqlite:DefaultValueSql", "CURRENT_TIMESTAMP");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("ProfileID");

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

                    b.HasOne("DotForums.Models.ThreadModel", "Thread")
                        .WithMany("Permissions")
                        .HasForeignKey("ThreadID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DotForums.Models.ThreadModel", "Parent")
                        .WithMany("Posts")
                        .HasForeignKey("ParentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DotForums.Models.CategoryModel", "Category")
                        .WithMany("Threads")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.UserGroupModel", b =>
                {
                    b.HasOne("DotForums.Models.GroupModel", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DotForums.Models.UserModel", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.UserInformationModel", b =>
                {
                    b.HasOne("DotForums.Models.ImageModel", "Avatar")
                        .WithOne()
                        .HasForeignKey("DotForums.Models.UserInformationModel", "AvatarID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotForums.Models.UserInformationModel+IP", b =>
                {
                    b.HasOne("DotForums.Models.UserInformationModel")
                        .WithMany("IPS")
                        .HasForeignKey("UserInformationModelID");
                });

            modelBuilder.Entity("DotForums.Models.UserModel", b =>
                {
                    b.HasOne("DotForums.Models.UserInformationModel", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
