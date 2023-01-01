using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace fourbot.DataBase.SQL
{
    class hashpass
    {
        public static string hashPassword(string pass)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(pass);
            byte[] hash = md5.ComputeHash(b);   
            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }
            return Convert.ToString(sb);
        }
    }
}
