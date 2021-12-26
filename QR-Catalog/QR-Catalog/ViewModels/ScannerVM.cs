using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Common;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ZXing.Mobile;
using QR_Catalog.Services;
using QR_Catalog.Models;
using QR_Catalog.Views;
using Newtonsoft.Json;

namespace QR_Catalog.ViewModels
{
    public class ScannerVM : BaseVM
    {
        public RemoteDB Database { get; set; }
        public string TableName { get; set; }
        public string IdItem { get; set; }
        public Command ScannerCommand { get; }

        
        public ScannerVM()
        {
            Title = "Scanner";
            ScannerCommand = new Command(async () => await Scanning());
        }



        private async Task Scanning()
        {
            try
            {
                var scanner = new MobileBarcodeScanner();
                scanner.AutoFocus();

                var result = await scanner.Scan();
                await ResultHandler(result);
            }
            catch (Exception ex)
            {
                SendMeassage(ex.Message);
            }

        }

        private async Task ResultHandler(ZXing.Result result)
        {
            if (result != null)
            {
                string[] dbInfo = result.Text.Split('/');
                if (dbInfo.Length != 3)
                {
                    throw new ArgumentException("Incorrect qr");
                }
                
                try
                {
                    await ConnectToDb(dbInfo);
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        private async Task ConnectToDb(string[] dbInfo)
        {
            string tag = dbInfo[0];
            TableName = dbInfo[1];
            IdItem = dbInfo[2];

            try
            {
                Database = await LocalDataStoreHelper.GetItemAsync(tag);
            }
            catch(Exception)
            {
                throw new ArgumentException($"Not found tag: {tag}");
            }

            IConnectionService test;
            switch (Database.Scheme)
            {
                case "postgres":
                    test = new PostgreConnectionService() { db = Database };
                    break;
                case "postgresql":
                    test = new PostgreConnectionService() { db = Database };
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

                        command.CommandText = $"SELECT * FROM {TableName} WHERE id={IdItem};";
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                DbItem item = new DbItem();
                                item.Id = Convert.ToInt32(reader["id"]);
                                item.Name = reader["name"].ToString();
                                item.Count = Convert.ToInt32(reader["count"]);
                                item.Description = reader["description"].ToString();

                                string json = await Task.Run(() => JsonConvert.SerializeObject(item));
                                //await INavigation.PushAsync(new LoadItemPage(item));
                                await Shell.Current.GoToAsync($"{nameof(LoadItemPage)}?Item={json}");
                            }
                            else
                            {
                                throw new ArgumentNullException($"Not found item with id={IdItem}");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException($"Connection error");
            }

            
            
        }
    }
}