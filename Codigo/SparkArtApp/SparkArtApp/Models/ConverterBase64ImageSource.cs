using System;
using System.Globalization;
using Xamarin.Forms;

namespace SparkArtApp.Models
{
    public class ConverterBase64ImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base64Image = (string)value;

            if (base64Image == null)
                return null;

            // Convert base64Image from string to byte-array
            byte[] imageBytes;
            try
            {
                imageBytes = System.Convert.FromBase64String(base64Image);
            }
            catch (Exception)
            {
                imageBytes = System.Convert.FromBase64String(ImageHelper.ImageNotAvailablex64);
            }

            // Return a new ImageSource
            return ImageSource.FromStream(() => { return new System.IO.MemoryStream(imageBytes); });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var streamImageSource = (StreamImageSource)value;
            var stream = streamImageSource.Stream(System.Threading.CancellationToken.None).Result;
            var image = ImageSource.FromStream(() => stream);
            var iamgeBase64 = ImageHelper.ImageSourceToByteArray(image);
            return System.Convert.ToBase64String(iamgeBase64);
        }
    }
}
