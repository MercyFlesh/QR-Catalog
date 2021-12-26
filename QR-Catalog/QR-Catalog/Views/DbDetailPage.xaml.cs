using QR_Catalog.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace QR_Catalog.Views
{
    public partial class DbDetailPage : ContentPage
    {
        public DbDetailPage()
        {
            InitializeComponent();
            BindingContext = new DetailDbVM();
        }
    }
}