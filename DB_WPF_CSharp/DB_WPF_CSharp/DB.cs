using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_WPF_CSharp
{
    class DB
    {
        MySqlConnection connection = 
            new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=messenger;SSL Mode=None");

        private static DB _instance;

        public static DB GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DB();
            }
            return _instance;
        }

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
