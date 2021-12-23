using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Catalog.Services
{
    public interface IMessageService
    {
        void ShortAlert(string msg);
        void LongAlert(string msg);
    }
}
