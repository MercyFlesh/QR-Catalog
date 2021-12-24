using QR_Catalog.Models;
using QR_Catalog.ViewModels;
using QR_Catalog.Services;
using QR_Catalog.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms.Xaml;

namespace QR_Catalog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ListItemsVM _viewModel;

        public ItemsPage()
        {
            InitializeComponent(); 
            _viewModel = (ListItemsVM)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}