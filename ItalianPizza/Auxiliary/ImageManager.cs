using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ItalianPizza.Auxiliary
{
    public class ImageManager
    {
        public byte[] GetBitmapImageBytes(BitmapImage imageSource)
        {        
            if (imageSource != null)
            {
                BitmapSource bitmapSource = imageSource as BitmapSource;

                // Convert BitmapSource to Bitmap
                BitmapEncoder encoder = new PngBitmapEncoder();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(memoryStream);

                    // Convert the bitmap to a byte array
                    return memoryStream.ToArray();
                }
            }

            return null;
        }

        public BitmapImage GetImageByItaliaPizzaStoragedImagePath(string filePath)
        {
            string incompletePath = Path.GetFullPath(filePath);
            string pathPartToDelete = "bin\\Debug\\";
            string completePath = incompletePath.Replace(pathPartToDelete, "");

            byte[] imageData = File.ReadAllBytes(completePath);

            BitmapImage imageSource = new BitmapImage();

            if (imageData != null)
            {
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(imageData);
                imageSource.EndInit();
            }

            return imageSource;
        }
    }
}
