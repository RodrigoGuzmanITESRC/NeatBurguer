using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NeatBurguer.Models
{
    public partial class neatContext : DbContext
    {
        public neatContext()
        {
        }

        public neatContext(DbContextOptions<neatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clasificacion> Clasificacions { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=neat", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Clasificacion>(entity =>
            {
                entity.ToTable("clasificacion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(45);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.HasIndex(e => e.IdClasificacion, "fkclas_idx");

                entity.Property(e => e.Descripción).HasColumnType("text");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasColumnType("double(8,2)");

                entity.HasOne(d => d.IdClasificacionNavigation)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.IdClasificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkclas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
