using QuanLyDiem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for SuaSinhVien.xaml
    /// </summary>
    public partial class SuaSinhVien : Window
    {
        public SuaSinhVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new();
        QuanLySinhVien f = new QuanLySinhVien();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var svSua = db.Sinhviens.SingleOrDefault(sv => sv.MaSv == txtMa.Text);
                if (svSua != null)
                {
                    svSua.TenSv = txtTen.Text;
                    svSua.NgaySinh = txtNgay.SelectedDate.Value;
                    if (rbNam.IsChecked == true)
                    {
                        svSua.GioiTinh = "Nam";
                    }
                    else
                    {
                        svSua.GioiTinh = "Nữ";
                    }
                    svSua.DiaChi = txtDiaChi.Text;
                    svSua.SoDt = txtSDT.Text;
                    svSua.Email = txtEmail.Text;
                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công", "Thông Báo", MessageBoxButton.OK);
                    f.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi sửa" + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDL();
        }
        private List<SV> LayDL()
        {
            var query = from t in db.Sinhviens
                        select new SV
                        {
                            maSV = t.MaSv,
                            tenSV = t.TenSv,
                            ngaySinh = t.NgaySinh,
                            gioiTinh = t.GioiTinh,
                            diaChi = t.DiaChi,
                            soDT = t.SoDt,
                            email = t.Email
                        };
            return query.ToList<SV>();
        }
        private void HienThiDL()
        {
            dgSV.ItemsSource = LayDL();
        }

        private void dgSV_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Type t = dgSV.SelectedItem.GetType();
            PropertyInfo[] p = t.GetProperties();
            txtMa.Text = p[0].GetValue(dgSV.SelectedValue).ToString();
            txtTen.Text = p[1].GetValue(dgSV.SelectedValue).ToString();
            txtNgay.Text = p[2].GetValue(dgSV.SelectedValue).ToString();
            if (p[3].GetValue(dgSV.SelectedValue).ToString() == "Nam")
            {
                rbNam.IsChecked = true;
            }
            else
            {
                rbNu.IsChecked = true;
            }
            txtDiaChi.Text = p[4].GetValue(dgSV.SelectedValue).ToString();
            txtSDT.Text = p[5].GetValue(dgSV.SelectedValue).ToString();
            txtEmail.Text = p[6].GetValue(dgSV.SelectedValue).ToString();
        }
    }
}
