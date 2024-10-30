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
    /// Interaction logic for KhungTheoKy.xaml
    /// </summary>
    public partial class KhungTheoKy : Window
    {
        public KhungTheoKy()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Monhocs
                        orderby mh.Ky
                        select mh;
            dtgMonHoc.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from mh in db.Monhocs
                        where mh.Ky == int.Parse(cboKy.Text)
                        select mh;
            dtgMonHoc.ItemsSource = query.ToList();
        }
    }
}
