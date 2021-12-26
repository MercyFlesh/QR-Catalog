using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QR_Catalog.Services
{
    public interface ISaveService
    {
        Task SaveByteArrayAsPNG(byte[] imageData, string folderName = "images", string fileName = "image");
    }
}
