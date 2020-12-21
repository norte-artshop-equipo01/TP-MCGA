using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SparkArtApp.Models;
using SparkArtApp.Services;
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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async void OnCancel(INavigation navigation, object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("", "¿Está seguro que desea salir sin guardar los cambios?", "Si", "No");

            if (result)
            {
                await navigation.PopModalAsync(true);
            }
        }


        #endregion


        #region Fields

        private string _title;

        #endregion


        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
