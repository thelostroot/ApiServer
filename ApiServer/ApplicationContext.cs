using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTags>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTags>()
                .HasOne(at => at.Post)
                .WithMany(a => a.PostTags)
                .HasForeignKey(at => at.PostId);

            modelBuilder.Entity<PostTags>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(at => at.TagId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public DbSet<ApiServer.Models.PostTags> PostTags { get; set; }
    }
}
