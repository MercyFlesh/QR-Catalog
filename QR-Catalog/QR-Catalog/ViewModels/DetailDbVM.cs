using QR_Catalog.Models;
using QR_Catalog.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using QR_Catalog.Services;

namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(DbTag), nameof(DbTag))]
    public class DetailDbVM : BaseVM
    {
        private RemoteDB item;
        private string tag;
        private string name;
        private string host;
        private string port;
        private string database;
        private string user;
        private string password;

        public Command UpdateItemCommand { get; }

        public string Tag
        {
            get => tag;
            set { tag = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }
        public string Host
        {
            get => host;
            set { host = value; OnPropertyChanged(); }
        }
        public string Port
        {
            get => port;
            set { port = value; OnPropertyChanged(); }
        }
        public string Database
        {
            get => database;
            set { database = value; OnPropertyChanged(); }
        }
        public string User
        {
            get => user;
            set { user = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }


        public string DbTag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
                LoadDbTag(value);
            }
        }

        public DetailDbVM()
        {
            UpdateItemCommand = new Command(async (object obj) => await UpdateItem(obj));
        }

        public async void LoadDbTag(string tag)
        {
            try
            {
                item = await LocalDataStoreHelper.GetItemAsync(tag);
                Tag = item.Tag;
                Name = item.Name;
                Host = item.Host;
                Port = item.Port;
                Database = item.Database;
                User = item.User;
                Password = item.Password;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async Task UpdateItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(UpdateDbPage)}?{nameof(UpdateDbVM.DbTag)}={item.Tag}");
        }
    }
}
