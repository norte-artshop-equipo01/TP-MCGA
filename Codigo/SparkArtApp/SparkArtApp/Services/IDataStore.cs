using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkArtApp.Services
{
    public interface IDataStore
    {
        Task AddItemAsync<T>(T item);
        Task UpdateItemAsync<T>(T item);
        Task DeleteItemAsync<T>(int id);
        Task<T> GetItemAsync<T>(int id);
        Task<List<T>> GetItemsAsync<T>();
    }
}
