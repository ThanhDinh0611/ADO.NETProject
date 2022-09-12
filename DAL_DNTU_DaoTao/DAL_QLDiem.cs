using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_DNTU_DaoTao;
using System.Data;

namespace DAL_DNTU_DaoTao
{
    public class DAL_QLDiem : connectDB
    {
        public DAL_QLDiem() {

        }

        public DataTable insertGrade(DTO_QLDiem QLDiem) {
            string command = string.Format("insert into GRADE(MASV, Tienganh, Tinhoc, GDTC) values('{0}',{1},{2},{3})", 
                QLDiem.Masv, QLDiem.Tienganh, QLDiem.Tienganh, QLDiem.GDTC1);
            return _query(command);
        }

        public DataTable selectGrade(string command) {
            return _query(command);
        }

        public DataTable updateGrade(DTO_QLDiem QLDiem) {
            string command = string.Format("update GRADE set Tienganh = {0}, Tinhoc = {1}, GDTC = {2} where MaSV = '{3}'",
                QLDiem.Tienganh, QLDiem.Tinhoc, QLDiem.GDTC1, QLDiem.Masv);
            return _query(command);
        }

        public DataTable deleteGrade(string MaSV)
        {
            string command = string.Format("delete GRADE where MaSV = '{0}'", MaSV);
            return _query(command);
        }
    }
}
