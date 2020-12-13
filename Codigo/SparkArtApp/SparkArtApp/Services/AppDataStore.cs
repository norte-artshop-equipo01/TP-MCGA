using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SparkArtApp.Models;
using Newtonsoft.Json;

namespace SparkArtApp.Services
{
    public class AppDataStore //: IDataStore<Item>
    {
        private readonly List<Item> _items;
        

        public AppDataStore()
        {
            _items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid(), Text = "LPPA", Description="Lenguajes de Programación para la Administración" },
                new Item { Id = Guid.NewGuid(), Text = "MCGA", Description="Modelos Computacionales de Gestión Administrativa" },

            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            _items.Add(item);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                //Uri uri = new Uri(string.Format("http://mcga-host-grupo01.somee.com/api/Artist/Listar", string.Empty));
                //var client = new HttpClient();
                //HttpResponseMessage response = await client.GetAsync(uri);
                //if (response.IsSuccessStatusCode)
                //{
                //    string content = await response.Content.ReadAsStringAsync();
                //    var Items = JsonConvert.DeserializeObject<List<Artist>>(content);
                //}
            }
            catch (Exception ex)
            {
                throw;
            }


            return await Task.FromResult(_items);
        }

        public Task<bool> UpdateItemAsync(Item item)
        {
            throw new NotImplementedException();
        }
    }

    public partial class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int QuantitySold { get; set; }
        public bool Disabled { get; set; }
        public double AvgStars { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
