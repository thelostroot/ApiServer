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
        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
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
            modelBuilder.Entity<ArticleTags>()
                .HasKey(t => new { t.ArticleId, t.TagId });

            modelBuilder.Entity<ArticleTags>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTagses)
                .HasForeignKey(at => at.ArticleId);

            modelBuilder.Entity<ArticleTags>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTagses)
                .HasForeignKey(at => at.TagId);
        }

        public DbSet<ApiServer.Models.ArticleTags> ArticleTags { get; set; }
    }
}
