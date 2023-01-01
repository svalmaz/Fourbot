using fourbot.DataBase.SQL;
using fourbot.DataBase.SQLite;
using fourbot.ViewModels.ViewModel;
using fourbot.Views;
using MvvmCross.Commands;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace fourbot.ViewModels.Models
{
    class AddExchangeModel : MainViewModel
    {
        private string exchange;
        public string Exchange
        {
            get { return exchange; }
            set
            {
                exchange = value;
                OnPropertyChanged();
            }
        }
        private string api;
        public string Api
        {
            get { return api; }
            set
            {
                api = value;
                OnPropertyChanged();
            }
        }
        private string secret;
        public string Secret
        {
            get { return secret; }
            set
            {
                secret = value;
                OnPropertyChanged();
            }
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
        
        public IList<string> ExchangeList
        {
            get
            {
                return new List<string> { "Binance", "Bybit", "Kucoin" };
            }
        }
        private ICommand _goMainPage;
        public ICommand GoMainPage
        {
            get { _goMainPage = _goMainPage ?? new MvxCommand(GoToMainPage); return _goMainPage; }

        }
        public void GoToMainPage()
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new FirstPage());

        }
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { _saveCommand = _saveCommand ?? new MvxCommand(Save); return _saveCommand; }

        }
        
        public void Save()
        {
            db_commands db1 = new db_commands();
            list = db.selectTable();
            string tokenmain = list[0].maintoken.ToString();
            if(api.Length > 5 && secret.Length > 5) {
                string logg = db1.UpdateExchanges(tokenmain, exchange, api, secret);
                CrossToastPopUp.Current.ShowToastMessage(logg, Plugin.Toast.Abstractions.ToastLength.Long);
                Api = "";
                Secret = "";
                Exchange = "";
            }
            else
            {

            }
            

        }
    }
}
