using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNTU_DaoTao
{
    public partial class DangNhap : Form
    {
        sql _sql = new sql();
        Encoding z = Encoding.UTF8;

        public DangNhap()
        {
            InitializeComponent();
        }

        //Đóng ứng dụng
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Chữ username biến mất và hiện ra khi nhập
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username") {
                txtUsername.Text = "";
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "") {
                txtUsername.Text = "Username";
            }
        }

        //Ký tự mật khẩu biến mật và hiện ra khi nhập
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password") {
                txtPassword.Text = "";
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "") {
                txtPassword.Text = "Password";
            }
        }

        //Xử lý sự kiện nhấn Enter
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }


        //Xử lý khi nhấn Login
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputUsername = txtUsername.Text;
            string inputPassword = txtPassword.Text;
            
           
            DataTable tb = _sql.loadTable("select * from USERS where username = '" + inputUsername + "'");
            if (tb.Rows.Count == 0)
            {
                MessageBox.Show("Tài khoản không tồn tại, xin kiểm tra lại thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (inputPassword != tb.Rows[0][1].ToString())
            {
                MessageBox.Show("Mật khẩu không chính xác, xin kiểm tra lại thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (tb.Rows[0][2].ToString() == "Quản trị viên")
            {
                QLSinhVien frm = new QLSinhVien(inputUsername);
                this.Visible = false;
                frm.ShowDialog();
                this.Close();
            }
            else if (tb.Rows[0][2].ToString() == "Giáo viên") {
                QLDiem frm = new QLDiem(inputUsername);
                this.Visible = false;
                frm.ShowDialog();
                this.Close();
            }
        }

        //Xử lý khi nhấn Hướng dẫn sử dụng
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Introduction frm = new Introduction();
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }
    }
}
