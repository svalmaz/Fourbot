using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using fourbot.DataBase.SQLite;
using CarouselView.FormsPlugin.Android;

namespace fourbot.Droid
{
    [Activity(Label = "fourbot", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        SQLiteDB db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
             db = new SQLiteDB();
            db.createDatabase();
            base.OnCreate(savedInstanceState);
            CarouselViewRenderer.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}