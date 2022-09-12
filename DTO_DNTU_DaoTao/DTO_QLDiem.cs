using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_DNTU_DaoTao
{
    public class DTO_QLDiem
    {
        private int id;
        private string masv;
        private float tienganh;
        private float tinhoc;
        private float GDTC;

        public DTO_QLDiem() {

        }

        public DTO_QLDiem(string masv, float tienganh, float tinhoc, float GDTC) {
            this.masv = masv;
            this.tienganh = tienganh;
            this.tinhoc = tinhoc;
            this.GDTC = GDTC;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
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

        public float Tienganh
        {
            get
            {
                return tienganh;
            }

            set
            {
                tienganh = value;
            }
        }

        public float Tinhoc
        {
            get
            {
                return tinhoc;
            }

            set
            {
                tinhoc = value;
            }
        }

        public float GDTC1
        {
            get
            {
                return GDTC;
            }

            set
            {
                GDTC = value;
            }
        }
    }
}
