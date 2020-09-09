using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniformityViewer2
{
    public class DBManager
    {
        static string SERVER_PATH = "Server = 192.168.29.20; Port=3939; Database = letinar_uniform; Uid = user; Pwd = QqN3y29JrK1nPtlk#;Connection Timeout=15";

        private static MySqlConnection conn = new MySqlConnection(SERVER_PATH);

        public static MySqlConnection GetConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
            else if(conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }

            return conn;
        }
    }
}
