using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace DNTU_DaoTao
{
    class sql
    {
        string _connectionString = "Data Source=.; Initial Catalog=DNTU_DaoTao; User ID=sa; password=123456";
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataAdapter _sqlAdapter;

        public void openDB() {
            _sqlConnection = new SqlConnection(_connectionString);
            if (_sqlConnection.State == ConnectionState.Closed)
                _sqlConnection.Open();
        }

        public void closeDB() {
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }

        public void query(string command) {
            try
            {
                openDB();
                _sqlCommand = new SqlCommand(command, _sqlConnection);
                _sqlCommand.ExecuteNonQuery();
                closeDB();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                closeDB();
            }
        }

        public DataTable loadTable(string command) {
            try
            {
                DataTable tb = new DataTable();
                _sqlAdapter = new SqlDataAdapter(command, _connectionString);
                _sqlAdapter.Fill(tb);
                return tb;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}
