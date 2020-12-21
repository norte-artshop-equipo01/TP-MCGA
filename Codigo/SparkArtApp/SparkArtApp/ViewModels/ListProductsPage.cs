using SparkArtApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    public class ListProductsPage : BaseViewModel
    {
        private ObservableCollection<Product> _items;
        public ObservableCollection<Product> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }
        public Command LoadItemsCommand { get; }
        public INavigation Navigation { get; set; }

        public ListProductsPage(INavigation navigation)
        {
            Title = "Lista de Productos";
            Items = new ObservableCollection<Product>();
            Navigation = navigation;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                var products = await DataStore.GetItemsAsync<Product>();
                foreach (var product in products)
                {
                    Items.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}