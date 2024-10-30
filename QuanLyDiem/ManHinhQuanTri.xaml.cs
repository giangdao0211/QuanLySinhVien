using QuanLyDiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuanLyDiem
{
    /// <summary>
    /// Interaction logic for ManHinhQuanTri.xaml
    /// </summary>
    public partial class ManHinhQuanTri : Window
    {
        public ManHinhQuanTri()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
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
            if (rs == MessageBoxResult.Yes)
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

        private void btnArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            tgHamburger.IsChecked = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nav_pnl.Width = 0;
            var query = from gv in db.Giaoviens
                        select gv;
            var query1 = from sv in db.Sinhviens
                        select sv;
            var query2 = from mh in db.Monhocs
                        select mh;
            tbSoGV.Text = query.Count() + "";
            tbSoSV.Text = query1.Count() + "";
            tbSoMH.Text = query2.Count() + "";
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var query = from gv in db.Giaoviens
                        select gv;
            var query1 = from sv in db.Sinhviens
                         select sv;
            var query2 = from mh in db.Monhocs
                         select mh;
            tbSoGV.Text = query.Count() + "";
            tbSoSV.Text = query1.Count() + "";
            tbSoMH.Text = query2.Count() + "";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoiMatKhau d = new DoiMatKhau();
            this.Close();
            d.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "HỎI THOÁT", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rs == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            XemThongTinGiaoVien xemThongTinGiaoVien = new XemThongTinGiaoVien();
            xemThongTinGiaoVien.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            XemThongTinMonHoc xemThongTinMonHoc = new XemThongTinMonHoc();
            xemThongTinMonHoc.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            TraCuuLopHP traCuuLopHP = new TraCuuLopHP();
            traCuuLopHP.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            CacLopHPDangDienRa cacLopHPDangDienRa = new CacLopHPDangDienRa();
            cacLopHPDangDienRa.ShowDialog();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            KhungTheoKy khungTheoKy = new KhungTheoKy();
            khungTheoKy.ShowDialog();
        }

    }
}
