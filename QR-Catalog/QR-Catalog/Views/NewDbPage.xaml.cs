using QR_Catalog.Models;
using QR_Catalog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QR_Catalog.Views
{
    public partial class NewDbPage : ContentPage
    {
        public NewDbPage()
        {
            InitializeComponent();
            BindingContext = new AddDbVM();
        }
    }
}