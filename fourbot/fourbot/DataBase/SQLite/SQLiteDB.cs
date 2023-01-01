using Plugin.Toast;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace fourbot.DataBase.SQLite
{
    public class SQLiteDB
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.CreateTable<token>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
           
                return false;
            }
        }
        //Add or Insert Operation  

        public bool insertIntoTable(token person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Insert(person);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
               
                return false;
            }
        }
        public List<token> selectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    return connection.Table<token>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
               
                return null;
            }
        }
        //Edit Operation  

        public bool updateTable(token person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<token>("UPDATE token set maintoken=?  WHERE Id=?",  person.maintoken, person.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
               
                return false;
            }
        }
        //Delete Data Operation  
   
        public bool removeTable(token person)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Delete(person);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                CrossToastPopUp.Current.ShowToastMessage(ex.ToString(), Plugin.Toast.Abstractions.ToastLength.Long);

                return false;
            }
        }
        //Select Operation  

        public bool selectTable(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Persons.db")))
                {
                    connection.Query<token>("SELECT * FROM Person Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                
                return false;
            }
        }
    }
}

