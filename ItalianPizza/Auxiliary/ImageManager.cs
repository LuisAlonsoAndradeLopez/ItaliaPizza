using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.XAMLViews;
using System;
using System.Data.Entity.Core;
using System.Diagnostics;
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

        public byte[] GetWriteableBitmapBytes(WriteableBitmap imageSource)
        {
            byte[] bytes;

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapSource bitmapSource = imageSource;
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
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

        public bool CheckProductImagePath(int productId)
        {
            bool result = true;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = $"..\\TempCache\\Products\\{productId}.png";
            string imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            string directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(imagePath))
            {
                ProductDAO productDAO = new ProductDAO();
                try
                {
                    ProductPictureSet productPicture = productDAO.GetProductPicturebyID(productId);
                    if (productPicture != null)
                    {
                        File.WriteAllBytes(imagePath, productPicture.ProductImage);
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (EntityException ex)
                {
                    result = false;
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
                catch (InvalidOperationException ex)
                {
                    result = false;
                    throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                }
            }

            return result;
        }

        public bool CheckUserImagePath(int userID)
        {
            bool result = true;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = $"..\\TempCache\\Users\\{userID}.png";
            string imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            string directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(imagePath))
            {
                UserDAO userDAO = new UserDAO();
                try
                {
                    EmployeePictureSet employeePicture = userDAO.GetUserPicturebyID(userID);
                    if (employeePicture != null)
                    {
                        File.WriteAllBytes(imagePath, employeePicture.EmployeeImage);
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (EntityException ex)
                {
                    result = false;
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
                catch (InvalidOperationException ex)
                {
                    result = false;
                    throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                }
            }

            return result;
        }

        public bool CheckSupplyImagePath(int supplyID)
        {
            bool result = true;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = $"..\\TempCache\\Supplies\\{supplyID}.png";
            string imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            string directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(imagePath))
            {
                SupplyDAO supplyDAO = new SupplyDAO();
                try
                {
                    SupplyPictureSet supplyPicture = supplyDAO.GetSupplyPicturebyID(supplyID);
                    if (supplyPicture != null)
                    {
                        File.WriteAllBytes(imagePath, supplyPicture.SupplyImage);
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch (EntityException ex)
                {
                    result = false;
                    throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                }
                catch (InvalidOperationException ex)
                {
                    result = false;
                    throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                }
            }

            return result;
        }

        public bool OverwriteProductImagePath(int productId)
        {
            bool result = true;
            string baseDirectory = Directory.GetCurrentDirectory();
            string relativePath = $"..\\TempCache\\Products\\{productId}.png";
            string imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            ProductDAO productDAO = new ProductDAO();
            try
            {
                ProductPictureSet productPicture = productDAO.GetProductPicturebyID(productId);
                if (productPicture != null)
                {                  
                    File.Delete(imagePath);
                    File.WriteAllBytes(imagePath, productPicture.ProductImage);
                }
                else
                {
                    result = false;
                }
            }
            catch (EntityException ex)
            {
                result = false;
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                result = false;
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }            

            return result;
        }

        public bool OverwriteSupplyImagePath(int supplyID)
        {
            bool result = true;
            string baseDirectory = Directory.GetCurrentDirectory();
            string relativePath = $"..\\TempCache\\Supplies\\{supplyID}.png";
            string imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            string directoryPath = Path.GetDirectoryName(imagePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            SupplyDAO supplyDAO = new SupplyDAO();
            try
            {
                SupplyPictureSet supplyPicture = supplyDAO.GetSupplyPicturebyID(supplyID);
                if (supplyPicture != null)
                {
                    File.Delete(imagePath);
                    File.WriteAllBytes(imagePath, supplyPicture.SupplyImage);
                }
                else
                {
                    result = false;
                }
            }
            catch (EntityException ex)
            {
                result = false;
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                result = false;
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }
    }
}
