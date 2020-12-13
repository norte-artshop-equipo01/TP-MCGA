using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SparkArtApp.Models;
using SparkArtApp.Services;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        #region Fields

        private string _text;
        private string _description;


        #endregion

        #region Properties

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                NotifyPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public INavigation Navigation { get; set; }
        #endregion

        public NewItemViewModel(INavigation navigation)
        {
            Title = "Nuevo Item";
            this.Navigation = navigation;
            this.CancelCommand = new Command(OnCancel);

            this.SaveCommand = new Command(OnSave, ValidateSave);
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCommand.ChangeCanExecute();
        }
        private bool ValidateSave(object arg)
        {
            return !string.IsNullOrWhiteSpace(_text)
                   && !string.IsNullOrWhiteSpace(_description);
        }

        private async void OnSave(object obj)
        {
            var item = new Item()
            {
                Text = this.Text,
                Description = this.Description

            };
            await DataStore.AddItemAsync(item);
            await this.Navigation.PopModalAsync(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        private async void OnCancel(object obj)
        {
            var result = await Application.Current.MainPage.DisplayAlert("", "¿Está seguro que desea finalizar el alta?", "Si", "No");

            if (result)
            {
                await this.Navigation.PopModalAsync(true);
            }
        }
    }
}
