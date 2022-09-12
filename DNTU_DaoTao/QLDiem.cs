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
    public partial class QLDiem : Form
    {
        //Khởi tạo các giá trị cần thiết
        sql _sql = new sql();
        DataTable tb = new DataTable();
        CurrencyManager cm;
        DataTable top3 = new DataTable();
        string user;
        
        //Constructor nhận giá trị username
        public QLDiem(string username)
        {
            user = username;
            InitializeComponent();
        }

        //Cập nhật lại bảng và bindings
        private void updateTable()
        {
            tb = _sql.loadTable("select Grade.MASV, Hoten, Tienganh, Tinhoc, GDTC, (Tienganh+Tinhoc+GDTC)/3 Trungbinh from Grade inner join STUDENTS on GRADE.MASV = STUDENTS.MASV");
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
            txtTienganh.DataBindings.Clear();
            txtTienganh.DataBindings.Add("Text", tb, "Tienganh");
            txtTinhoc.DataBindings.Clear();
            txtTinhoc.DataBindings.Add("Text", tb, "Tinhoc");
            txtGiaoducTC.DataBindings.Clear();
            txtGiaoducTC.DataBindings.Add("Text", tb, "GDTC");
            lblDiemTB.DataBindings.Clear();
            lblDiemTB.DataBindings.Add("Text", tb, "Trungbinh");
            dgvTop3.DataSource = _sql.loadTable("select top 3 Grade.MASV, Hoten, Tienganh, Tinhoc, GDTC, round((Tienganh+Tinhoc+GDTC)/3,1) Trungbinh from Grade inner join STUDENTS on GRADE.MASV = STUDENTS.MASV order by Trungbinh desc");

        }

        //Khởi tạo Form
        private void QLDiem_Load(object sender, EventArgs e)
        {
            updateTable();
            txtHoTen.DataBindings.Add("Text", tb, "HoTen");
            txtMaSV.DataBindings.Add("Text", tb, "MaSV");
            txtTienganh.DataBindings.Add("Text", tb, "Tienganh");
            txtTinhoc.DataBindings.Add("Text", tb, "Tinhoc");
            txtGiaoducTC.DataBindings.Add("Text", tb, "GDTC");
            lblDiemTB.DataBindings.Add("Text", tb, "Trungbinh");
            top3 = _sql.loadTable("select top 3 Grade.MASV, Hoten, Tienganh, Tinhoc, GDTC, round((Tienganh+Tinhoc+GDTC)/3,1) Trungbinh from Grade inner join STUDENTS on GRADE.MASV = STUDENTS.MASV order by Trungbinh desc");
            dgvTop3.DataSource = top3;
            lblUser.Text = user;
            txtMaSV.Enabled = false;
        }

        //Button Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string maSV = txtSearch.Text;
            int count = -1;
            foreach (DataRow row in tb.Rows)
            {
                count++;
                if (maSV == row[0].ToString())
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                MessageBox.Show("Mã sinh viên không tồn tại hoặc chưa được nhập điểm!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cm.Position = count;
        }

        //Các Button tiến lùi
        private void btnFirst_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            cm.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            cm.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            cm.Position = cm.Count - 1;
        }

        //Enter ở textbox Search để nhấn Button Search
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        //Button Delete
        private void btnDel_Click(object sender, EventArgs e)
        {
            string command = "delete GRADE where MaSV = '" + txtMaSV.Text + "'";
            _sql.query(command);
            updateTable();
            updateBinding();
        }

        //Button Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Kiểm tra giá trị trước khi cập nhật
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

            string command = "update GRADE set Tienganh = " + txtTienganh.Text + ", Tinhoc = " +
                txtTinhoc.Text + ", GDTC = " + txtGiaoducTC.Text + " where MaSV = '" + txtMaSV.Text + "'";
            _sql.query(command);
            updateTable();
            updateBinding();
        }

        //Bảng Top3 không được bind với các Textbox vì thế phải cập nhật thủ công
        private void dgvTop3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maSV = top3.Rows[e.RowIndex][0].ToString();
            int count = -1;
            foreach (DataRow r in tb.Rows)
            {
                count++;
                if (r[0].ToString() == maSV)
                    break;
            }
            cm.Position = count;
        }

        //Button New
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewDIem frm = new NewDIem(user);
            frm.ShowDialog();
            updateTable();
            updateBinding();
        }

        private void txtTinhoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaoducTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_Validating(object sender, CancelEventArgs e)
        {
            string reg = "^(SV[0-9]{2,4}|.?)$";
            if (Regex.IsMatch(txtSearch.Text, reg)){
                epError.SetError(txtSearch, "");
            }
            else {
                e.Cancel = true;
                epError.SetError(txtSearch, "Mã sinh viên không đúng định dạng! (SV + 2-4 chữ số)");
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
            if (Regex.IsMatch(txtMaSV.Text, reg)){
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
                    lblDiemTB.Text = ((Convert.ToDecimal(txtTienganh.Text)+Convert.ToDecimal(txtTinhoc.Text)+Convert.ToDecimal(txtGiaoducTC.Text))/3).ToString();
            }
            else {
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
