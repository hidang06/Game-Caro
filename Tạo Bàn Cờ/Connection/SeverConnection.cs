using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tạo_Bàn_Cờ.Connection
{
    class SeverConnection
    {
        public static string stringConnection = @"Data Source=DESKTOP-SFL7UVG\SQLEXPRESS;Initial Catalog=GAME_CARO;Integrated Security=True";




        public static DataTable executeSQL(string sql)
        {
            SqlConnection connection = new SqlConnection();
            SqlDataAdapter adapter = default(SqlDataAdapter);
            DataTable dt = new DataTable();
            try
            {
                connection.ConnectionString=stringConnection;
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occured: " + ex.Message, "SQL Sever Connection Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            return dt;
        }
    }
}
