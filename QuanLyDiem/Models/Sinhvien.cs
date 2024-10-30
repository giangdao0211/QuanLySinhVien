using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Sinhvien
{
    public string MaSv { get; set; } = null!;

    public string TenSv { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string SoDt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<Lopvasinhvien> Lopvasinhviens { get; set; } = new List<Lopvasinhvien>();

    public virtual Taikhoan UserNameNavigation { get; set; } = null!;
}
