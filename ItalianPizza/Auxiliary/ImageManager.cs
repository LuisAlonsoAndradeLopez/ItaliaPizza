using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ItalianPizza.Auxiliary
{
    public class ImageManager
    {
        public byte[] GetBitmapImageBytes(BitmapImage imageSource)
        {
            byte[] bytes;

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSource));
                encoder.Save(stream);

                bytes = stream.ToArray();
            }

            return bytes;
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
