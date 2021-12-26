using SQLite;

namespace QR_Catalog.Models
{
    public class RemoteDB
    {
        [PrimaryKey, Unique]
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }

        // <summary>
        // requires readonly user
        // </summary>
        public string User { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return null;
        }
    }
}