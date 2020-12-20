using SparkArtApp.Services;
using SparkArtApp.Views;
using System;
using System.Collections.Generic;
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
    }
}
