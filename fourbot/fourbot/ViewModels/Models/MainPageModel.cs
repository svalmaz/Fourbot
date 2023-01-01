using fourbot.DataBase.SQL;
using fourbot.DataBase.SQLite;
using fourbot.ViewModels.Classes;
using fourbot.ViewModels.ViewModel;
using fourbot.Views;
using MvvmCross.Commands;
using Plugin.Toast;
using System;
using fourbot.ViewModels.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows.Input;
using System.Net.Http;
using Xamarin.Forms;

namespace fourbot.ViewModels.Models
{
    class MainPageModel : MainViewModel
    {
        public MainPageModel() { 
        checkExchanges();

    }
        public async void checkExchanges()
        {
            list = db.selectTable();

            db_commands db1 = new db_commands();
            DataTable table = db1.getExchanges(list[0].maintoken.ToString());
            for (int i = 0; i <= 3; i++)
            {
                DataRow rr = table.Rows[0];
                string secret = rr[4].ToString();
                string api = rr[3].ToString();
                string name = rr[2].ToString();
               

                HttpClient httpClient = new HttpClient();
               string timeStamp;
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://testnet.binance.vision/api/v3/time"))
                {
                    var response = await httpClient.SendAsync(request);
                    string snapshot = await response.Content.ReadAsStringAsync();

                    string[] mainSnapshot1 = snapshot.Split(':');
                    string[] ss = mainSnapshot1[1].Split('}');
                    timeStamp = ss[0];


                }
                string req = "https://fapi.binance.com/fapi/v1/openOrders?recvWindow=5000&timestamp=" + timeStamp + "&signature=" + HmacSha256Digest("recvWindow=5000&timestamp=" + timeStamp, secret);
               

                using (var request = new HttpRequestMessage(new HttpMethod("GET"), req))
                {
                    httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", api);
                    var response = await httpClient.SendAsync(request);
                    string snapshot = await response.Content.ReadAsStringAsync();
                 
                    string[] snap1 = snapshot.Split('{');
                    
                    for(int e = 1; e<2; e++)
                    {
                        string[] valu = snap1[e].Split('"');

                        //   int column = Convert.ToInt32(valu[17]) * Convert.ToInt32();
                        CrossToastPopUp.Current.ShowToastMessage("as", Plugin.Toast.Abstractions.ToastLength.Long);

                        OrderList.Add(new Orders
                        {
                            symbol = valu[5],
                            price = valu[17].Length,
                            column = valu[25].Length,
                            
                            side = valu[49]
                        });
                        OnPropertyChanged();

                    }
                }


            }
            OnPropertyChanged();
        }
        public string HmacSha256Digest(string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            System.Security.Cryptography.HMACSHA256 cryptographer = new System.Security.Cryptography.HMACSHA256(keyBytes);

            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get { _logoutCommand = _logoutCommand ?? new MvxCommand(LogoutCom); return _logoutCommand; }

        }
        List<token> list = new List<token>();
        SQLiteDB db = new SQLiteDB();
        public void LogoutCom()
        {
            list = db.selectTable();
            if (list.Count > 0)
            {
                token tk = new token()
                { 
                    Id = 1,
                    maintoken = "0"
                };
                db.updateTable(tk);
              CrossToastPopUp.Current.ShowToastMessage("Успешный выход!", Plugin.Toast.Abstractions.ToastLength.Long);
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

            }
            else
            {

            }
        }
        private ICommand _goAddExch;
        public ICommand GoAddExch
        {
            get { _goAddExch = _goAddExch ?? new MvxCommand(AddExchange); return _goAddExch; }

        }
        public void AddExchange()
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new AddExchangePage());

        }
        


        private List<Orders> orderList = new List<Orders>();
        public List<Orders> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                if(orderList != value)
                {
                    orderList = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
