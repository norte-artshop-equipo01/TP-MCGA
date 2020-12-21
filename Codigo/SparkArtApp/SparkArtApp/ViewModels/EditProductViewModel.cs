using Plugin.Toasts;
using SparkArtApp.Models;
using SparkArtApp.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    class EditProductViewModel : BaseViewModel
    {
        private readonly bool isNewProduct;
        private readonly IToastNotificator notificator;
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
            isNewProduct = product.Id == 0;
            if (!isNewProduct)
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
            notificator = DependencyService.Get<IToastNotificator>();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }

        private async void OnUpdateProductAsync()
        {
            ExecuteAction(DataStore.UpdateItemAsync);
        }

        private async void OnAddProductAsync()
        {
            ExecuteAction(DataStore.AddItemAsync);
        }

        private async void ExecuteAction(System.Func<Product, Task> method)
        {
            var errors = await ValidateProduct();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", errors, "OK");
                return;
            }

            var options = new NotificationOptions()
            {
                Title = isNewProduct ? "Crear Nuevo Producto" : "Editar Producto",
                Description = isNewProduct
                            ? "Creando" + $" el producto { _product.Title }"
                            : "Editando" + $" el producto { _product.Title }"
            };

            _ = await notificator.Notify(options);
            await method(_product);


            var action = isNewProduct ? "creado" : "editado";
            var result = await Application.Current.MainPage.DisplayAlert("Aviso", $"El producto { _product.Title } ha sido { action } con éxito!\nDesea volver al menú anterior?", "Sí", "No");
            notificator.CancelAllDelivered();

            if (result) Application.Current.MainPage = new NavigationPage(new ProductsPage());
        }

        private async Task<string> ValidateProduct()
        {
            bool hasError = false;
            var message = "Se encontraron los siguientes problemas al validar el producto:\n";
            
            if (string.IsNullOrWhiteSpace(_product.Title))
            {
                message += "\nEl campo Título no puede ser vacío";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_product.Description))
            {
                message += "\nEl campo Descripción no puede ser vacío";
                hasError = true;
            }

            if (_product.Price < 1)
            {
                message += "\nEl campo Precio no puede ser menor a 1";
                hasError = true;
            }

            var artist = await DataStore.GetItemAsync<Artist>(_product.ArtistId);
            if (artist == null)
            {
                message += $"\nEl campo ID de Artista '{ _product.ArtistId }' no fue encontrado en la base de datos";
                hasError = true;
            }

            if (hasError)
            {
                return message;
            }

            return string.Empty;
        }

        private async void OnCancel(object obj) => OnCancel(Navigation, obj);
    }
}
