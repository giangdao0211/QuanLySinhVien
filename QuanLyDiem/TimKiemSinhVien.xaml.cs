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
using QuanLyDiem.Models;
namespace QuanLyDiem
{
    /// <summary>
    /// Interaction logic for TimKiemSinhVien.xaml
    /// </summary>
    public partial class TimKiemSinhVien : Window
    {
        public TimKiemSinhVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from sv in db.Sinhviens
                        where sv.MaSv == txtMaSV.Text
                        select sv;
            Sinhvien s = query.FirstOrDefault();
            if (s != null)
            {
                tbThongBao.Text = "";
                dtgSinhVien.ItemsSource = query.ToList();
            }
            else
            {
                dtgSinhVien.ItemsSource = null;
                tbThongBao.Text = "Không tìm thấy sinh viên!";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from sv in db.Sinhviens
                        select sv;
            dtgSinhVien.ItemsSource = query.ToList();
        }
    }
}
