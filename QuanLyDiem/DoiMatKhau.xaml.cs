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
    /// Interaction logic for DoiMatKhau.xaml
    /// </summary>
    public partial class DoiMatKhau : Window
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManHinhQuanTri manHinhQuanTri = new ManHinhQuanTri();
            manHinhQuanTri.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            var query = from tk in db.Taikhoans
                        where tk.UserName == txtTaiKhoan.Text
                        select tk;
            Taikhoan taikhoan = query.FirstOrDefault();
            if(taikhoan != null)
            {
                if (isCheck(taikhoan))
                {
                    taikhoan.Passwords = txtMatKhauMoi2.Password;
                    db.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công","Thông báo");
                    ManHinhQuanTri m = new ManHinhQuanTri();
                    m.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private bool isCheck(Taikhoan taikhoan)
        {
            if (txtMatKhauCu.Password != taikhoan.Passwords)
            {
                MessageBox.Show("Mật khẩu không đúng!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtMatKhauMoi1.Password != txtMatKhauMoi2.Password)
            {
                MessageBox.Show("Mật khẩu mới và nhập lại khác nhau!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
