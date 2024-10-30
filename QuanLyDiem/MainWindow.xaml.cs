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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyDiem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            Check();
        }
        private void Check()
        {
            var query = from acc in db.Taikhoans
                        where acc.UserName == txtTaiKhoan.Text && acc.Passwords == txtMatKhau.Password
                        select acc;
            
            Taikhoan tk = query.FirstOrDefault();
            if (tk == null)
            {
                MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không chính xác!", "Sai tài khoản",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var query1 = from gv in db.Giaoviens
                             where gv.UserName == txtTaiKhoan.Text
                             select gv;
                Giaovien gv1 = query1.FirstOrDefault();
                ManHinhQuanTri w = new ManHinhQuanTri();
                if (gv1 == null)
                {
                    var query2 = from sv in db.Sinhviens
                                 where sv.UserName == txtTaiKhoan.Text
                                 select sv;
                    Sinhvien sv1 = query2.FirstOrDefault();
                    if (sv1 != null)
                    {
                        w.tbHoTen.Text = sv1.TenSv;
                        w.mniQuanLy.IsEnabled = false;
                        w.qlGV.IsEnabled = false;
                        w.qlDS.IsEnabled = false;
                        w.qlLop.IsEnabled = false;
                        w.qlMH.IsEnabled = false;
                        w.qlSV.IsEnabled = false;
                        w.Show();
                        this.Close();
                    }
                }
                else
                {
                    w.tbHoTen.Text = gv1.HoTen;
                    w.Show();
                    this.Close();
                }

            }
        }
    }
}
