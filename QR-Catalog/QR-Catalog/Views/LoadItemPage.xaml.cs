using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QR_Catalog.Models;
using QR_Catalog.ViewModels;

namespace QR_Catalog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadItemPage : ContentPage
    {
        public LoadItemPage()
        {
            InitializeComponent();
            BindingContext = new LoadItemVM();
        }
    }
}