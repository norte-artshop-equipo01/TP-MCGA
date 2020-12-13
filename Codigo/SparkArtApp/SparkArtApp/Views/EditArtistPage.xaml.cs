using SparkArtApp.Models;
using SparkArtApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditArtistPage : ContentPage
    {
        private EditArtistViewModel _viewModel;

        public EditArtistPage(Artist artist)
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditArtistViewModel(Navigation, artist);
        }
    }
}