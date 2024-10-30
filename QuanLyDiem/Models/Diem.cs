using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Diem
{
    public string MaMh { get; set; } = null!;

    public string MaSv { get; set; } = null!;

    public double? DiemTx1 { get; set; }

    public double? DiemTx2 { get; set; }

    public double? DiemThi { get; set; }

    public double? DiemTkso { get; set; }

    public string? DiemTkchu { get; set; }

    public virtual Monhoc MaMhNavigation { get; set; } = null!;

    public virtual Sinhvien MaSvNavigation { get; set; } = null!;
}
