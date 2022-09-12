using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAL_DNTU_DaoTao
{
    public class DAL_DangNhap : connectDB
    {
        public DAL_DangNhap() {

        }

        public DataTable selectUsers(string username) {
            DataTable tb = new DataTable();
            string command = "Select * from USERS where username = '"+username+"'";
            tb = _query(command);
            return tb;
        }
    }
}
