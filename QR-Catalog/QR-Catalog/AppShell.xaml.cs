using QR_Catalog.ViewModels;
using QR_Catalog.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QR_Catalog
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DbDetailPage), typeof(DbDetailPage));
            Routing.RegisterRoute(nameof(NewDbPage), typeof(NewDbPage));
            Routing.RegisterRoute(nameof(UpdateDbPage), typeof(UpdateDbPage));
            Routing.RegisterRoute(nameof(GenerateQrCodesPage), typeof(GenerateQrCodesPage));
            Routing.RegisterRoute(nameof(LoadItemPage), typeof(LoadItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ScannerPage");
        }
    }
}
