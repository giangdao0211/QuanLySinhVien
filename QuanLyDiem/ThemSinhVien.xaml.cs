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
    /// Interaction logic for ThemSinhVien.xaml
    /// </summary>
    public partial class ThemSinhVien : Window
    {
        public ThemSinhVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new();
        QuanLySinhVien f = new QuanLySinhVien();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = db.Sinhviens.SingleOrDefault(t => t.MaSv.Equals(txtMa.Text));
                if (query != null)
                {
                    MessageBox.Show("Da ton tai sinh vien", "Thong Bao", MessageBoxButton.OK);
                    f.Show();
                }
                else
                {
                    Sinhvien a = new Sinhvien();
                    a.MaSv = txtMa.Text;
                    a.TenSv = txtTen.Text;
                    a.NgaySinh = txtNgay.SelectedDate.Value;
                    if (rbNam.IsChecked == true)
                    {
                        a.GioiTinh = "Nam";
                    }
                    else
                    {
                        a.GioiTinh = "Nữ";
                    }
                    a.DiaChi = txtDiaChi.Text;
                    a.SoDt = txtSDT.Text;
                    a.Email = txtEmail.Text;
                    a.UserName = "dao";
                    db.Sinhviens.Add(a);
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButton.OK);
                    f.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thêm" + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
