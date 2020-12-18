using SparkArtApp.Models;
using SparkArtApp.Services;
using SparkArtApp.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        private readonly ListArtistPage _viewModel;
        
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ListArtistPage(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadItemsCommand.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("", "¿Está seguro que desea volver al menú principal?", "Si", "No");

                if (result)
                {
                    Application.Current.MainPage = new MainPage();
                }
            });
            return true;
        }

        private void AddArtist(object sender, EventArgs e)
        {
            var newArtist = new Artist
            {
                CreatedBy = "SparkArtApp",
                CreatedOn = DateTime.Now,
                ChangedBy = "SparkArtApp",
                ChangedOn = DateTime.Now
            };
            
            Navigation.PushModalAsync(new EditArtistPage(newArtist));
        }

        private void EditArtist(object sender, EventArgs e)
        {
            var artist = ((MenuItem)sender).CommandParameter as Artist;
            Navigation.PushModalAsync(new EditArtistPage(artist));
        }

        private void DeleteArtist(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Advertencia", "¿Está seguro que desea eliminar el artista seleccionado?", "Si", "No");

                if (result)
                {
                    var artist = ((MenuItem)sender).CommandParameter as Artist;
                    await DataStore.DeleteItemAsync<Artist>(artist.Id);
                    _viewModel.Items.Remove(_viewModel.Items.FirstOrDefault(x => x.Id == artist.Id));
                }
            });
        }
    }
}