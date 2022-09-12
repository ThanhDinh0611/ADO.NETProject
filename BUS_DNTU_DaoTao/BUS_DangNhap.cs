using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_DNTU_DaoTao;
using DAL_DNTU_DaoTao;
using System.Data;

namespace BUS_DNTU_DaoTao
{
    public class BUS_DangNhap
    {
        DAL_DangNhap dalDangNhap = new DAL_DangNhap();

        public BUS_DangNhap()
        {

        }

        public DataTable getUsers(string username)
        {
            return dalDangNhap.selectUsers(username);
        }
    }
}
