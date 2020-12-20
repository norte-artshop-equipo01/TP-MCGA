using SparkArtApp.Models;
using SparkArtApp.Services;
using SparkArtApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        private readonly ListProductsPage _viewModel;

        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ListProductsPage(Navigation);
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

        public void AddProduct(object sender, EventArgs e)
        {
            var newProduct = new Product
            {
                CreatedBy = "SparkArtApp",
                CreatedOn = DateTime.Now,
                ChangedBy = "SparkArtApp",
                ChangedOn = DateTime.Now
            };

            Navigation.PushModalAsync(new EditProductPage(newProduct));
        }

        private void EditProduct(object sender, EventArgs e)
        {
            var product = ((MenuItem)sender).CommandParameter as Product;
            Navigation.PushModalAsync(new EditProductPage(product));
        }

        private void DeleteProduct(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Advertencia", "¿Está seguro que desea eliminar el producto seleccionado?", "Si", "No");

                if (result)
                {
                    var product = ((MenuItem)sender).CommandParameter as Product;
                    await DataStore.DeleteItemAsync<Product>(product.Id);
                    _viewModel.Items.Remove(_viewModel.Items.FirstOrDefault(x => x.Id == product.Id));
                }
            });
        }
    }
}