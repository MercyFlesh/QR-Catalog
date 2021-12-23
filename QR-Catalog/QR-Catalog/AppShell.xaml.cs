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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ScannerPage");
        }
    }
}
