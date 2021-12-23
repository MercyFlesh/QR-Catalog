using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QR_Catalog.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        
        public ScannerViewModel()
        {
            Title = "Scanner";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }
    }
}