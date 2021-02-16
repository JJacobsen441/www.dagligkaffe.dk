using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace www.dagligkaffe.dk.Models
{
    public partial class dagligkaffedkContext : DbContext
    {
        public dagligkaffedkContext()
        {
        }

        public dagligkaffedkContext(DbContextOptions<dagligkaffedkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coffee> Coffees { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=dagligkaffe.dk;Username=Joaki;Password=Nostromo2503");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<Coffee>(entity =>
            {
                entity.ToTable("coffee");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("author");

                entity.Property(e => e.Bean).HasColumnName("bean");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.Storie)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("storie");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("_comment");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("author");

                entity.Property(e => e.CoffeeId).HasColumnName("coffee_id");

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("comment");

                entity.HasOne(d => d.Coffee)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CoffeeId)
                    .HasConstraintName("comment_coffee_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
