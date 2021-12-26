using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QR_Catalog.Services;
using QR_Catalog.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SaveService))]
namespace QR_Catalog.Droid
{
    public class SaveService : ISaveService
    {

        public async Task SaveByteArrayAsPNG(byte[] imageData, string folderName="images", string fileName="image")
        {
            if (imageData == null)
                return;

            string root = Application.Context.GetExternalFilesDir(null).ToString();
            
            string docsPath = System.IO.Path.Combine(root, folderName);
            Directory.CreateDirectory(docsPath);

            string path = System.IO.Path.Combine(docsPath, $"{fileName}.png");
            Bitmap bmp = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length);
            
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                bmp.Compress(Bitmap.CompressFormat.Png, 40, fs);
            }
        }
    }
}