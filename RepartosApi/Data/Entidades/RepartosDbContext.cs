using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepartosApi.Data.Entidades;

public partial class RepartosDbContext : DbContext
{
    public RepartosDbContext()
    {
    }

    public RepartosDbContext(DbContextOptions<RepartosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reparto> Repartos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=repartosdb,1433;Database=RepartosDB;User Id=sa;Password=Repartos123.;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reparto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repartos__3214EC073CDBF888");

            entity.Property(e => e.DireccionEntrega).HasMaxLength(255);
            entity.Property(e => e.Estado)
                .HasConversion<string>()  
                .HasMaxLength(50);
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
