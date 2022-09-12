using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_DNTU_DaoTao
{
    public class DTO_QLSinhVien
    {
        private string masv;
        private string hoten;
        private string email;
        private string sdt;
        private char gioitinh;
        private string diachi;
        private string hinh;

        public DTO_QLSinhVien() {

        }

        public DTO_QLSinhVien(string masv, string hoten, string email, string sdt, char gioitinh, string diachi, string hinh)
        {
            this.masv = masv;
            this.hoten = hoten;
            this.email = email;
            this.sdt = sdt;
            this.gioitinh = gioitinh;
            this.diachi = diachi;
            this.hinh = hinh;
        }

        public string Masv
        {
            get
            {
                return masv;
            }

            set
            {
                masv = value;
            }
        }

        public string Hoten
        {
            get
            {
                return hoten;
            }

            set
            {
                hoten = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Sdt
        {
            get
            {
                return sdt;
            }

            set
            {
                sdt = value;
            }
        }

        public char Gioitinh
        {
            get
            {
                return gioitinh;
            }

            set
            {
                gioitinh = value;
            }
        }

        public string Diachi
        {
            get
            {
                return diachi;
            }

            set
            {
                diachi = value;
            }
        }

        public string Hinh
        {
            get
            {
                return hinh;
            }

            set
            {
                hinh = value;
            }
        }
    }
}
