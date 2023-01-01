using fourbot.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Toast;
using Rg.Plugins.Popup.Services;
using MvvmCross.Commands;
using System.Windows.Input;
using fourbot.DataBase.SQL;
using Xamarin.Forms;
using fourbot.Views;
using SQLite;
using System.Collections.ObjectModel;
using fourbot.DataBase.SQLite;
using System.Data;

namespace fourbot.ViewModels.Models
{
    

    class LoginModel : MainViewModel
    {
     
        public  LoginModel()
        {
           
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }
        private string pass;
        public string Pass
        {
            get { return pass; }
            set
            {
                pass = value;
                OnPropertyChanged();
            }
        }
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { _loginCommand = _loginCommand ?? new MvxCommand(LoginCom); return _loginCommand; }

        }
        public void LoginCom()
        {
            
            db_commands db = new db_commands();
            string logg = db.LoginCommand(login, pass);
            
            if(logg == "Успешно")
            {
                CrossToastPopUp.Current.ShowToastMessage(logg, Plugin.Toast.Abstractions.ToastLength.Long);
                CheckLog();
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage(logg, Plugin.Toast.Abstractions.ToastLength.Long);

            }
        }
        private ICommand _regCommand;
        public ICommand RegCommand
        {
            get { _regCommand = _regCommand ?? new MvxCommand(RegCom); return _regCommand; }

        }
      


        public  void CheckLog()
        {
          Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new FirstPage());
        }
        public void RegCom()
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }
    }
}
