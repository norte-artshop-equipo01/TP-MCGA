using SparkArtApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public Command AddItemCommand { get; }
        public INavigation Navigation { get; set; }

        public ListProductsPage(INavigation navigation)
        {
            Title = "Lista de Productos";
            Items = new ObservableCollection<Product>();
            Navigation = navigation;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(OnAddItem);
        }

        private async void OnAddItem(object obj)
        {
            //await Navigation.PushModalAsync(new NewItemPage());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
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