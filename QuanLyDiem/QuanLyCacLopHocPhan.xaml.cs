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
    /// Interaction logic for QuanLyCacLopHocPhan.xaml
    /// </summary>
    public partial class QuanLyCacLopHocPhan : Window
    {
        public QuanLyCacLopHocPhan()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgayBD.SelectedDate = DateTime.Today.AddDays(-1);
            dtpNgayKT.SelectedDate = DateTime.Today;

            var query = from gv in db.Giaoviens
                        select gv;
            var query1 = from kh in db.Khoas
                         select kh;
            var query2 = from mh in db.Monhocs
                         select mh;

            cboGiaoVien.ItemsSource = query.ToList();
            cboGiaoVien.DisplayMemberPath = "HoTen";
            cboGiaoVien.SelectedValuePath = "MaGv";

            cboKhoa.ItemsSource = query1.ToList();
            cboKhoa.DisplayMemberPath = "TenKhoa";
            cboKhoa.SelectedValuePath = "MaKhoa";

            cboMonHoc.ItemsSource = query2.ToList();
            cboMonHoc.DisplayMemberPath = "TenMh";
            cboMonHoc.SelectedValuePath = "MaMh";
            HienThiDuLieu();

        }
        private void HienThiDuLieu()
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
            if (isKiemTra())
            {
                Lop lop = new Lop();
                lop.MaLop = txtMa.Text;
                lop.TenLop = txtTen.Text;
                lop.MaGv = cboGiaoVien.SelectedValue.ToString();
                lop.MaKhoa = cboKhoa.SelectedValue.ToString();
                lop.MaMh = cboMonHoc.SelectedValue.ToString();
                lop.NgayBatDauLopHoc = dtpNgayBD.SelectedDate;
                lop.NgayKetThucLopHoc = dtpNgayKT.SelectedDate;
                db.Lops.Add(lop);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }
        private bool isKiemTra()
        {
            if (txtMa.Text == "")
            {
                MessageBox.Show("Không được để trống mã!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTen.Text == "")
            {
                MessageBox.Show("Không được để trống tên!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cboGiaoVien.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn Giáo viên!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cboKhoa.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn Khoa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cboMonHoc.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn Học phần!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            DateTime d1 = (DateTime)dtpNgayBD.SelectedDate;
            DateTime d2 = (DateTime)dtpNgayKT.SelectedDate;
            if (d2 < d1)
            {
                MessageBox.Show("Ngày kết thúc không thể có trước ngày bắt đầu!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //kiểm tra trùng khóa chính
            var l = db.Lops.FirstOrDefault(x => x.MaLop.Equals(txtMa.Text));
            if (l != null)
            {
                MessageBox.Show("Mã lớp đã tồn tại!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var querySua = from l in db.Lops
                           where l.MaLop == txtMa.Text
                           select l;
            Lop lop = querySua.FirstOrDefault();
            if (lop != null)
            {
                lop.TenLop = txtTen.Text;
                lop.MaGv = cboGiaoVien.SelectedValue.ToString();
                lop.MaKhoa = cboKhoa.SelectedValue.ToString();
                lop.MaMh = cboMonHoc.SelectedValue.ToString();
                lop.NgayBatDauLopHoc = dtpNgayBD.SelectedDate;
                lop.NgayKetThucLopHoc = dtpNgayKT.SelectedDate;
                db.SaveChanges();
                HienThiDuLieu();
            }
            else
            {
                MessageBox.Show("Không có mã lớp học muốn sửa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var queryXoa = from l in db.Lops
                           where l.MaLop == txtMa.Text
                           select l;
            Lop lop = queryXoa.FirstOrDefault();
            if (lop != null)
            {
                MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Hỏi xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rs == MessageBoxResult.Yes)
                {
                    db.Lops.Remove(lop);
                    db.SaveChanges();
                    HienThiDuLieu();
                }
            }
            else
            {
                MessageBox.Show("Không có mã lớp học muốn xóa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgLop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtgLop.SelectedItem != null)
            {
                LopHP l = dtgLop.SelectedItem as LopHP;
                txtMa.Text = l.MaLop;
                txtTen.Text = l.TenLop;
                cboGiaoVien.SelectedValue = l.MaGv;
                cboKhoa.SelectedValue = l.MaKhoa;
                cboMonHoc.SelectedValue = l.MaMh;
                dtpNgayBD.SelectedDate = l.NgayBatDauLopHoc;
                dtpNgayKT.SelectedDate = l.NgayKetThucLopHoc;
            }
        }
    }
    public class LopHP
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public string? MaGv { get; set; }
        public string? MaKhoa { get; set; }
        public string? MaMh { get; set; }
        public string HoTen { get; set; }
        public string TenKhoa { get; set; }
        public string TenMh { get; set; }
        public DateTime? NgayBatDauLopHoc { get; set; }
        public DateTime? NgayKetThucLopHoc { get; set; }
    }
}
