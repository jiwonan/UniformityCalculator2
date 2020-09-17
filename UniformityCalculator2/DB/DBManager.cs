using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Data;
using System.Runtime.CompilerServices;

namespace UniformityCalculator2.DB
{
    public class DBManager
    {
        public const string SERVER_PATH = "Server = 192.168.29.20; Port=3939; Database = letinar_uniform; Uid = user; Pwd = QqN3y29JrK1nPtlk#;Connection Timeout=15";

        private MySqlConnection conn = new MySqlConnection(SERVER_PATH);


        protected MySqlConnection GetConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Dispose();
                conn = new MySqlConnection(SERVER_PATH);
                conn.Open();
            }

            return conn;
        }

    }
}
