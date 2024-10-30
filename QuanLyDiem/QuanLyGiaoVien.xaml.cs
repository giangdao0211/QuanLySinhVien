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
    /// Interaction logic for QuanLyGiaoVien.xaml
    /// </summary>
    public partial class QuanLyGiaoVien : Window
    {
        QldiemContext db = new QldiemContext();
        public QuanLyGiaoVien()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isKiemTra())
            {
                Giaovien gv = new Giaovien();
                gv.MaGv = txtMa.Text;
                gv.HoTen = txtTen.Text;
                gv.NgaySinh = (DateTime)dtpNgaySinh.SelectedDate;
                gv.QueQuan = txtQueQuan.Text;
                gv.SoDt = txtSDT.Text;
                gv.TrinhDoHocVan = txtTrinhDoHocVan.Text;
                gv.UserName = "default";
                gv.MaKhoa = "CNTT";
                db.Giaoviens.Add(gv);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }
        private bool isKiemTra()
        {
            if(txtMa.Text == "")
            {
                MessageBox.Show("Không được để trống mã GV!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTen.Text == "")
            {
                MessageBox.Show("Không được để trống tên GV!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtQueQuan.Text == "")
            {
                MessageBox.Show("Không được để trống quê quán!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Không được để trống SĐT!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTrinhDoHocVan.Text == "")
            {
                MessageBox.Show("Không được để trống trình độ học vấn!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var mh = db.Giaoviens.FirstOrDefault(x => x.MaGv.Equals(txtMa.Text));
            if (mh != null)
            {
                MessageBox.Show("Mã giáo viên đã tồn tại!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var querySua = from gv in db.Giaoviens
                           where gv.MaGv == txtMa.Text
                           select gv;
            Giaovien g = querySua.FirstOrDefault();
            if (g != null)
            {
                g.HoTen = txtTen.Text;
                g.NgaySinh = (DateTime)dtpNgaySinh.SelectedDate;
                g.QueQuan = txtQueQuan.Text;
                g.SoDt = txtSDT.Text;
                g.TrinhDoHocVan = txtTrinhDoHocVan.Text;
                db.SaveChanges();
                HienThiDuLieu();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var queryXoa = from gv in db.Giaoviens
                           where gv.MaGv == txtMa.Text
                           select gv;
            Giaovien g = queryXoa.FirstOrDefault();
            if (g != null)
            {
                MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Hỏi xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rs == MessageBoxResult.Yes)
                {
                    db.Giaoviens.Remove(g);
                    db.SaveChanges();
                    HienThiDuLieu();
                }
            }
            else
            {
                MessageBox.Show("Không có mã giáo viên muốn xóa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgGiaoVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgGiaoVien.SelectedItem != null)
            {
                Giaovien g = dtgGiaoVien.SelectedItem as Giaovien;
                txtMa.Text = g.MaGv;
                txtTen.Text = g.HoTen;
                dtpNgaySinh.SelectedDate = g.NgaySinh;
                txtQueQuan.Text = g.QueQuan;
                txtSDT.Text = g.SoDt;
                txtTrinhDoHocVan.Text = g.TrinhDoHocVan;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Giaoviens
                        select mh;
            dtgGiaoVien.ItemsSource = query.ToList();
            dtpNgaySinh.SelectedDate = DateTime.Now;
        }
        private void HienThiDuLieu()
        {
            //-- dùng linq để lấy dữ liệu trong lớp thực thể phòng ban
            var query = from gv in db.Giaoviens
                        select gv;
            //--hiển thị lên datagrid
            dtgGiaoVien.ItemsSource = query.ToList();
        }
    }
}
