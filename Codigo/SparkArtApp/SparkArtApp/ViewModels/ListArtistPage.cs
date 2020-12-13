using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SparkArtApp.Models;
using SparkArtApp.Services;
using SparkArtApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.ViewModels
{
    public class ListArtistPage : BaseViewModel
    {
        private ObservableCollection<Artist> _items;
        public ObservableCollection<Artist> Items
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


        public ListArtistPage(INavigation navigation)
        {
            this.Title = "Lista de Items";
            this.Items= new ObservableCollection<Artist>();
            this.Navigation = navigation;
            this.LoadItemsCommand = new Command(async ()=> await ExecuteLoadItemsCommand());
            this.AddItemCommand = new Command(OnAddItem);

        }

        private async void OnAddItem(object obj)
        {
            await this.Navigation.PushModalAsync(new NewItemPage());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
                var artists = await DataStore.GetItemsAsync<Artist>();
                foreach (var artist in artists)
                {
                    Items.Add(artist);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
        }
    }
}
