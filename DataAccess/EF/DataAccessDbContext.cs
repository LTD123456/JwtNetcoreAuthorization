using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EF
{
    public class DataAccessDbContext : DbContext
    {
        public DataAccessDbContext(DbContextOptions<DataAccessDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("blogs");

                entity.Property(e => e.Id).HasColumnType("int");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.HasIndex(e => e.BlogId)
                    .HasName("FK_Post_Blog_BlogId_idx");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Post_Blog_BlogId");
            });
        }


    }
}
