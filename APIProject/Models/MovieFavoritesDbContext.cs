using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIProject.Models
{
    public partial class MovieFavoritesDbContext : DbContext
    {
        public MovieFavoritesDbContext()
        {
        }

        public MovieFavoritesDbContext(DbContextOptions<MovieFavoritesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavList> FavList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress01;Database=MovieFavoritesDb;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavList>(entity =>
            {
                entity.Property(e => e.Actor).HasMaxLength(100);

                entity.Property(e => e.Genre).HasMaxLength(30);

                entity.Property(e => e.Plot).HasMaxLength(200);

                entity.Property(e => e.Runtime).HasMaxLength(20);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
