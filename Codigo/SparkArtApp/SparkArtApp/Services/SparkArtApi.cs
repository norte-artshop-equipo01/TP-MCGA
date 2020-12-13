using SparkArtApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SparkArtApp.Services
{
    public class SparkArtApi : IDataStore
    {
        private readonly HttpClient client;

        public SparkArtApi()
        {
            client = new HttpClient();
        }


        public Task<bool> AddItemAsync<T>(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetItemsAsync<T>()
        {
            var requestUrl = GetModelUrl<T>();
            var response = await client.GetAsync(requestUrl + "listar");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error while calling " + requestUrl);

            string content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public async Task UpdateItemAsync<T>(T item)
        {
            var requestUrl = GetModelUrl<T>();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(requestUrl + "Editar", content);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Error while calling " + requestUrl);
        }

        private static string GetModelUrl<T>()
        {
            object item = Activator.CreateInstance(typeof(T));

            var url = "https://norte-mcga-grupo01.azurewebsites.net/api/";
            switch (item)
            {
                case Artist _:
                    return url + "artist/";
                default:
                    throw new NotSupportedException("Object not supported: " + typeof(T).Name);
            }
        }
    }
}
