using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using QR_Catalog.Models;
using Newtonsoft.Json;

namespace QR_Catalog.ViewModels
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public class LoadItemVM : BaseVM
    {
        private int id;
        private string name;
        private int count;
        private string description;
        private DbItem item;

        public string Item
        {
            set
            {
                string jsonVal = Uri.UnescapeDataString(value);
                item  = Task.Run(() => JsonConvert.DeserializeObject<DbItem>(jsonVal)).Result;
                Id = item.Id;
                Name = item.Name;
                Count = item.Count;
                Description = item.Description;
            }
        }

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }
        public int Count
        {
            get => count;
            set { count = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }
    }
}
