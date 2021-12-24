using QR_Catalog.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemUpdateVM : BaseViewModel
    {
        private Item item;
        private string itemId;
        private string text;
        private string description;

        public Command SaveItemCommand { get; }

        public string Id { get; set; }

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

        public string ItemId
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

        public ItemUpdateVM()
        {
            SaveItemCommand = new Command(async() => await SaveItem());
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task SaveItem()
        {
            //item.Text = Text;
            //item.Description = description;
            
            await DataStore.UpdateItemAsync(new Item(){ Id = itemId, Text = text, Description = description });
            await Shell.Current.GoToAsync("..");
        }
    }
}
