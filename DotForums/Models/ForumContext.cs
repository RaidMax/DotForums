﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DotForums.Models
{
    public class ForumContext : DbContext
    {
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DotForums.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserModel
           modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Threads)
                .WithOne(t => t.Author)
                .HasForeignKey(t => t.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(u => u.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Groups)
                .WithOne(g => g.User)
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Make Username and Email unique (but changable)
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => new { u.Username, u.Email });
                
            // Set creation date to current time
            modelBuilder.Entity<UserModel>()
                .Property(c => c.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<UserModel>()
                .Property(u => u.Seen)
                .ValueGeneratedOnAddOrUpdate()
                .ForSqliteHasDefaultValueSql("CURRENT_TIMESTAMP");
            #endregion

            #region UserGroup
            modelBuilder.Entity<UserGroupModel>()
                .HasAlternateKey(u => new { u.GroupID, u.UserID });

            modelBuilder.Entity<UserGroupModel>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(u => u.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region UserInformation
            modelBuilder.Entity<UserInformationModel>()
                .HasOne(ui => ui.Avatar)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region ThreadModel
            modelBuilder.Entity<ThreadModel>()
                .Property(t => t.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ThreadModel>()
                .Property(t => t.Modified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ThreadModel>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Threads)
                .HasForeignKey(a => a.AuthorID);

            modelBuilder.Entity<ThreadModel>()
                 .HasMany(t => t.Posts)
                 .WithOne(p => p.Parent)
                 .HasForeignKey(p => p.ParentID)
                 .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region PermissionModel
            modelBuilder.Entity<PermissionModel>()
                .HasOne(p => p.Thread)
                .WithMany(t => t.Permissions)
                .HasForeignKey(p => p.ThreadID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
