using fourbot.DataBase.SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fourbot
{
    public partial class App : Application
    {
        public static NavigationPage NavPage { get; set; }
        public App()
        {
            InitializeComponent();

            NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
