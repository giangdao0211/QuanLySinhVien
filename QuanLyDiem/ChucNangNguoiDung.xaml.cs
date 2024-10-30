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
    /// Interaction logic for ChucNangNguoiDung.xaml
    /// </summary>
    public partial class ChucNangNguoiDung : Window
    {
        public ChucNangNguoiDung()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TimKiemSinhVien tk = new TimKiemSinhVien();
            tk.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThongKeDiemSo thong = new ThongKeDiemSo();
            thong.ShowDialog();
        }

        

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            XemThongTinMonHoc xemThongTinMonHoc = new XemThongTinMonHoc();
            xemThongTinMonHoc.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            XemThongTinGiaoVien xemThongTinGiaoVien = new XemThongTinGiaoVien();
            xemThongTinGiaoVien.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "HỎI THOÁT", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rs == MessageBoxResult.Yes)
            {
                MainWindow m = new MainWindow();
                this.Close();
                m.ShowDialog();
            }
        }
    }
}
