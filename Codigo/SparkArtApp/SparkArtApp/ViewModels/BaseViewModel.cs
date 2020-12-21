using SparkArtApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region Fields

        private string _title;

        #endregion

        protected async void OnCancel(INavigation navigation, object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("", "¿Está seguro que desea salir sin guardar los cambios?", "Si", "No");

            if (result)
            {
                await navigation.PopModalAsync(true);
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }
    }
}
