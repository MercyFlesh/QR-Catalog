using QR_Catalog.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace QR_Catalog.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}