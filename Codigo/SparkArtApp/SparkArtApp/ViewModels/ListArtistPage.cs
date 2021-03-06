﻿using SparkArtApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

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
        public INavigation Navigation { get; set; }


        public ListArtistPage(INavigation navigation)
        {
            Title = "Lista de Artistas";
            Items= new ObservableCollection<Artist>();
            Navigation = navigation;
            LoadItemsCommand = new Command(async ()=> await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
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
