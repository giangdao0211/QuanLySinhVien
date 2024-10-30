using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiem
{
    internal class DiemN
    {
        public string MaMh { get; set; }

        public string MaSv { get; set; }

        public double? DiemTx1 { get; set; }

        public double? DiemTx2 { get; set; }

        public double? DiemThi { get; set; }

        public double? DiemTkso { get; set; }

        public string? DiemTkchu { get; set; }
        public string TenSv { get; set; }
        public string TenMh { get; set; } = null!;
    }
}
