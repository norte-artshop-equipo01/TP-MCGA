using SparkArtApp.Views;
using System;
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
            Application.Current.MainPage = new NavigationPage(new ItemsPage());
        }

        private void ShowProducts_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new ItemsPage());
        }
    }
}
