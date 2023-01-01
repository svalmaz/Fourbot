using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
namespace fourbot.DataBase.SQLite
{
    public class token
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string maintoken { get; set; }
    }
}
