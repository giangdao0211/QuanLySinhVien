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
    /// Interaction logic for ChucNangQuanLy.xaml
    /// </summary>
    public partial class ChucNangQuanLy : Window
    {
        public ChucNangQuanLy()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuanLyMonHoc ql = new QuanLyMonHoc();
            ql.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            QuanLyGiaoVien ql1 = new QuanLyGiaoVien();
            ql1.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            QuanLySinhVien ql2 = new QuanLySinhVien();
            ql2.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            QuanLyDiemSo ql3 = new QuanLyDiemSo();
            ql3.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TimKiemSinhVien t = new TimKiemSinhVien();
            t.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ThongKeDiemSo tk = new ThongKeDiemSo();
            tk.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "HỎI THOÁT", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(rs == MessageBoxResult.Yes)
            {
                MainWindow m = new MainWindow();
                this.Close();
                m.ShowDialog();
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            TheoDoiKQHT td = new TheoDoiKQHT();
            td.ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            QuanLyCacLopHocPhan quanLyCacLopHocPhan = new QuanLyCacLopHocPhan();
            quanLyCacLopHocPhan.ShowDialog();
        }
    }
}
