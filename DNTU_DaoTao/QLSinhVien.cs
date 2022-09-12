using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace DNTU_DaoTao
{
    public partial class QLSinhVien : Form
    {
        //Khởi tạo các giá trị cần thiết 
        sql _sql = new sql();
        DataTable tb = new DataTable();
        CurrencyManager cm;
        string user;

        //Constructor nhận giá trị username
        public QLSinhVien(string username)
        {
            user = username;
            InitializeComponent();
        }

        //Cập nhật bảng và các bindings
        private void updateTable()
        {
            tb = _sql.loadTable("select * from STUDENTS");
            cm = (CurrencyManager)this.BindingContext[tb];
        }

        private void updateBinding()
        {
            cm.EndCurrentEdit();
            cm = (CurrencyManager)this.BindingContext[tb];
            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", tb, "HoTen");
            txtMaSV.DataBindings.Clear();
            txtMaSV.DataBindings.Add("Text", tb, "MaSV");
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("Text", tb, "Email");
            txtSoDT.DataBindings.Clear();
            txtSoDT.DataBindings.Add("Text", tb, "SoDT");
            txtDiachi.DataBindings.Clear();
            txtDiachi.DataBindings.Add("Text", tb, "DiaChi");
            picSinhVien.DataBindings.Clear();
            picSinhVien.DataBindings.Add("ImageLocation", tb, "Hinh");
            if (Convert.ToByte(tb.Rows[cm.Position][4]) == 0)
            {
                radNam.PerformClick();
            }
            else
                radNu.PerformClick();

            dgvSinhVien.DataSource = tb;

        }

        //Khởi tạo form
        private void QLSinhVien_Load(object sender, EventArgs e)
        {
            updateTable();
            txtHoTen.DataBindings.Add("Text", tb, "HoTen");
            txtMaSV.DataBindings.Add("Text", tb, "MaSV");
            txtEmail.DataBindings.Add("Text", tb, "Email");
            txtSoDT.DataBindings.Add("Text", tb, "SoDT");
            txtDiachi.DataBindings.Add("Text", tb, "DiaChi");
            picSinhVien.DataBindings.Add("ImageLocation", tb, "Hinh");
            if (Convert.ToByte(tb.Rows[cm.Position][4]) == 0)
                radNam.PerformClick();
            else
                radNu.PerformClick();
            dgvSinhVien.DataSource = tb;
            lblUser.Text = user;
            txtMaSV.Enabled = false;
        }

        //Button delete
        private void btnDel_Click(object sender, EventArgs e)
        {
            //Xóa bảng sinh viên ở bảng Grade trước
            _sql.query("delete GRADE where MaSV = '" + txtMaSV.Text + "'");
            string command = "delete STUDENTS where MaSV = '" + txtMaSV.Text + "'";
            _sql.query(command);
            updateTable();
            updateBinding();
        }

        //Xử lý cập nhật giới tính thủ công
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cm.Position == tb.Rows.Count)
            {
                radNam.Checked = false;
                radNu.Checked = false;
                return;
            }
            if (Convert.ToByte(tb.Rows[cm.Position][4]) == 0)
                radNam.PerformClick();
            else
                radNu.PerformClick();
        }

        //Button Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Bạn chưa chọn giới tính!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] s = picSinhVien.ImageLocation.Split('/');
            if(s.Length == 1)
                s = s[0].Split('\\');
            string file = "Resources/" + s[s.Length - 1];
            string command = "update STUDENTS set HoTen=N'" +
                txtHoTen.Text + "', Email='" + txtEmail.Text + "', SoDT='" + txtSoDT.Text + "', GioiTinh=" +
                ((radNam.Checked) ? "0" : "1") + ", DiaChi=N'" + txtDiachi.Text + "', Hinh='" + file + "' where MaSV='" +
                txtMaSV.Text + "'";
            _sql.query(command);
            updateTable();
            updateBinding();
        }

        //Button New
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewSinhvIen frm = new NewSinhvIen(user);
            frm.ShowDialog();
            updateTable();
            updateBinding();
        }

        //Chọn ảnh
        private void picSinhVien_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|All Files (*.*)|*.*";
            dialog.Title = "Choose an image...";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picSinhVien.ImageLocation = dialog.FileName;
            }
        }

        private void txtHoTen_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ
fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu
UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ\s]+$";
            if (Regex.IsMatch(txtHoTen.Text, reg))
            {
                erError.SetError(txtHoTen, "");
            }
            else {
                e.Cancel = true;
                erError.SetError(txtHoTen, "Họ tên không được chứa số hoặc ký tự đặc biệt!");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-.]+\.[a-zA-Z]+$";
            if (Regex.IsMatch(txtEmail.Text, reg))
            {
                erError.SetError(txtEmail, "");
            }
            else {
                e.Cancel = true;
                erError.SetError(txtEmail, "Email không đúng định dạng!");
            }
        }

        private void txtSoDT_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^0[0-9]{9}$";
            if (Regex.IsMatch(txtSoDT.Text, reg))
            {
                erError.SetError(txtSoDT, "");
            }
            else {
                e.Cancel = true;
                erError.SetError(txtSoDT, "Số điện thoại không đúng định dạng!");
            }
        }
    }
}

//adc@gmail.com
//adc@gmail-com
