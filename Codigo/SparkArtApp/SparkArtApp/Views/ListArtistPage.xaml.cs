using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparkArtApp.Models;
using SparkArtApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        private readonly ListArtistPage _viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ListArtistPage(this.Navigation);
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

                var result = await DisplayAlert("", "¿Está seguro que desea finalizar la operación?", "Si", "No");

                if (result)
                {
                    Application.Current.MainPage = new MainPage();
                }
            });
            return true;
        }

        private void EditArtist(object sender, EventArgs e)
        {
            var artist = ((MenuItem)sender).CommandParameter as Artist;
            Navigation.PushModalAsync(new EditArtistPage(artist));
        }
    }
}