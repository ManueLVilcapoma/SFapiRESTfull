using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SFapiRESTfull.Models;

public partial class FalabellaDbContext : DbContext
{
    public FalabellaDbContext()
    {
    }

    public FalabellaDbContext(DbContextOptions<FalabellaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleDeCompra> DetalleDeCompras { get; set; }

    public virtual DbSet<Kardex> Kardexs { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=constring");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.HasIndex(e => e.Nombre, "UK_Categorias_Nombre").IsUnique();

            entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra);

            entity.Property(e => e.IdCompra).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_ProveedorId");
        });

        modelBuilder.Entity<DetalleDeCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDeCompra);

            entity.ToTable("DetalleDeCompra");

            entity.Property(e => e.IdDetalleDeCompra).ValueGeneratedNever();

            entity.HasOne(d => d.Compra).WithMany(p => p.DetalleDeCompras)
                .HasForeignKey(d => d.CompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDeCompra_CompraId");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleDeCompras)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleDeCompra_ProductoId");
        });

        modelBuilder.Entity<Kardex>(entity =>
        {
            entity.HasKey(e => e.IdKardex);

            entity.Property(e => e.IdKardex).ValueGeneratedNever();
            entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.Kardices)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardexs_ProductoId");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Kardices)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardexs_UsuarioId");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso);

            entity.HasIndex(e => e.Nombre, "UK_Permisos_Nombre").IsUnique();

            entity.Property(e => e.IdPermiso).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.HasIndex(e => e.Codigo, "UK_Productos_Codigo").IsUnique();

            entity.HasIndex(e => e.Nombre, "UK_Productos_Nombre").IsUnique();

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaDeVencimiento).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_CategoriaId");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.HasIndex(e => e.Nombre, "UK_Proveedores_Nombre").IsUnique();

            entity.HasIndex(e => e.Telefono, "UK_Proveedores_Telefono").IsUnique();

            entity.Property(e => e.IdProveedor).ValueGeneratedNever();
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.HasIndex(e => e.Email, "UK_Usuarios_Email").IsUnique();

            entity.HasIndex(e => e.Login, "UK_Usuarios_Login").IsUnique();

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuarioPermiso>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioPermiso);

            entity.ToTable("UsuarioPermiso");

            entity.Property(e => e.IdUsuarioPermiso).ValueGeneratedNever();

            entity.HasOne(d => d.Permiso).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPermiso_PermisoId");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioPermiso_UsuarioId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
