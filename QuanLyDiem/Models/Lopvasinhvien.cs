using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Lopvasinhvien
{
    public string MaLop { get; set; } = null!;

    public string MaSv { get; set; } = null!;

    public DateTime? NgayBatDauLopHoc { get; set; }

    public DateTime? NgayKetThucLopHoc { get; set; }

    public virtual Lop MaLopNavigation { get; set; } = null!;

    public virtual Sinhvien MaSvNavigation { get; set; } = null!;
}
