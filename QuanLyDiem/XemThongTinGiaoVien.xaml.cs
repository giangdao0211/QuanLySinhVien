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
    /// Interaction logic for XemThongTinGiaoVien.xaml
    /// </summary>
    public partial class XemThongTinGiaoVien : Window
    {
        public XemThongTinGiaoVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Giaoviens
                        select mh;
            dtgGiaoVien.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Giaoviens
                        where mh.MaGv == txtMaGV.Text
                        select mh;
            Giaovien g = query.FirstOrDefault();
            if (g != null) 
            {
                dtgGiaoVien.ItemsSource = query.ToList();
                tbThongBao.Text = "";
            }
            else
            {
                tbThongBao.Text = "Không tìm thấy Giáo viên!";
            }
        }
    }
}
