using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL_DNTU_DaoTao;
using DTO_DNTU_DaoTao;

namespace BUS_DNTU_DaoTao
{
    public class BUS_QLSinhVien
    {
        DAL_QLSinhVien dalQLSinhVien = new DAL_QLSinhVien();
        public BUS_QLSinhVien()
        {

        }

        public DataTable createStudents(DTO_QLSinhVien QLSinhVien) {
            return dalQLSinhVien.insertStudents(QLSinhVien);
        }

        public DataTable getStudents()
        {
            return dalQLSinhVien.selectStudents();
        }

        public DataTable updateStudents(DTO_QLSinhVien QLSinhVien)
        {
            return dalQLSinhVien.updateStudents(QLSinhVien);
        }

        public DataTable deleteStudents(string MaSV) {
            return dalQLSinhVien.deleteStudents(MaSV);
        }
    }
}
