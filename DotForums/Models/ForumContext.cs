using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace DotForums.Models
{
    public class ForumContext : DbContext
    {
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DotForums.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserModel
            // Make Username and Email unique
            modelBuilder.Entity<UserModel>().HasAlternateKey(u => new { u.Username, u.Email });
            // Set creation date to current time
            modelBuilder.Entity<UserModel>()
                .Property(c => c.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<UserModel>()
                .Property(u => u.lastLogin)
                .ValueGeneratedOnAddOrUpdate()
                .ForSqliteHasDefaultValueSql("CURRENT_TIMESTAMP");
            #endregion

            #region ThreadModel
            modelBuilder.Entity<ThreadModel>()
                .Property(t => t.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); ;
            modelBuilder.Entity<ThreadModel>()
                .Property(t => t.Modified)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            #endregion
        }
    }
}
