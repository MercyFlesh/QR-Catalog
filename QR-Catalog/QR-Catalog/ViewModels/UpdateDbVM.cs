using QR_Catalog.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using QR_Catalog.Services;


namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(DbTag), nameof(DbTag))]
    public class UpdateDbVM : BaseVM
    {
        private RemoteDB item;
        private string tagKey;
        private string scheme;
        private string name;
        private string host;
        private string port;
        private string database;
        private string user;
        private string password;

        public Command SaveItemCommand { get; }

        public string Scheme
        {
            get => scheme;
            set { scheme = value; OnPropertyChanged(); }
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
                return tagKey;
            }
            set
            {
                tagKey = value;
                LoadDbTag(value);
            }
        }

        public UpdateDbVM()
        {
            SaveItemCommand = new Command(async() => await SaveItem());
        }

        public async void LoadDbTag(string tag)
        {
            try
            {
                item = await LocalDataStoreHelper.GetItemAsync(tag);
                Scheme = item.Scheme;
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

        private async Task SaveItem()
        {
            RemoteDB Item = new RemoteDB()
            {
                Tag = tagKey,
                Name = name,
                Scheme = scheme,
                Host = host,
                Port = port,
                Database = database,
                User = user,
                Password = password
            };

            await LocalDataStoreHelper.UpdateItemAsync(Item);
            await Shell.Current.GoToAsync("..");
        }
    }
}
