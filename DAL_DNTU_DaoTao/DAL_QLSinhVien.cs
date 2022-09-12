using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO_DNTU_DaoTao;

namespace DAL_DNTU_DaoTao
{
    public class DAL_QLSinhVien : connectDB
    {
        public DAL_QLSinhVien() {

        }

        public DataTable insertStudents(DTO_QLSinhVien QLSinhVien) {
            string command = "insert into STUDENTS values('"+QLSinhVien.Masv+"',N'"+QLSinhVien.Hoten+"','"+QLSinhVien.Email+"','"+QLSinhVien.Sdt
                +"',"+QLSinhVien.Gioitinh+",N'"+QLSinhVien.Diachi+"','"+QLSinhVien.Hinh+"')";
            return _query(command);
        }

        public DataTable selectStudents()
        {
            string command = "select * from STUDENTS";
            return _query(command);
        }

        public DataTable updateStudents(DTO_QLSinhVien QLSinhVien)
        {
            string command = string.Format("update STUDENTS set HoTen=N'{0}', Email='{1}', SoDT='{2}', GioiTinh={3}, DiaChi=N'{4}', Hinh='{5}' where MaSV ='{6}'", 
                QLSinhVien.Hoten, QLSinhVien.Email, QLSinhVien.Sdt, QLSinhVien.Gioitinh, QLSinhVien.Diachi, QLSinhVien.Hinh, QLSinhVien.Masv);
            return _query(command);
        }

        public DataTable deleteStudents(string MaSV) {
            string command = string.Format("delete STUDENTS where MaSV='{0}'", MaSV);
            return _query(command);
        }
    }
}
