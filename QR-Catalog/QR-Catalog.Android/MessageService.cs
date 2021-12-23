using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QR_Catalog.Services;
using QR_Catalog.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace QR_Catalog.Droid
{
    public class MessageAndroid : IMessageService
    {
        public void LongAlert(string msg)
        {
            Toast.MakeText(Application.Context, msg, ToastLength.Long).Show();
        }

        public void ShortAlert(string msg)
        {
            Toast.MakeText(Application.Context, msg, ToastLength.Short).Show();
        }
    }
}