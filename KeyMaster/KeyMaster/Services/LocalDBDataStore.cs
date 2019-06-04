using KeyMaster.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyMaster.Services
{
    public class LocalDBDataStore : IDataStore<Item>
    {
        readonly static SQLiteAsyncConnection database;

        static LocalDBDataStore()
        {
            if(database == null)
            {
                database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "KeyMasterSQLite.db3"));
                database.CreateTableAsync<Item>().Wait();
            }
        }

        private LocalDBDataStore()
        {

        }

        static LocalDBDataStore _instance;
        public static LocalDBDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalDBDataStore();
                }
                return _instance;
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item.ID != 0)
            {
                return await database.UpdateAsync(item) >= 0;
            }
            else
            {
                return await database.InsertAsync(item) >= 0;
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            return await database.UpdateAsync(item) >= 0;
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            return await database.DeleteAsync(item) >= 0;
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await database.Table<Item>().ToListAsync();
        }
    }
}
