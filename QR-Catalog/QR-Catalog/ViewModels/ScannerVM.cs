using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QR_Catalog.ViewModels
{
    public class ScannerVM : BaseVM
    {
        public ICommand OpenWebCommand { get; }
        
        public ScannerVM()
        {
            Title = "Scanner";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }
    }
}