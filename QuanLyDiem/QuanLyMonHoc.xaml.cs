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
    /// Interaction logic for QuanLyMonHoc.xaml
    /// </summary>
    public partial class QuanLyMonHoc : Window
    {
        QldiemContext db = new QldiemContext();
        public QuanLyMonHoc()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Monhocs
                        select mh;
            dtgMonHoc.ItemsSource = query.ToList();
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
            if (txtTCLT.Text == "" || txtTCTH.Text == "")
            {
                MessageBox.Show("Không được để trống số tín chỉ!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            {
                int soTCTH = int.Parse(txtTCTH.Text);
                int soTCLT = int.Parse(txtTCLT.Text);
                if (soTCTH < 0 || soTCLT < 0)
                {
                    MessageBox.Show("Số tín chỉ phải là số nguyên >= 0!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Số tín chỉ phải là số!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //kiểm tra trùng khóa chính
            var mh = db.Monhocs.FirstOrDefault(x => x.MaMh.Equals(txtMa.Text));
            if (mh != null)
            {
                MessageBox.Show("Mã môn học đã tồn tại!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isKiemTra())
            {
                Monhoc mh = new Monhoc();
                mh.MaMh = txtMa.Text;
                mh.TenMh = txtTen.Text;
                mh.LyThuyet = int.Parse(txtTCLT.Text);
                mh.ThucHanh = int.Parse(txtTCTH.Text);
                mh.SoTc = mh.LyThuyet + mh.ThucHanh;
                mh.Ky = int.Parse(cboKyHoc.Text);
                db.Monhocs.Add(mh);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }
        private void HienThiDuLieu()
        {
            //-- dùng linq để lấy dữ liệu trong lớp thực thể phòng ban
            var query = from mh in db.Monhocs
                        select mh;
            //--hiển thị lên datagrid
            dtgMonHoc.ItemsSource = query.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var querySua = from mh in db.Monhocs
                           where mh.MaMh == txtMa.Text
                           select mh;
            Monhoc m = querySua.FirstOrDefault();
            if (m != null)
            {
                m.TenMh = txtTen.Text;
                m.LyThuyet = int.Parse(txtTCLT.Text);
                m.ThucHanh = int.Parse(txtTCTH.Text);
                m.SoTc = m.LyThuyet + m.ThucHanh;
                m.Ky = int.Parse(cboKyHoc.Text);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var queryXoa = from mh in db.Monhocs
                           where mh.MaMh == txtMa.Text
                           select mh;
            Monhoc m = queryXoa.FirstOrDefault();
            if (m != null)
            {
                MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Hỏi xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rs == MessageBoxResult.Yes)
                {
                    db.Monhocs.Remove(m);
                    db.SaveChanges();
                    HienThiDuLieu();
                }
            }
            else
            {
                MessageBox.Show("Không có mã môn học muốn xóa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgMonHoc.SelectedIndex != -1)
            {
                Monhoc m = (Monhoc)dtgMonHoc.SelectedItem;
                txtMa.Text = m.MaMh;
                txtTen.Text = m.TenMh;
                txtTCLT.Text = m.LyThuyet + "";
                txtTCTH.Text = m.ThucHanh + "";
                cboKyHoc.SelectedIndex = (int)(m.Ky - 1); 
            }
        }
    }
}
