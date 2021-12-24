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
        public Command SelectCommand { get; }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ItemsViewModel()
        {
            Title = "Databases";

            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new AsyncCommand<Item>(OnItemDeleted);
            SelectCommand = new Command(async (object item) => await OnItemSeleted(item));
            AddItemCommand = new Command(async () => await OnAddItem());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ItemDataStoreHelper.GetItemsAsync();
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

        private async Task OnItemDeleted(Item item)
        {
            if (item != null && (await ItemDataStoreHelper.DeleteItemAsync(item.Id)) != 0)
            {
                await ExecuteLoadItemsCommand();

                var messageService = DependencyService.Get<IMessageService>();
                if (messageService != null)
                    messageService.ShortAlert("delete success");
            }
        }

        public async Task OnItemSeleted(object obj)
        {
            Item item = obj as Item;
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async Task OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            await ExecuteLoadItemsCommand();
        }
    }
}