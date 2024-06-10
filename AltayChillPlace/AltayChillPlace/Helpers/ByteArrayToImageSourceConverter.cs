using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageBytes)
            {
                if (imageBytes.Length == 0)
                {
                    Console.WriteLine("ByteArrayToImageSourceConverter: Image data is empty.");
                    return null;
                }

                try
                {
                    // Логирование первых байтов данных изображения
                    Console.WriteLine($"First 10 bytes in converter: {BitConverter.ToString(imageBytes.Take(10).ToArray())}");

                    var imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    Console.WriteLine("ByteArrayToImageSourceConverter: Image data successfully converted.");
                    return imageSource;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ByteArrayToImageSourceConverter: Failed to convert image data. Exception: {ex}");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"ByteArrayToImageSourceConverter: Value is not a byte array. Type: {value?.GetType().Name}");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
