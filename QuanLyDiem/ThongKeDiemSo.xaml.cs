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
    /// Interaction logic for ThongKeDiemSo.xaml
    /// </summary>
    public partial class ThongKeDiemSo : Window
    {
        public ThongKeDiemSo()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã SV!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var query = from d in db.Diems
                        join sv in db.Sinhviens on d.MaSv equals sv.MaSv
                        join mh in db.Monhocs on d.MaMh equals mh.MaMh
                        where d.MaSv == txtMaSV.Text
                        select new DiemN
                        {
                            MaMh = d.MaMh,
                            MaSv = d.MaSv,
                            DiemTx1 = d.DiemTx1,
                            DiemTx2 = d.DiemTx2,
                            DiemThi = d.DiemThi,
                            DiemTkso = d.DiemTkso,
                            DiemTkchu = d.DiemTkchu,
                            TenSv = sv.TenSv,
                            TenMh = mh.TenMh
                        };
            DiemN m = query.FirstOrDefault();
            if (m != null)
            {
                tb0.Visibility = Visibility.Visible; tb1.Visibility = Visibility.Visible;
                tbHoTen.Text = m.TenSv;
                double gpa = 0; int c = 0;
                foreach (var d in query)
                {
                    gpa += (double)d.DiemTkso;
                    c++;
                }
                gpa = gpa / c;
                gpa = gpa * 4 / 10;
                tbGPA.Text = String.Format("{0:0.00}",gpa);
                dtgDiem.ItemsSource = query.ToList();
            }
            else
            {
                tb0.Visibility = Visibility.Hidden; tb1.Visibility = Visibility.Hidden;
                tbHoTen.Text = "";
                tbGPA.Text = "";
                MessageBox.Show("Không có sinh viên muốn tìm", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
