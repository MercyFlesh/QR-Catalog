using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Catalog.Models
{
    public class DbItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}
