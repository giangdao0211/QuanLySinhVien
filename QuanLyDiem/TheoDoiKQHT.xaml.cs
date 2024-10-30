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
    /// Interaction logic for TheoDoiKQHT.xaml
    /// </summary>
    public partial class TheoDoiKQHT : Window
    {
        public TheoDoiKQHT()
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

        private void dtgMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtgMonHoc.SelectedItem != null)
            {
                Monhoc m = dtgMonHoc.SelectedItem as Monhoc;
                var query = from mh in db.Monhocs
                            where mh.MaMh == m.MaMh
                            select mh;
                Monhoc m1 = query.FirstOrDefault();
                if (m1 != null)
                {
                    DiemTheoMonHocPage p = new DiemTheoMonHocPage();
                    var query1 = from d in db.Diems
                                join sv in db.Sinhviens on d.MaSv equals sv.MaSv
                                join mh in db.Monhocs on d.MaMh equals mh.MaMh
                                where d.MaMh == m1.MaMh
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
                    //--hiển thị lên datagrid
                    p.dtgDiem.ItemsSource = query1.ToList();
                    dtgMonHoc.Visibility = Visibility.Hidden;
                    frame.NavigationService.Navigate(p);
                    cdTB.Content = "Kết quả học tập học phần " + m1.TenMh;
                }
            }
        }
    }
}
