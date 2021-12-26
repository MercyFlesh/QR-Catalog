using QR_Catalog.Models;
using QR_Catalog.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.Common;
using Npgsql;
using MySql.Data.MySqlClient;
using QR_Catalog.Services;

namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(DbTag), nameof(DbTag))]
    class GenerateQrVM : BaseVM
    {
        private RemoteDB item;
        private string tag;
        private string tableName;

        public Command GenerateCommand { get; }

        public string TableName
        {
            get => tableName;
            set
            {
                tableName = value;
                OnPropertyChanged();
            }
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


        public GenerateQrVM()
        {
            GenerateCommand = new Command(async () => await LoadPositions());
        }

        public async void LoadDbTag(string tag)
        {
            try
            {
                item = await ItemDataStoreHelper.GetItemAsync(tag);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private void SendMeassage(string msg)
        {
            var messageService = DependencyService.Get<IMessageService>();
            if (messageService != null)
                messageService.ShortAlert(msg);
        }

        public async Task LoadPositions()
        {
            int id = -1;
            //public.testdata

            IConnectionService test;
            switch(item.Scheme)
            {
                case "postgres":
                    test = new PostgreConnectionService() { db = item };
                    break;
                case "postgresql":
                    test = new PostgreConnectionService() { db = item };
                    break;
                default:
                    SendMeassage("Incorrect database scheme");
                    return;
            }
            
            try
            {
                using (DbConnection connection = test.Connection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM {TableName} LIMIT 100;";
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader["id"];
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                SendMeassage("Reading error");
                return;
            }

            SendMeassage(id.ToString());
        }
    }
}
