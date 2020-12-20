using SparkArtApp.Models;
using SparkArtApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    class EditProductViewModel : BaseViewModel
    {
        private Product _product;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public INavigation Navigation { get; set; }

        public Product product
        {
            get => _product;
            set
            {
                _product = value;
                NotifyPropertyChanged();
            }
        }

        public EditProductViewModel(INavigation navigation, Product product)
        {
            if (product.Id != 0)
            {
                Title = "Editar: " + product.Title;
                SaveCommand = new Command(OnUpdateProductAsync);
            }
            else
            {
                Title = "Crear Nuevo Producto";
                SaveCommand = new Command(OnAddProductAsync);
            }

            product.Image = product.Image ?? ImageHelper.ImageNotAvailablex64;
            _product = product;
            Navigation = navigation;
            CancelCommand = new Command(OnCancel);
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }

        private async void OnUpdateProductAsync()
        {
            await DataStore.UpdateItemAsync(_product);
            Application.Current.MainPage = new NavigationPage(new ProductsPage());
        }

        private async void OnAddProductAsync()
        {
            await DataStore.AddItemAsync(_product);
            Application.Current.MainPage = new NavigationPage(new ProductsPage());
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
