using SparkArtApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        private NewItemViewModel _viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel(this.Navigation);
        }
    }
}