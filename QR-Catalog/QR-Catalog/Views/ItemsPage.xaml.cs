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
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            
            _viewModel = (ItemsViewModel)BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        /*private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dbInfo = ((ListView)sender).SelectedItem as Item;
            var messageService = DependencyService.Get<IMessageService>();
            if (dbInfo != null && messageService != null)
                messageService.ShortAlert("db selected");
                //await Application.Current.MainPage.DisplayToastAsync("db selected", 1500);
        }*/

        private void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        /*private void Delete_Clicked(object sender, EventArgs e)
        {
            _viewModel.Items.Remove(((MenuItem)sender).
        }*/
    }
}