using Explorer.Models.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Explorer.DataAccess
{
    public partial class explorerdbContext : DbContext
    {
        public explorerdbContext()
        {
        }

        public explorerdbContext(DbContextOptions<explorerdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<Models.Entities.Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=explorerdb;Username=postgres;Password=12321325");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("files");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Description)
                    .HasMaxLength(70)
                    .HasColumnName("description");

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .HasColumnName("file_name");

                
                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.FolderId).HasColumnName("folder_id");
                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.Files)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(d => d.FolderId)
                    .HasConstraintName("files_folder_id_fkey");   
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.ToTable("folders");

                entity.Property(e => e.FolderId).HasColumnName("folder_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<Models.Entities.Type>(entity =>
            {
                entity.ToTable("types");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.Format)
                    .HasMaxLength(100)
                    .HasColumnName("format");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
