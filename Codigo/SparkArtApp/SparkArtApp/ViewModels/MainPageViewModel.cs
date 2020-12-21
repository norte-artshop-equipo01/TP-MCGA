using SparkArtApp.Models;
using SparkArtApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SparkArtApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();
        readonly IList<Product> source;

        public ObservableCollection<Product> Products { get; private set; }
        public IList<Product> EmptyProducts { get; private set; }

        public Product PreviousProduct { get; set; }
        public Product CurrentProduct { get; set; }
        public Product CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public Command ItemChangedCommand => new Command<Product>(ItemChanged);
        public Command PositionChangedCommand => new Command<int>(PositionChanged);

        public MainPageViewModel()
        {
            source = new List<Product>();
            var task = Task.Run(async () => { await CreateProductCollection(); });
            Task.WaitAll(task);

            CurrentItem = Products.Skip(3).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 3;
            OnPropertyChanged("Position");
        }
        

        private async Task CreateProductCollection()
        {
            var prod1 = await DataStore.GetItemAsync<Product>(66);
            if (prod1 != null)
            {
                source.Add(new DemoProduct
                {
                    Title = prod1.Title,
                    Description = prod1.Description,
                    ImageSource = ImageSource.FromStream(() => { return new System.IO.MemoryStream(System.Convert.FromBase64String(prod1.Image)); })
                });
            }

            var prod2 = await DataStore.GetItemAsync<Product>(68);
            if (prod2 != null)
            {
                source.Add(new DemoProduct
                {
                    Title = prod2.Title,
                    Description = prod2.Description,
                    ImageSource = ImageSource.FromStream(() => { return new System.IO.MemoryStream(System.Convert.FromBase64String(prod2.Image)); })
                });
            }

            var prod3 = await DataStore.GetItemAsync<Product>(69);
            if (prod3 != null)
            {
                source.Add(new DemoProduct
                {
                    Title = prod3.Title,
                    Description = prod3.Description,
                    ImageSource = ImageSource.FromStream(() => { return new System.IO.MemoryStream(System.Convert.FromBase64String(prod3.Image)); })
                });
            }

            Products = new ObservableCollection<Product>(source);
        }

        void ItemChanged(Product item)
        {
            PreviousProduct = CurrentProduct;
            CurrentProduct = item;
            OnPropertyChanged("PreviousProduct");
            OnPropertyChanged("CurrentProduct");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
