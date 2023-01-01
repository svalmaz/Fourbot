using fourbot.DataBase.SQLite;
using fourbot.Views;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fourbot
{
    public partial class MainPage : ContentPage
    {
        List<token> list = new List<token>();
        SQLiteDB db = new SQLiteDB();
        public MainPage()
        {
            InitializeComponent();
            list = db.selectTable();
            if (list.Count > 0)
            {
                if(list[0].maintoken.Length >2)
                {
                    CrossToastPopUp.Current.ShowToastMessage(list[0].maintoken.ToString(), Plugin.Toast.Abstractions.ToastLength.Long);
                    Navigation.PushModalAsync(new FirstPage());
                }
                else
                {

                }
                
            }
            else
            {

            }
        }
    }
}
