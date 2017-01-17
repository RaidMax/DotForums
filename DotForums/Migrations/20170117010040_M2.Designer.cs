﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotForums.Models;

namespace DotForums.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20170117010040_M2")]
    partial class M2
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

                    b.Property<int>("Members");

                    b.Property<string>("Name");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<ulong?>("UserModelID");

                    b.HasKey("ID");

                    b.HasIndex("UserModelID");

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

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.Property<ulong>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("AuthorID");

                    b.Property<ulong?>("CategoryModelID");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name");

                    b.Property<string>("Slug");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.HasIndex("CategoryModelID");

                    b.ToTable("Threads");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ThreadModel");
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

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.HasBaseType("DotForums.Models.ThreadModel");

                    b.Property<ulong?>("ParentID");

                    b.Property<ulong?>("UserModelID");

                    b.HasIndex("ParentID");

                    b.HasIndex("UserModelID");

                    b.ToTable("PostModel");

                    b.HasDiscriminator().HasValue("PostModel");
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

            modelBuilder.Entity("DotForums.Models.GroupModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel")
                        .WithMany("Groups")
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

            modelBuilder.Entity("DotForums.Models.ThreadModel", b =>
                {
                    b.HasOne("DotForums.Models.UserModel", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DotForums.Models.CategoryModel")
                        .WithMany("Threads")
                        .HasForeignKey("CategoryModelID");
                });

            modelBuilder.Entity("DotForums.Models.PostModel", b =>
                {
                    b.HasOne("DotForums.Models.ThreadModel", "Parent")
                        .WithMany("Posts")
                        .HasForeignKey("ParentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DotForums.Models.UserModel")
                        .WithMany("Posts")
                        .HasForeignKey("UserModelID");
                });
        }
    }
}