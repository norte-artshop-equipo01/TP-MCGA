using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkArtApp.Views;
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
