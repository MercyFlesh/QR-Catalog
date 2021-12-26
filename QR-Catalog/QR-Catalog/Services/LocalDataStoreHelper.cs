using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Essentials;
using SQLite;
using System.Threading.Tasks;
using QR_Catalog.Models;

namespace QR_Catalog.Services
{
    public class LocalDataStoreHelper
    {
        private static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<RemoteDB>();
            }
        }

        public static async Task<int> AddItemAsync(RemoteDB item)
        {
            await Init();
            return await db.InsertAsync(item);
        }

        public static async Task<int> DeleteItemAsync(string tag)
        {
            await Init();
            return await db.DeleteAsync<RemoteDB>(tag);
        }

        public static async Task<RemoteDB> GetItemAsync(string tag)
        {
            await Init();
            return await db.GetAsync<RemoteDB>(tag);
        }

        public static async Task<IEnumerable<RemoteDB>> GetItemsAsync()
        {
            await Init();
            return await db.Table<RemoteDB>().ToListAsync();
        }

        public static async Task<int> UpdateItemAsync(RemoteDB item)
        {
            await Init();
            return await db.UpdateAsync(item);
        }
    }
}
