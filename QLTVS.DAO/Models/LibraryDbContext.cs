using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLTVS.DAO.Models;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietmuonsach> Chitietmuonsaches { get; set; }

    public virtual DbSet<Phieumuon> Phieumuons { get; set; }

    public virtual DbSet<Quanly> Quanlies { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Tailieu> Tailieus { get; set; }

    public virtual DbSet<Theloai> Theloais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=QLTVS;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietmuonsach>(entity =>
        {
            entity.HasKey(e => new { e.Maphieu, e.Matailieu }).HasName("chitietmuonsach_pkey");

            entity.ToTable("chitietmuonsach");

            entity.Property(e => e.Maphieu)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("maphieu");
            entity.Property(e => e.Matailieu)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("matailieu");
            entity.Property(e => e.Ngaytratailieu).HasColumnName("ngaytratailieu");
            entity.Property(e => e.Soluong)
                .HasDefaultValue(1)
                .HasColumnName("soluong");
            entity.Property(e => e.Tinhtrangmuon)
                .HasMaxLength(50)
                .HasColumnName("tinhtrangmuon");
            entity.Property(e => e.Tinhtrangtra)
                .HasMaxLength(50)
                .HasColumnName("tinhtrangtra");

            entity.HasOne(d => d.MaphieuNavigation).WithMany(p => p.Chitietmuonsaches)
                .HasForeignKey(d => d.Maphieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chitietmuonsach_maphieu_fkey");

            entity.HasOne(d => d.MatailieuNavigation).WithMany(p => p.Chitietmuonsaches)
                .HasForeignKey(d => d.Matailieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("chitietmuonsach_matailieu_fkey");
        });

        modelBuilder.Entity<Phieumuon>(entity =>
        {
            entity.HasKey(e => e.Maphieu).HasName("phieumuon_pkey");

            entity.ToTable("phieumuon");

            entity.Property(e => e.Maphieu)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("maphieu");
            entity.Property(e => e.Hantra).HasColumnName("hantra");
            entity.Property(e => e.Masv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("masv");
            entity.Property(e => e.Ngaymuon).HasColumnName("ngaymuon");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Đang mượn'::character varying")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.MasvNavigation).WithMany(p => p.Phieumuons)
                .HasForeignKey(d => d.Masv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("phieumuon_masv_fkey");
        });

        modelBuilder.Entity<Quanly>(entity =>
        {
            entity.HasKey(e => e.Maql).HasName("quanly_pkey");

            entity.ToTable("quanly");

            entity.Property(e => e.Maql)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("maql");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("hoten");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("sdt");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.Masv).HasName("sinhvien_pkey");

            entity.ToTable("sinhvien");

            entity.Property(e => e.Masv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("masv");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("hoten");
            entity.Property(e => e.Lop)
                .HasMaxLength(50)
                .HasColumnName("lop");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("sdt");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Tendangnhap).HasName("taikhoan_pkey");

            entity.ToTable("taikhoan");

            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(50)
                .HasColumnName("tendangnhap");
            entity.Property(e => e.Maql)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("maql");
            entity.Property(e => e.Masv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("masv");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(100)
                .HasColumnName("matkhau");
            entity.Property(e => e.Vaitro)
                .HasMaxLength(20)
                .HasColumnName("vaitro");

            entity.HasOne(d => d.MaqlNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Maql)
                .HasConstraintName("taikhoan_maql_fkey");

            entity.HasOne(d => d.MasvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.Masv)
                .HasConstraintName("taikhoan_masv_fkey");
        });

        modelBuilder.Entity<Tailieu>(entity =>
        {
            entity.HasKey(e => e.Matailieu).HasName("tailieu_pkey");

            entity.ToTable("tailieu");

            entity.Property(e => e.Matailieu)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("matailieu");
            entity.Property(e => e.Linktailieu)
                .HasMaxLength(255)
                .HasColumnName("linktailieu");
            entity.Property(e => e.Matheloai)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("matheloai");
            entity.Property(e => e.Namxuatban).HasColumnName("namxuatban");
            entity.Property(e => e.Nhaxuatban)
                .HasMaxLength(100)
                .HasColumnName("nhaxuatban");
            entity.Property(e => e.Soluong)
                .HasDefaultValue(0)
                .HasColumnName("soluong");
            entity.Property(e => e.Tacgia)
                .HasMaxLength(100)
                .HasColumnName("tacgia");
            entity.Property(e => e.Tentailieu)
                .HasMaxLength(200)
                .HasColumnName("tentailieu");

            entity.HasOne(d => d.MatheloaiNavigation).WithMany(p => p.Tailieus)
                .HasForeignKey(d => d.Matheloai)
                .HasConstraintName("tailieu_matheloai_fkey");
        });

        modelBuilder.Entity<Theloai>(entity =>
        {
            entity.HasKey(e => e.Matheloai).HasName("theloai_pkey");

            entity.ToTable("theloai");

            entity.Property(e => e.Matheloai)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("matheloai");
            entity.Property(e => e.Tentheloai)
                .HasMaxLength(100)
                .HasColumnName("tentheloai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
