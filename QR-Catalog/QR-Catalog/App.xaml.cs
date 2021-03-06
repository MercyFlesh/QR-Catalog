using QR_Catalog.Services;
using QR_Catalog.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Catalog
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            LocalDataStoreHelper.Init();

            MainPage = new AppShell();
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
