using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using QR_Catalog.ViewModels;

namespace QR_Catalog.Views
{
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
            BindingContext = new ScannerVM();
        }
    }
}