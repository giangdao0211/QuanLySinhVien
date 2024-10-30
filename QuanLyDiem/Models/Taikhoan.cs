using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Taikhoan
{
    public string UserName { get; set; } = null!;

    public string Passwords { get; set; } = null!;

    public virtual ICollection<Giaovien> Giaoviens { get; set; } = new List<Giaovien>();

    public virtual ICollection<Sinhvien> Sinhviens { get; set; } = new List<Sinhvien>();
}
