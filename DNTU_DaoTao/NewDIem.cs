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
    public partial class NewDIem : Form
    {
        string user;
        sql _sql = new sql();
        DataTable tb = new DataTable();
        CurrencyManager cm;
        public NewDIem(string username)
        {
            user = username;
            InitializeComponent();
        }

        //Button Save
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtTienganh.Text == "" || txtTinhoc.Text == "" || txtGiaoducTC.Text == "") {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin điểm!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Convert.ToInt32(txtTienganh.Text) < 0 || Convert.ToInt32(txtTienganh.Text) > 10)
            {
                MessageBox.Show("Điểm Tiếng anh phải nằm trong khoảng từ 0 đến 10!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Convert.ToInt32(txtTinhoc.Text) < 0 || Convert.ToInt32(txtTinhoc.Text) > 10)
            {
                MessageBox.Show("Điểm Tin học phải nằm trong khoảng từ 0 đến 10!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Convert.ToInt32(txtGiaoducTC.Text) < 0 || Convert.ToInt32(txtGiaoducTC.Text) > 10)
            {
                MessageBox.Show("Điểm Giáo dục thể chất phải nằm trong khoảng từ 0 đến 10!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string command = "insert into GRADE values('" + txtMaSV.Text + "'," + txtTienganh.Text + "," + txtTinhoc.Text + "," + txtGiaoducTC.Text + ")";
            _sql.query(command);
            updateTable();
            updateBinding();
            if (cm.Count == 0)
            {
                MessageBox.Show("Tất cả sinh viên đều đã được nhập điểm!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
        }

        //Hàm cập nhật lại table và bindings
        private void updateTable() {
            tb = _sql.loadTable("select STUDENTS.MaSV, HoTen, Tienganh, Tinhoc, GDTC, Round((TiengAnh+Tinhoc+GDTC)/3,1) TrungBinh from Grade right join STUDENTS on GRADE.MASV = STUDENTS.MASV where Tienganh is null");
            cm = (CurrencyManager)this.BindingContext[tb];
        }

        private void updateBinding() {
            cm.EndCurrentEdit();
            cm = (CurrencyManager)this.BindingContext[tb];
            txtMaSV.DataBindings.Clear();
            txtMaSV.DataBindings.Add("Text", tb, "MaSV");
            txtHoTen.DataBindings.Clear();
            txtHoTen.DataBindings.Add("Text", tb, "HoTen");
            txtTienganh.DataBindings.Clear();
            txtTienganh.DataBindings.Add("Text", tb, "Tienganh");
            txtTinhoc.DataBindings.Clear();
            txtTinhoc.DataBindings.Add("Text", tb, "Tinhoc");
            txtGiaoducTC.DataBindings.Clear();
            txtGiaoducTC.DataBindings.Add("Text", tb, "GDTC");
            lblDiemTB.DataBindings.Clear();
            lblDiemTB.DataBindings.Add("Text", tb, "TrungBinh");
            dgvSinhVien.DataSource = tb;
        }

        //Khởi tạo Form
        private void NewDIem_Load(object sender, EventArgs e)
        {
            lblUser.Text = user;
            updateTable();
            if (cm.Count == 0) {
                MessageBox.Show("Tất cả sinh viên đều đã được nhập điểm!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            txtMaSV.Enabled = false;
            txtMaSV.DataBindings.Add("Text", tb, "MaSV");
            txtHoTen.DataBindings.Add("Text", tb, "HoTen");
            txtTienganh.DataBindings.Add("Text", tb, "Tienganh");
            txtTinhoc.DataBindings.Add("Text", tb, "Tinhoc");
            txtGiaoducTC.DataBindings.Add("Text", tb, "GDTC");
            lblDiemTB.DataBindings.Add("Text", tb, "TrungBinh");
            dgvSinhVien.DataSource = tb;

        }

        private void txtTinhoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaoducTC_TextChanged(object sender, EventArgs e)
        {

        }

        //Nhấn phím Enter ở ô Giáo dục TC để click button Save
        private void txtGiaoducTC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnSave.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void txtHoTen_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆ
fFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTu
UùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ\s]+$";
            if (Regex.IsMatch(txtHoTen.Text, reg))
            {
                epError.SetError(txtHoTen, "");
            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtHoTen, "Họ tên không được chứa số hoặc ký tự đặc biệt!");
            }
        }

        private void txtMaSV_Validating(object sender, CancelEventArgs e)
        {
            string reg = "^SV[0-9]{2,4}$";
            if (Regex.IsMatch(txtMaSV.Text, reg))
            {
                epError.SetError(txtMaSV, "");
            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtMaSV, "Mã sinh viên không đúng định dạng! (SV + 2-4 chữ số)");
            }
        }

        private void txtTienganh_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^(([0-9](\.[0-9])?)|10)$";
            if (Regex.IsMatch(txtTienganh.Text, reg))
            {
                epError.SetError(txtTienganh, "");
                if (txtTienganh.Text != "" && txtTinhoc.Text != "" && txtGiaoducTC.Text != "")
                    lblDiemTB.Text = ((Convert.ToDecimal(txtTienganh.Text) + Convert.ToDecimal(txtTinhoc.Text) + Convert.ToDecimal(txtGiaoducTC.Text)) / 3).ToString();

            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtTienganh, "Điểm không hợp lệ");
            }
        }

        private void txtTinhoc_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^(([0-9](\.[0-9])?)|10)$";
            if (Regex.IsMatch(txtTinhoc.Text, reg))
            {
                epError.SetError(txtTinhoc, "");
                if (txtTienganh.Text != "" && txtTinhoc.Text != "" && txtGiaoducTC.Text != "")
                    lblDiemTB.Text = ((Convert.ToDecimal(txtTienganh.Text) + Convert.ToDecimal(txtTinhoc.Text) + Convert.ToDecimal(txtGiaoducTC.Text)) / 3).ToString();

            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtTinhoc, "Điểm không hợp lệ");
            }
        }

        private void txtGiaoducTC_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^(([0-9](\.[0-9])?)|10)$";
            if (Regex.IsMatch(txtGiaoducTC.Text, reg))
            {
                epError.SetError(txtGiaoducTC, "");
                if (txtTienganh.Text != "" && txtTinhoc.Text != "" && txtGiaoducTC.Text != "")
                    lblDiemTB.Text = ((Convert.ToDecimal(txtTienganh.Text) + Convert.ToDecimal(txtTinhoc.Text) + Convert.ToDecimal(txtGiaoducTC.Text)) / 3).ToString();

            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtGiaoducTC, "Điểm không hợp lệ");
            }
        }
    }
}
