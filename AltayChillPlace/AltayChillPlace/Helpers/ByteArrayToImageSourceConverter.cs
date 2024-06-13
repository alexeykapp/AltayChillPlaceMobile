using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        // Использование ConcurrentDictionary для кэширования изображений
        private static readonly ConcurrentDictionary<int, ImageSource> _imageCache = new ConcurrentDictionary<int, ImageSource>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageBytes)
            {
                if (imageBytes.Length == 0)
                {
#if DEBUG
                    Console.WriteLine("ByteArrayToImageSourceConverter: Image data is empty.");
#endif
                    return null;
                }

                var hash = GetByteArrayHashCode(imageBytes);

                // Попытка получить изображение из кэша
                if (_imageCache.TryGetValue(hash, out var cachedImageSource))
                {
                    return cachedImageSource;
                }

                try
                {
                    var imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    _imageCache[hash] = imageSource; // Сохранение в кэш
                    return imageSource;
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine($"ByteArrayToImageSourceConverter: Failed to convert image data. Exception: {ex}");
#endif
                    return null;
                }
            }
            else
            {
#if DEBUG
                Console.WriteLine($"ByteArrayToImageSourceConverter: Value is not a byte array. Type: {value?.GetType().Name}");
#endif
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // Оптимизированная функция для получения хэш-кода массива байтов
        private static int GetByteArrayHashCode(byte[] obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // Быстрый хэш-код для массивов байтов
            int hash = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                hash = hash * 31 + obj[i];
            }
            return hash;
        }
    }
}