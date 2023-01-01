
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace fourbot.DataBase.SQL
{
    public class DB
    {
        
        MySqlConnection connection = new MySqlConnection("Server=mysql95.1gb.ru;Database=gb_4bot;Uid=gb_4bot;Pwd=S-XYF8m6ZgbC");
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeCoxnnection()
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
