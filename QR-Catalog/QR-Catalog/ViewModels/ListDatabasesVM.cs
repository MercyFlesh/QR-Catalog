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
    public class ListDatabasesVM : BaseVM
    {
        private RemoteDB _selectedItem;

        public ObservableCollection<RemoteDB> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public AsyncCommand<RemoteDB> DeleteCommand { get; }
        public AsyncCommand<RemoteDB> GenerateCommand { get; }
        public Command SelectCommand { get; }

        public RemoteDB SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ListDatabasesVM()
        {
            Title = "Databases";

            Items = new ObservableCollection<RemoteDB>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new AsyncCommand<RemoteDB>(OnItemDeleted);
            GenerateCommand = new AsyncCommand<RemoteDB>(OnGenerateQR);
            SelectCommand = new Command(async (object item) => await OnItemSeleted(item));
            AddItemCommand = new Command(async () => await OnAddItem());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await LocalDataStoreHelper.GetItemsAsync();
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


        private async Task OnGenerateQR(RemoteDB item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(GenerateQrCodesPage)}?{nameof(GenerateQrVM.DbTag)}={item.Tag}");
        }

        private async Task OnItemDeleted(RemoteDB item)
        {
            if (item != null && (await LocalDataStoreHelper.DeleteItemAsync(item.Tag)) != 0)
            {
                await ExecuteLoadItemsCommand();

                var messageService = DependencyService.Get<IMessageService>();
                if (messageService != null)
                    messageService.ShortAlert("delete success");
            }
        }

        public async Task OnItemSeleted(object obj)
        {
            RemoteDB item = obj as RemoteDB;
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DbDetailPage)}?{nameof(DetailDbVM.DbTag)}={item.Tag}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async Task OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(NewDbPage));
            await ExecuteLoadItemsCommand();
        }
    }
}