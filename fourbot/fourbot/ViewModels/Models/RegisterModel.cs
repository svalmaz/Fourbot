using fourbot.DataBase.SQL;
using fourbot.ViewModels.ViewModel;
using MvvmCross.Commands;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace fourbot.ViewModels.Models
{
    class RegisterModel : MainViewModel
    {
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
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        private ICommand _registerCommand;
        public ICommand RegisterCommand
        {
            get { _registerCommand = _registerCommand ?? new MvxCommand(RegisterCom); return _registerCommand; }

        }
        public void RegisterCom()
        {
            db_commands db = new db_commands();
            if (pass.Length > 7 && login.Length >4 )
            {
                string logg = db.RegisterCommand(login, pass, email);
                CrossToastPopUp.Current.ShowToastMessage(logg, Plugin.Toast.Abstractions.ToastLength.Long);
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());

            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage("Логин или пароль слишком короткие", Plugin.Toast.Abstractions.ToastLength.Long);


            }
        }
    }
}
