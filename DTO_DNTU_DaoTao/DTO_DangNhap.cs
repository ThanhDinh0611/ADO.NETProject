using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_DNTU_DaoTao
{
    public class DTO_DangNhap
    {
        private string username;
        private string password;
        private string role;

        public DTO_DangNhap() {

        }

        public DTO_DangNhap(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }
    }
}
