using fourbot.DataBase.SQLite;
using MySqlConnector;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace fourbot.DataBase.SQL
{
    public class db_commands 
    {
        DB db = new DB();
        SQLiteDB sqldb = new SQLiteDB();
        DataTable table = new DataTable();
        MySqlDataAdapter adapter = new MySqlDataAdapter();

        public string RegisterCommand(string log, string pass, string ema)
        {
            db.openConnection();
            MySqlCommand com = new MySqlCommand("SELECT * FROM `users` WHERE `login` ='" + log+"'", db.getConnection());
            adapter.SelectCommand = com;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                
                string password = hashpass.hashPassword(pass);
                string token = hashpass.hashPassword(log);
                MySqlDataAdapter adapter1 = new MySqlDataAdapter();
                MySqlDataAdapter adapter2 = new MySqlDataAdapter();
                MySqlDataAdapter adapter3 = new MySqlDataAdapter();
                MySqlDataAdapter adapter4 = new MySqlDataAdapter();
                MySqlDataAdapter adapter5 = new MySqlDataAdapter();
                MySqlCommand com1 = new MySqlCommand("INSERT INTO `users`(`login`,`pass`,`email`,`token`) VALUES ('" + log + "','" + password + "','" + ema + "','"+token+"')", db.getConnection());
                MySqlCommand com2 = new MySqlCommand("INSERT INTO `exchanges`(`name`,`api`,`secret`,`token`) VALUES ('Binance','0','0','" + token + "')", db.getConnection());
                MySqlCommand com3 = new MySqlCommand("INSERT INTO `exchanges`(`name`,`api`,`secret`,`token`) VALUES ('Bybit','0','0','" + token + "')", db.getConnection());
                MySqlCommand com4 = new MySqlCommand("INSERT INTO `exchanges`(`name`,`api`,`secret`,`token`) VALUES ('Kucoin','0','0','" + token + "')", db.getConnection());
                MySqlCommand com5 = new MySqlCommand("INSERT INTO `exchanges`(`name`,`api`,`secret`,`token`) VALUES ('Finandy','0','0','" + token + "')", db.getConnection());
                adapter1.SelectCommand = com1;
                adapter2.SelectCommand = com2;  
                adapter3.SelectCommand = com3;
                adapter4.SelectCommand = com4;
                adapter5.SelectCommand = com5;

              
                adapter1.Fill(table);
                adapter2.Fill(table);
                adapter3.Fill(table);
                adapter4.Fill(table);

                adapter5.Fill(table);
                return ("Регистрация прошло успешно!");
            }
            else
            {
                return ("Данный логин занят, выберите другой");
             }
            
        }
        public  string LoginCommand(string log, string pass)
        {
            string password = hashpass.hashPassword(pass);
            db.openConnection();
            MySqlCommand com = new MySqlCommand("SELECT * FROM `users` WHERE `login` = '" + log+"' AND `pass` ='"+password+"'", db.getConnection());
            adapter.SelectCommand = com;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                DataRow rr = table.Rows[0];
                string token1 = rr[4].ToString();

                token tk = new token()
                {
                    maintoken = token1
                };
                try
                {
                    token tk1 = new token()
                    {   
                        Id = 1,
                        maintoken = token1
                    };

                    sqldb.updateTable(tk1);
                    return ("Успешно");
                }
                catch
                {
                    sqldb.insertIntoTable(tk);
                    return ("Успешно");
                }

            }
            else
            {
                return ("Логин или пароль были введены неправильно!");
            }
        }
        public string UpdateExchanges(string token, string name, string api, string secret)
        {
            try
            {
                db.openConnection();
                MySqlCommand com = new MySqlCommand("UPDATE `exchanges` SET `api`='" + api + "',`secret`='" + secret + "' WHERE `token` ='" + token + "' AND `name` = '" + name + "'", db.getConnection());
                adapter.SelectCommand = com;
                adapter.Fill(table);
                db.closeCoxnnection();
                return ("Успешно изменено!");
            }
            catch
            {
                db.closeCoxnnection();
                return ("Ошибка, свяжитесь с тех поддержкой!");
            }
        }
        public DataTable getExchanges(string token)
        {
            db.openConnection();
            MySqlCommand com = new MySqlCommand("SELECT * FROM `exchanges`  WHERE `token` ='" + token + "'", db.getConnection());
            adapter.SelectCommand = com;
            adapter.Fill(table);
            db.closeCoxnnection();
            return table;


        }
    }
}
