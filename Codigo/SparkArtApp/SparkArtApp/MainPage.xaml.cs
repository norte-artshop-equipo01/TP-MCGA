using SparkArtApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SparkArtApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ShowArtist_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ArtistPage());
        }

        private void ShowProducts_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ProductsPage());
        }

        private async void Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://norte-mcga-ecomm-grupo01.azurewebsites.net"));
        }
    }
}
