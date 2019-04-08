using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyMaster.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync();
    }
}
