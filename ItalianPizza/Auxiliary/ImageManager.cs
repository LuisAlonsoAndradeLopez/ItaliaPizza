using System.IO;
using System.Windows.Media.Imaging;

namespace ItalianPizza.Auxiliary
{
    public class ImageManager
    {
        public byte[] GetBitmapImageBytes(BitmapImage imageSource)
        {
            BitmapSource bitmapSource = imageSource as BitmapSource;

            // Convert BitmapSource to Bitmap
            BitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(memoryStream);

            // Get the bytes of the Bitmap
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
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
