using AltayChillPlace.ApiResponses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

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
        public static async Task<byte[]> PickPhotoAsync()
        {
            try
            {
                bool hasPermission = await PermissionsHelper.EnsureStoragePermissionsAsync();
                if (!hasPermission)
                {
                    return null;
                }

                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    using (var stream = await result.OpenReadAsync())
                    {
                        var byteArray = ConvertStreamToByteArray(stream);

                        Console.WriteLine($"Image byte array length: {byteArray.Length}");
                        Console.WriteLine($"First 10 bytes: {BitConverter.ToString(byteArray.Take(10).ToArray())}");

                        return byteArray;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null;
        }

        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        //public static byte[] ConvertStreamToByteArray(Stream stream)
        //{
        //    long originalPosition = 0;

        //    if (stream.CanSeek)
        //    {
        //        originalPosition = stream.Position;
        //        stream.Position = 0;
        //    }

        //    try
        //    {
        //        byte[] readBuffer = new byte[4096];
        //        int totalBytesRead = 0;
        //        int bytesRead;

        //        while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
        //        {
        //            totalBytesRead += bytesRead;

        //            if (totalBytesRead == readBuffer.Length)
        //            {
        //                int nextByte = stream.ReadByte();
        //                if (nextByte != -1)
        //                {
        //                    byte[] temp = new byte[readBuffer.Length * 2];
        //                    Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
        //                    Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
        //                    readBuffer = temp;
        //                    totalBytesRead++;
        //                }
        //            }
        //        }

        //        byte[] buffer = readBuffer;
        //        if (readBuffer.Length != totalBytesRead)
        //        {
        //            buffer = new byte[totalBytesRead];
        //            Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
        //        }
        //        return buffer;
        //    }
        //    finally
        //    {
        //        if (stream.CanSeek)
        //        {
        //            stream.Position = originalPosition;
        //        }
        //    }
        //}
    }
}
