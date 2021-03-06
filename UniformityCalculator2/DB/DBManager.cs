﻿using MySql.Data.MySqlClient;
using System.Data;

namespace UniformityCalculator2.DB
{
    public class DBManager
    {
        public const string SERVER_PATH = "Server = 192.168.29.20; Port=3939; Database = letinar_uniform; Uid = user; Pwd = QqN3y29JrK1nPtlk#;Connection Timeout=15";

        private MySqlConnection conn = new MySqlConnection(SERVER_PATH);

        protected MySqlConnection GetConnection()
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
            else if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            /*if (conn.State != ConnectionState.Open)
            {
                conn.Dispose();
                conn = new MySqlConnection(SERVER_PATH);
                conn.Open();
            }*/

            return conn;
        }

    }
}
