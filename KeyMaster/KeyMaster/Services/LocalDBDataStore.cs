using KeyMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyMaster.Services
{
    public class LocalDBDataStore : IDataStore<Item>
    {
        List<Item> items;

        public LocalDBDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { ID = 1, Name = "First item", Account = "Account1", Password="Password1" },
                new Item { ID = 2, Name = "Second item", Account = "Account2",Password="Password2" },
                new Item { ID = 3, Name = "Third item", Password="Password3" },
                new Item { ID = 4, Name = "Fourth item", Password="Password4" },
                new Item { ID = 5, Name = "Fifth item", Password="Password5" },
                new Item { ID = 6, Name = "Sixth item", Account = "Account6", Password="Password6" },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) =>  arg.ID== item.ID).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
