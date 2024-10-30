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
using Microsoft;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.IO;

namespace QuanLyDiem
{
    /// <summary>
    /// Interaction logic for QuanLySinhVien.xaml
    /// </summary>
    public partial class QuanLySinhVien : Window
    {
        public QuanLySinhVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemSinhVien f = new ThemSinhVien();
            this.Close();
            f.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SuaSinhVien f = new SuaSinhVien();
            this.Close();
            f.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XoaSinhVien f = new XoaSinhVien();
            this.Close();
            f.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Duong dan khong hop le");
                return;
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    p.Workbook.Properties.Title = "Danh sach sinh vien";
                    p.Workbook.Worksheets.Add("Test sheet");
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];
                    ws.Name = "Danh sach sinh vien";
                    ws.Cells.Style.Font.Size = 14;
                    ws.Cells.Style.Font.Name = "Times New Roman";
                    string[] arrColumnHeader =
                    {
                        "Mã sinh viên",
                        "Họ và tên",
                        "Ngày sinh",
                        "Giới tính",
                        "Địa chỉ",
                        "Số điện thoại",
                        "Email"
                    };
                    var countColHeader = arrColumnHeader.Count();
                    ws.Cells[1, 1].Value = "Danh sach sinh vien";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    int colIndex = 1;
                    int rowIndex = 2;
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                        var boder = cell.Style.Border;
                        boder.Bottom.Style =
                            boder.Top.Style =
                            boder.Left.Style =
                            boder.Right.Style = ExcelBorderStyle.Thin;
                        cell.Value = item;
                        colIndex++;
                    }
                    foreach (var item in db.Sinhviens)
                    {
                        colIndex = 1;
                        rowIndex++;
                        ws.Cells[rowIndex, colIndex++].Value = item.MaSv.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.TenSv.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.NgaySinh.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.GioiTinh.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.DiaChi.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.SoDt.ToString();
                        ws.Cells[rowIndex, colIndex++].Value = item.Email.ToString();
                    }
                    for (int i = 1; i <= ws.Dimension.End.Column; i++)
                    {
                        ws.Column(i).AutoFit();
                    }

                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuat thanh cong");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
