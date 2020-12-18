using SparkArtApp.Models;
using SparkArtApp.Views;
using System.ComponentModel;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    class EditArtistViewModel : BaseViewModel
    {
        private Artist _artist;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
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
            if (artist.Id != 0)
            {
                Title = "Editar: " + artist.FullName;
                SaveCommand = new Command(OnUpdateArtistAsync);
            }
            else
            {
                Title = "Crear Nuevo Artista";
                SaveCommand = new Command(OnAddArtistAsync);
            }
            
            _artist = artist;
            Navigation = navigation;
            CancelCommand = new Command(OnCancel);
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }

        private async void OnUpdateArtistAsync()
        {
            await DataStore.UpdateItemAsync(_artist);
            Application.Current.MainPage = new NavigationPage(new ItemsPage());
        }

        private async void OnAddArtistAsync()
        {
            await DataStore.AddItemAsync(_artist);
            Application.Current.MainPage = new NavigationPage(new ItemsPage());
        }

        private async void OnCancel(object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("", "¿Está seguro que desea salir sin guardar los cambios?", "Si", "No");

            if (result)
            {
                await Navigation.PopModalAsync(true);
            }
        }
    }
}
