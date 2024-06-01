using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AltayChillPlace.Helpers
{
    public static class ImageConverter
    {
        public static ImageSource ConvertBase64StringToImage(PhotoResponse photoResponse)
        {
            if (photoResponse.Data.Length == 0)
            {
                return null;
            }
            try
            {
                var image = ImageSource.FromStream(() => new MemoryStream(photoResponse.Data));
                return image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public static async Task<ObservableCollection<ImageSource>> ConvertBase64StringToImages(List<PhotoResponse> photoResponses)
        {
            if (photoResponses.Count == 0)
            {
                return null;
            }
            var imageSources = new ObservableCollection<ImageSource>();

            foreach(var photo in photoResponses)
            {
                try
                {
                    imageSources.Add(ImageSource.FromStream(() => new MemoryStream(photo.Data)));
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            return imageSources;
        }
        public static ImageSource ConvertBase64StringToOneImages(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return null;
            }
            var base64Images = base64String.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Images[0]);

                ImageSource image = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                return image;
            }
            catch (FormatException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
