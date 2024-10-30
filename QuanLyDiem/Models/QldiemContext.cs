using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDiem.Models;

public partial class QldiemContext : DbContext
{
    public QldiemContext()
    {
    }

    public QldiemContext(DbContextOptions<QldiemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<Lopvasinhvien> Lopvasinhviens { get; set; }

    public virtual DbSet<Monhoc> Monhocs { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-AFRCPMNE\\SQLEXPRESS;Initial Catalog=QLDiem;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => new { e.MaMh, e.MaSv }).HasName("pk_Diem");

            entity.ToTable("DIEM");

            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maMH");
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maSV");
            entity.Property(e => e.DiemThi).HasColumnName("diemThi");
            entity.Property(e => e.DiemTkchu)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("diemTKChu");
            entity.Property(e => e.DiemTkso).HasColumnName("diemTKSo");
            entity.Property(e => e.DiemTx1).HasColumnName("diemTX1");
            entity.Property(e => e.DiemTx2).HasColumnName("diemTX2");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__maMH__4316F928");

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaSv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__maSV__440B1D61");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GIAOVIEN__7A3E2D75E03BD345");

            entity.ToTable("GIAOVIEN");

            entity.Property(e => e.MaGv)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("maGV");
            entity.Property(e => e.HoTen)
                .HasMaxLength(32)
                .HasColumnName("hoTen");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("date")
                .HasColumnName("ngaySinh");
            entity.Property(e => e.QueQuan)
                .HasMaxLength(50)
                .HasColumnName("queQuan");
            entity.Property(e => e.SoDt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("soDT");
            entity.Property(e => e.TrinhDoHocVan)
                .HasMaxLength(20)
                .HasColumnName("trinhDoHocVan");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Giaoviens)
                .HasForeignKey(d => d.MaKhoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GIAOVIEN__maKhoa__3E52440B");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Giaoviens)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GIAOVIEN__userNa__3D5E1FD2");
        });

        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__KHOA__C79B8C228CB091B1");

            entity.ToTable("KHOA");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.TenKhoa)
                .HasMaxLength(50)
                .HasColumnName("tenKhoa");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__LOP__261ECAE376896016");

            entity.ToTable("LOP");

            entity.Property(e => e.MaLop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maLop");
            entity.Property(e => e.MaGv)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("maGV");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("maKhoa");
            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maMH");
            entity.Property(e => e.TenLop)
                .HasMaxLength(32)
                .HasColumnName("tenLop");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__LOP__maGV__46E78A0C");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__LOP__maKhoa__47DBAE45");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaMh)
                .HasConstraintName("FK__LOP__maMH__48CFD27E");
        });

        modelBuilder.Entity<Lopvasinhvien>(entity =>
        {
            entity.HasKey(e => new { e.MaLop, e.MaSv }).HasName("pk_LOP_SV");

            entity.ToTable("LOPVASINHVIEN");

            entity.Property(e => e.MaLop)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maLop");
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maSV");
            entity.Property(e => e.NgayBatDauLopHoc)
                .HasColumnType("date")
                .HasColumnName("ngayBatDauLopHoc");
            entity.Property(e => e.NgayKetThucLopHoc)
                .HasColumnType("date")
                .HasColumnName("ngayKetThucLopHoc");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.Lopvasinhviens)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LOPVASINH__maLop__4BAC3F29");

            entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.Lopvasinhviens)
                .HasForeignKey(d => d.MaSv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LOPVASINHV__maSV__4CA06362");
        });

        modelBuilder.Entity<Monhoc>(entity =>
        {
            entity.HasKey(e => e.MaMh).HasName("PK__MONHOC__7A3EDFA6C23538E8");

            entity.ToTable("MONHOC");

            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maMH");
            entity.Property(e => e.Ky).HasColumnName("ky");
            entity.Property(e => e.LyThuyet).HasColumnName("lyThuyet");
            entity.Property(e => e.SoTc).HasColumnName("soTC");
            entity.Property(e => e.TenMh)
                .HasMaxLength(32)
                .HasColumnName("tenMH");
            entity.Property(e => e.ThucHanh).HasColumnName("thucHanh");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__SINHVIEN__7A227A6469C1D28F");

            entity.ToTable("SINHVIEN");

            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("maSV");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(5)
                .HasColumnName("gioiTinh");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("date")
                .HasColumnName("ngaySinh");
            entity.Property(e => e.SoDt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("soDT");
            entity.Property(e => e.TenSv)
                .HasMaxLength(32)
                .HasColumnName("tenSV");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SINHVIEN__userNa__3A81B327");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__TAIKHOAN__66DCF95DFC93CF14");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.Property(e => e.Passwords)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("passwords");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
