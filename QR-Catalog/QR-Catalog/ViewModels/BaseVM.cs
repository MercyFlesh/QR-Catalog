using QR_Catalog.Models;
using QR_Catalog.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QR_Catalog.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        bool isBusy = false;
        string title = string.Empty;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        protected void SendMeassage(string msg)
        {
            var messageService = DependencyService.Get<IMessageService>();
            if (messageService != null)
                messageService.ShortAlert(msg);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
