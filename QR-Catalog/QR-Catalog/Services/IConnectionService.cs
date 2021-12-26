using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace QR_Catalog.Services
{
    public interface IConnectionService
    {
        DbConnection Connection();
    }
}
