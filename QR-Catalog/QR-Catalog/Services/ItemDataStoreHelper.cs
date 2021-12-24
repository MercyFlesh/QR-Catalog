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
    public class ItemDataStoreHelper
    {
        private static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Item>();
            }
        }

        public static async Task<int> AddItemAsync(Item item)
        {
            await Init();
            return await db.InsertAsync(new Item { Text = item.Text, Description = item.Description });
        }

        public static async Task<int> DeleteItemAsync(int id)
        {
            await Init();
            return await db.DeleteAsync<Item>(id);
        }

        public static async Task<Item> GetItemAsync(int id)
        {
            await Init();
            return await db.GetAsync<Item>(id);
        }

        public static async Task<IEnumerable<Item>> GetItemsAsync()
        {
            await Init();
            return await db.Table<Item>().ToListAsync();
        }

        public static async Task<int> UpdateItemAsync(Item item)
        {
            await Init();
            return await db.UpdateAsync(item);
        }
    }
}
