using System;
using System.Collections.Generic;

namespace QuanLyDiem.Models;

public partial class Giaovien
{
    public string MaGv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string QueQuan { get; set; } = null!;

    public string SoDt { get; set; } = null!;

    public string TrinhDoHocVan { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string MaKhoa { get; set; } = null!;

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();

    public virtual Khoa MaKhoaNavigation { get; set; } = null!;

    public virtual Taikhoan UserNameNavigation { get; set; } = null!;
}
