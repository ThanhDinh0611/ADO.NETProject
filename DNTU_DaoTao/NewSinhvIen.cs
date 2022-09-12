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
    public partial class NewSinhvIen : Form
    {
        //Khởi tạo các giá trị cần thiết
        sql _sql = new sql();
        string imageFile;
        string user;

        //Constructor nhận vào giá trị username
        public NewSinhvIen(string username)
        {
            user = username;
            InitializeComponent();
        }

        //Button Clear
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            txtSoDT.Text = "";
            txtDiachi.Text = "";
            txtEmail.Text = "";
            radNam.Checked = false;
            radNu.Checked = false;
            picSinhVien.ImageLocation = "Resources/add.png";
            picSinhVien.SizeMode = PictureBoxSizeMode.Zoom;
        }

        //Button Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Validate
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Bạn chưa chọn giới tính!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int gioiTinh = ((radNam.Checked) ? 0 : 1);
            string command = "insert STUDENTS values('" + txtMaSV.Text + "',N'" + txtHoTen.Text
                + "','" + txtEmail.Text + "','" + txtSoDT.Text + "'," + gioiTinh + ",N'" + txtDiachi.Text + "','" + imageFile + "')";
            _sql.query(command);
            this.Close();
        }

        //Chọn ảnh
        private void picSinhVien_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|All Files (*.*)|*.*";
            dialog.Title = "Choose an image...";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picSinhVien.Image = new Bitmap(dialog.FileName);
                string[] s = dialog.FileName.Split('\\');
                imageFile = "Resources/" + s[s.Length - 1];
            }
        }

        //Khởi tạo form
        private void NewSinhvIen_Load(object sender, EventArgs e)
        {
            lblUser.Text = user;
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
                epError.SetError(txtMaSV, "Mã sinh viên không hơp lệ! (SV + 2-4 chữ số)");
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

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-.]+\.[a-zA-Z]+$";
            if (Regex.IsMatch(txtEmail.Text, reg))
            {
                epError.SetError(txtEmail, "");
            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtEmail, "Email không đúng định dạng!");
            }
        }

        private void txtSoDT_Validating(object sender, CancelEventArgs e)
        {
            string reg = @"^0[0-9]{9}$";
            if (Regex.IsMatch(txtSoDT.Text, reg))
            {
                epError.SetError(txtSoDT, "");
            }
            else
            {
                e.Cancel = true;
                epError.SetError(txtSoDT, "Số điện thoại không đúng định dạng!");
            }
        }
    }
}
