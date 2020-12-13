using System;
using SparkArtApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SparkArtApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<SparkArtApi>();
           
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
