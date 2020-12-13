using SparkArtApp.Models;
using SparkArtApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SparkArtApp.Views;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    class EditArtistViewModel : BaseViewModel
    {
        private Artist _artist;

        public Command SaveCommand { get; }
        public INavigation Navigation { get; set; }

        public Artist artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropertyChanged();
            }
        }

        public EditArtistViewModel(INavigation navigation, Artist artist)
        {
            Title = "Editar: " + artist.FullName;
            _artist = artist;
            Navigation = navigation;
            SaveCommand = new Command(OnSaveAsync);
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }

        private async void OnSaveAsync()
        {
            await DataStore.UpdateItemAsync(_artist);
            Application.Current.MainPage = new NavigationPage(new ItemsPage());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private async void OnCancel(object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("", "¿Está seguro que desea guardar los cambios?", "Si", "No");

            if (result)
            {
                await Navigation.PopModalAsync(true);
            }
        }
    }
}
