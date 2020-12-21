using Plugin.Media;
using SparkArtApp.Models;
using SparkArtApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPage : ContentPage
    {
        private EditProductViewModel _viewModel;

        public EditProductPage(Product product)
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditProductViewModel(Navigation, product);
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            { 
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null) return;
                
                var image = ImageSource.FromStream(() => file.GetStream());
                var imageBase64 = ImageHelper.ImageSourceToByteArray(image);

                _viewModel.product.Image = Convert.ToBase64String(imageBase64);
                _viewModel.product = _viewModel.product;
            }
        }
    }
}