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
    public class BUS_QLDiem
    {
        DAL_QLDiem dalQLDiem = new DAL_QLDiem();
        public BUS_QLDiem() {

        }
        
        public DataTable createGrade(DTO_QLDiem QLDiem)
        {
            return dalQLDiem.insertGrade(QLDiem);
        }        

        public DataTable getGrade(string command)
        {
            return dalQLDiem.selectGrade(command);
        }

        public DataTable updateGrade(DTO_QLDiem QLDiem)
        {
            return dalQLDiem.updateGrade(QLDiem);
        }

        public DataTable deleteGrade(string MaSV)
        {
            return dalQLDiem.deleteGrade(MaSV);
        }
    }
}
