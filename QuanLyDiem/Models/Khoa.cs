using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public virtual ICollection<Giaovien> Giaoviens { get; set; } = new List<Giaovien>();

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
