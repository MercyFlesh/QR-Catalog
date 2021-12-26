using QR_Catalog.Models;
using QR_Catalog.Views;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
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

        private async Task LoadPositions()
        {
            int count = -1;
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
                        command.CommandText = $"SELECT COUNT(*) FROM {TableName};";
                        count = Convert.ToInt32(command.ExecuteScalar());

                        //command.CommandText = $"SELECT * FROM {TableName};";
                        /*using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader["id"];
                            }
                        }*/
                    }
                }
            }
            catch(Exception)
            {
                SendMeassage($"Reading error");
                return;
            }

            await GenerateQrById(count);
        }

        private async Task GenerateQrById(int countItems)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            for (int id = 1; id <= countItems; id++)
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{item.Tag}/{TableName}/{id}", QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeImageData = qrCode.GetGraphic(20);

                var saveService = DependencyService.Get<ISaveService>();
                if (saveService != null)
                    await saveService.SaveByteArrayAsPNG(qrCodeImageData, item.Name.Replace(' ', '_'), id.ToString());
            }

            SendMeassage("All qr-codes saved");
        }
    }
}
