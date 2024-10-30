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
    /// Interaction logic for QuanLyDiemSo.xaml
    /// </summary>
    public partial class QuanLyDiemSo : Window
    {
        public QuanLyDiemSo()
        {
            InitializeComponent();
        }
        QldiemContext db = new QldiemContext();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDuLieu();
            var query = from mh in db.Monhocs
                        select new
                        {
                            TenMh = mh.TenMh + "(" + mh.MaMh + ")",
                            MaMh = mh.MaMh
                        };
            cboMH.ItemsSource = query.ToList();
            cboMH.DisplayMemberPath = "TenMh";
            cboMH.SelectedValuePath = "MaMh";

        }
        private void HienThiDuLieu()
        {
            ////-- dùng linq để lấy dữ liệu trong lớp thực thể phòng ban
            //var query = from d in db.Diems
            //            select d;
            ////--hiển thị lên datagrid
            //dtgDiem.ItemsSource = query.ToList();
            //-- dùng linq để lấy dữ liệu trong lớp thực thể phòng ban
            var query = from d in db.Diems
                        join sv in db.Sinhviens on d.MaSv equals sv.MaSv
                        join mh in db.Monhocs on d.MaMh equals mh.MaMh
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
            dtgDiem.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isKiemTra())
            {
                Diem d = new Diem();
                d.MaSv = txtMaSV.Text;
                d.MaMh = cboMH.SelectedValue.ToString();
                d.DiemTx1 = double.Parse(txtTX1.Text);
                d.DiemTx2 = double.Parse(txtTX2.Text);
                d.DiemThi = double.Parse(txtDiemThi.Text);
                d.DiemTkso = double.Parse(txtTKSo.Text);
                string chu = TinhDiemChu((double)d.DiemTkso);
                
                d.DiemTkchu = chu;
                db.Diems.Add(d);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }
        private string TinhDiemChu(double d)
        {
            string chu = "";
            if (d >= 8.5)
            {
                chu = "A";
            }
            else if (d >= 8)
            {
                chu = "B+";
            }
            else if (d >= 7)
            {
                chu = "B";
            }
            else if (d >= 6.5)
            {
                chu = "C+";
            }
            else if (d >= 5.5)
            {
                chu = "C";
            }
            else if (d >= 5)
            {
                chu = "D+";
            }
            else if (d >= 4)
            {
                chu = "D";
            }
            else
            {
                chu = "F";
            }
            return chu;
        }
        private bool isKiemTra()
        {
            if (txtMaSV.Text == "")
            {
                MessageBox.Show("Không được để trống mã SV!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (txtTX1.Text == "")
            {
                MessageBox.Show("Không được để trống TX1!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTX2.Text == "")
            {
                MessageBox.Show("Không được để trống TX2!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDiemThi.Text == "")
            {
                MessageBox.Show("Không được để trống điểm thi!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTKSo.Text == "")
            {
                MessageBox.Show("Không được để trống điểm tổng kết số!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var queries = from sv in db.Sinhviens
                          where sv.MaSv == txtMaSV.Text
                          select sv;
            Sinhvien s = queries.FirstOrDefault();
            if(s == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên để thêm!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            {
                double tx1 = double.Parse(txtTX1.Text);
                double tx2 = double.Parse(txtTX2.Text);
                double diemThi = double.Parse(txtDiemThi.Text);
                double diemTK = double.Parse(txtTKSo.Text);
                if (tx1 < 0 || tx1 > 10  || tx2 < 0 || tx2 > 10 || diemThi < 0 || diemThi > 10 || diemTK < 0 || diemTK > 10)
                {
                    MessageBox.Show("Điểm phải là số thực >0 và <10!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Các điểm phải là số!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //kiểm tra trùng khóa chính
            var mh = db.Diems.FirstOrDefault(x => x.MaMh.Equals(cboMH.SelectedValue.ToString()) && x.MaSv.Equals(txtMaSV.Text));
            if (mh != null)
            {
                MessageBox.Show($"Điểm của sinh viên {mh.MaSv} ở môn {mh.MaMh} đã tồn tại! ", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var querySua = from d in db.Diems
                           where d.MaSv == txtMaSV.Text && d.MaMh == cboMH.SelectedValue.ToString()
            select d;
            Diem m = querySua.FirstOrDefault();
            if (m != null)
            {
                m.DiemTx1 = double.Parse(txtTX1.Text);
                m.DiemTx2 = double.Parse(txtTX2.Text);
                m.DiemThi = double.Parse(txtDiemThi.Text);
                m.DiemTkso = double.Parse(txtTKSo.Text);
                m.DiemTkchu = TinhDiemChu((double)m.DiemTkso);
                db.SaveChanges();
                HienThiDuLieu();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var queryXoa = from d in db.Diems
                           where d.MaMh == cboMH.SelectedValue.ToString() && d.MaSv == txtMaSV.Text
                           select d;
            Diem m = queryXoa.FirstOrDefault();
            if (m != null)
            {
                MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Hỏi xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rs == MessageBoxResult.Yes)
                {
                    db.Diems.Remove(m);
                    db.SaveChanges();
                    HienThiDuLieu();
                }
            }
            else
            {
                MessageBox.Show("Không có mã môn học muốn xóa!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(txtMaSV.Text == "")
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
            DiemN diem = query.FirstOrDefault();
            
            if (diem != null)
            {
                dtgDiem.ItemsSource = query.ToList();
            }
            else
            {
                MessageBox.Show("Không có sinh viên muốn tìm", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dtgDiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgDiem.SelectedIndex != -1)
            {
                DiemN m = (DiemN)dtgDiem.SelectedItem;
                txtMaSV.Text = m.MaSv;
                cboMH.SelectedValue = m.MaMh;
                txtTX1.Text = m.DiemTx1 + "";
                txtTX2.Text = m.DiemTx2 + "";
                txtDiemThi.Text = m.DiemThi + "";
                txtTKSo.Text = m.DiemTkso + "";
            }
        }
    }
}
