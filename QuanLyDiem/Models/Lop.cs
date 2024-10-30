using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Lop
{
    public string MaLop { get; set; } = null!;

    public string TenLop { get; set; } = null!;

    public string? MaGv { get; set; }

    public string? MaKhoa { get; set; }

    public string? MaMh { get; set; }
    public DateTime? NgayBatDauLopHoc { get; set; }
    public DateTime? NgayKetThucLopHoc { get; set; }

    public virtual ICollection<Lopvasinhvien> Lopvasinhviens { get; set; } = new List<Lopvasinhvien>();

    public virtual Giaovien? MaGvNavigation { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual Monhoc? MaMhNavigation { get; set; }
}
