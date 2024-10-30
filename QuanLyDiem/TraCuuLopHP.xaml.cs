using QuanLyDiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyDiem
{
    /// <summary>
    /// Interaction logic for TraCuuLopHP.xaml
    /// </summary>
    public partial class TraCuuLopHP : Window
    {
        public TraCuuLopHP()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query3 = from l in db.Lops
                         join gv in db.Giaoviens on l.MaGv equals gv.MaGv
                         join kh in db.Khoas on l.MaKhoa equals kh.MaKhoa
                         join mh in db.Monhocs on l.MaMh equals mh.MaMh
                         select new LopHP
                         {
                             MaLop = l.MaLop,
                             TenLop = l.TenLop,
                             MaGv = l.MaGv,
                             MaMh = l.MaMh,
                             MaKhoa = l.MaKhoa,
                             HoTen = gv.HoTen,
                             TenKhoa = kh.TenKhoa,
                             TenMh = mh.TenMh,
                             NgayBatDauLopHoc = l.NgayBatDauLopHoc,
                             NgayKetThucLopHoc = l.NgayKetThucLopHoc
                         };
            dtgLop.ItemsSource = query3.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from l in db.Lops
                        join gv in db.Giaoviens on l.MaGv equals gv.MaGv
                        join kh in db.Khoas on l.MaKhoa equals kh.MaKhoa
                        join mh in db.Monhocs on l.MaMh equals mh.MaMh
                        where l.MaLop == txtMaLop.Text
                         select new LopHP
                         {
                             MaLop = l.MaLop,
                             TenLop = l.TenLop,
                             MaGv = l.MaGv,
                             MaMh = l.MaMh,
                             MaKhoa = l.MaKhoa,
                             HoTen = gv.HoTen,
                             TenKhoa = kh.TenKhoa,
                             TenMh = mh.TenMh,
                             NgayBatDauLopHoc = l.NgayBatDauLopHoc,
                             NgayKetThucLopHoc = l.NgayKetThucLopHoc
                         };
            LopHP s = query.FirstOrDefault();
            if (s != null)
            {
                tbThongBao.Text = "";
                dtgLop.ItemsSource = query.ToList();
            }
            else
            {
                dtgLop.ItemsSource = null;
                tbThongBao.Text = "Không tìm thấy sinh viên!";
            }
        }

    }
}
