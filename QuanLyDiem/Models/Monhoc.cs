using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Monhoc
{
    public string MaMh { get; set; } = null!;

    public string TenMh { get; set; } = null!;

    public int? LyThuyet { get; set; }

    public int? ThucHanh { get; set; }

    public int? SoTc { get; set; }

    public int? Ky { get; set; }

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
