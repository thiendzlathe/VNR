using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VNR.Models;

public partial class VnrInternShipContext : DbContext
{
    public VnrInternShipContext()
    {
    }

    public VnrInternShipContext(DbContextOptions<VnrInternShipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=VNR_InternShip;User ID=sa;Password=123;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KhoaHoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.TheLoai");

            entity.ToTable("KhoaHoc");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenKhoaHoc).HasMaxLength(30);
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.BaiHat");

            entity.ToTable("MonHoc");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KhoaHocId).HasColumnName("KhoaHocID");
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.TenMonHoc).HasMaxLength(100);

            entity.HasOne(d => d.KhoaHoc).WithMany(p => p.MonHocs)
                .HasForeignKey(d => d.KhoaHocId)
                .HasConstraintName("FK_dbo.BaiHat_dbo.TheLoai_TheLoaiID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
