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
    /// Interaction logic for XemThongTinMonHoc.xaml
    /// </summary>
    public partial class XemThongTinMonHoc : Window
    {
        public XemThongTinMonHoc()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Monhocs
                        select mh;
            dtgMonHoc.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Monhocs
                        where mh.MaMh == txtMaMH.Text
                        select mh;
            Monhoc m = query.FirstOrDefault();
            if (m != null)
            {
                dtgMonHoc.ItemsSource = query.ToList();
                tbThongBao.Text = "";
            }
            else
            {
                dtgMonHoc.ItemsSource = null;
                tbThongBao.Text = "Không tìm thấy môn học!";
            }
        }
    }
}
