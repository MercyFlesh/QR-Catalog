using QR_Catalog.Models;
using QR_Catalog.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MvvmHelpers.Commands;
using System.Windows.Input;
using QR_Catalog.Services;

using Command = MvvmHelpers.Commands.Command;

namespace QR_Catalog.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public AsyncCommand<Item> DeleteCommand { get; }

        //public ICommand DeleteCommand { get; }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                var messageService = DependencyService.Get<IMessageService>();
                if (value != null && messageService != null)
                    messageService.ShortAlert("db selected");

                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ItemsViewModel()
        {
            Title = "Databases";

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new AsyncCommand<Item>(DeleteItem);
            //ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteItem(Item item)
        {
            if (item != null && await DataStore.DeleteItemAsync(item.Id))
            {
                await ExecuteLoadItemsCommand();

                var messageService = DependencyService.Get<IMessageService>();
                if (messageService != null)
                    messageService.ShortAlert("delete success");
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            await ExecuteLoadItemsCommand();
        }

        /*async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }*/
    }
}