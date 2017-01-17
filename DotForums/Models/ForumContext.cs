using System;
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

            modelBuilder.Entity<ThreadModel>()
                .HasMany(t => t.Posts)
                .WithOne(p => p.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Threads)
                .WithOne(u => u.Author)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostModel>()
                .HasOne(u => u.Parent)
                .WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.Cascade);
            
            /*
            omEF CdelBuilder.Entity<UserModel>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<UserInformationModel>(b => b.ID)
                .OnDelete(DeleteBehavior.Cascade);*/

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
