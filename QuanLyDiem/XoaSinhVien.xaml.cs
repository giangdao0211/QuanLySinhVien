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
    /// Interaction logic for XoaSinhVien.xaml
    /// </summary>
    public partial class XoaSinhVien : Window
    {
        public XoaSinhVien()
        {
            InitializeComponent();
        }
        QldiemContext db = new();
        QuanLySinhVien f = new QuanLySinhVien();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = db.Sinhviens.SingleOrDefault(t => t.MaSv.Equals(txtMa.Text));
                if (query != null)
                {
                    MessageBoxResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Thông Báo", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (rs == MessageBoxResult.Yes)
                    {
                        db.Sinhviens.Remove(query);
                        db.SaveChanges();
                        MessageBox.Show("Xóa thành công", "Thông Báo", MessageBoxButton.OK);
                        f.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không tồn tại sinh viên này", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    f.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa" + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
