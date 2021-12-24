using QR_Catalog.Models;
using QR_Catalog.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using QR_Catalog.Services;

namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private Item item;
        private int itemId;
        private string text;
        private string description;

        public Command UpdateItemCommand { get; }

        public int Id { get; set; }

        public string Text
        {
            get => text;
            set { text = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public ItemDetailViewModel()
        {
            UpdateItemCommand = new Command(async (object obj) => await UpdateItem(obj));
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                item = await ItemDataStoreHelper.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task UpdateItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateVM.ItemId)}={item.Id}");
        }
    }
}
