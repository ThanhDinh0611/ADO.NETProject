using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAL_DNTU_DaoTao
{
    public class connectDB
    {
        private SqlDataAdapter _sqlAdapter;
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private string _connectionString = "Data Source = .; Initial Catalog = DNTU_DaoTao; User id = sa; Password = 123456";

        public connectDB() {

        }

        public DataTable _query(string command) {
            DataTable tb = new DataTable();
            _sqlAdapter = new SqlDataAdapter(command, _connectionString);
            try
            {
                _sqlAdapter.Fill(tb);
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString());
            }
            return tb;
        }
    }
}
