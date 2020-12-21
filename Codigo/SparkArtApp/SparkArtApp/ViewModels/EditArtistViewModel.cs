using Plugin.Toasts;
using SparkArtApp.Models;
using SparkArtApp.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    class EditArtistViewModel : BaseViewModel
    {
        private readonly bool isNewArtist;
        private readonly IToastNotificator notificator;
        private Artist _artist;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public INavigation Navigation { get; set; }

        public Artist artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropertyChanged();
            }
        }

        public EditArtistViewModel(INavigation navigation, Artist artist)
        {
            isNewArtist = artist.Id == 0;
            if (!isNewArtist)
            {
                Title = "Editar: " + artist.FullName;
                SaveCommand = new Command(OnUpdateArtistAsync);
            }
            else
            {
                Title = "Crear Nuevo Artista";
                SaveCommand = new Command(OnAddArtistAsync);
            }
            
            _artist = artist;
            Navigation = navigation;
            CancelCommand = new Command(OnCancel);
            PropertyChanged += OnPropertyChanged;
            notificator = DependencyService.Get<IToastNotificator>();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }

        private async void OnUpdateArtistAsync()
        {
            ExecuteAction(DataStore.UpdateItemAsync);
        }

        private async void OnAddArtistAsync()
        {
            ExecuteAction(DataStore.AddItemAsync);
        }

        private async void ExecuteAction(System.Func<Artist, Task> method)
        {
            var errors = ValidateArtist();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", errors, "OK");
                return;
            }

            var options = new NotificationOptions()
            {
                Title = isNewArtist ? "Crear Nuevo Artista" : "Editar Artista",
                Description = isNewArtist
                            ? "Creando" + $" el artista { _artist.FullName }"
                            : "Editando" + $" el artista { _artist.FullName }"
            };

            _ = await notificator.Notify(options);
            await method(_artist);

            var action = isNewArtist ? "creado" : "editado";
            var result = await Application.Current.MainPage.DisplayAlert("Aviso", $"El artista { _artist.FullName } ha sido { action } con éxito!\nDesea volver al menú anterior?", "Sí", "No");
            notificator.CancelAllDelivered();

            if (result) Application.Current.MainPage = new NavigationPage(new ArtistPage());
        }

        private string ValidateArtist()
        {
            bool hasError = false;
            var message = "Se encontraron los siguientes problemas al validar el artista:\n";

            if (string.IsNullOrWhiteSpace(_artist.FirstName))
            {
                message += "\nEl campo Nombre no puede ser vacío";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_artist.LastName))
            {
                message += "\nEl campo Apellido no puede ser vacío";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_artist.Country))
            {
                message += "\nEl campo País no puede ser vacío";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_artist.LifeSpan))
            {
                message += "\nEl campo Edad no puede ser vacío";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_artist.Description))
            {
                message += "\nEl campo Descripción no puede ser vacío";
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
