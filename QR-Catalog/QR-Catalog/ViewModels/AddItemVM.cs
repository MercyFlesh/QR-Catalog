using QR_Catalog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using QR_Catalog.Services;

namespace QR_Catalog.ViewModels
{
    public class AddItemVM : BaseVM
    {
        private string tag;
        private string name;
        private string scheme;
        private string host;
        private string port;
        private string database;
        private string user;
        private string password;
        
        private string uriStr;

        
        public string URIStr
        {
            get => uriStr;
            set { uriStr = value; OnPropertyChanged(); }
        }
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        

        public AddItemVM()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
        }

        private bool ValidateSave()
        {
            try
            {
                Uri uri = new Uri(uriStr);
                scheme = uri.Scheme;
                host = uri.Host;
                port = uri.Port.ToString();
                database = uri.LocalPath;
                user = uri.UserInfo.Split(':')[0];
                password = uri.UserInfo.Split(':')[1];
            }
            catch (Exception)
            {
                var messageService = DependencyService.Get<IMessageService>();
                if (messageService != null)
                    messageService.ShortAlert("Incorrect URI link");
            }

            return !string.IsNullOrWhiteSpace(tag)
                && !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(host)
                && !string.IsNullOrWhiteSpace(port)
                && !string.IsNullOrWhiteSpace(database)
                && !string.IsNullOrWhiteSpace(user)
                && !string.IsNullOrWhiteSpace(password);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        
        private async void OnSave()
        {
            if (ValidateSave())
            {
                RemoteDB newDbItem = new RemoteDB()
                {
                    Tag = tag,
                    Name = name,
                    Scheme = scheme,
                    Host = host,
                    Port = port,
                    Database = database,
                    User = user,
                    Password = password
                };
                
                await ItemDataStoreHelper.AddItemAsync(newDbItem);
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                var messageService = DependencyService.Get<IMessageService>();
                if (messageService != null)
                    messageService.ShortAlert("Incorrect URI link");
            }
        }
    }
}
