using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Text;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductDAO
    {
        public int AddProduct(ProductSaleSet product)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.ProductSaleSet.Add(product);
                    context.SaveChanges();
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public int DisableProduct(string productName)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ProductSaleSet productToDisable = context.ProductSaleSet.Where(p => p.Name == productName).FirstOrDefault();
                    if (productToDisable != null)
                    {
                        productToDisable.ProductStatusId = 2;
                        context.SaveChanges();
                    }
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public List<ProductSaleSet> GetAllActiveProducts()
        {
            List<ProductSaleSet> activeProducts = new List<ProductSaleSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                activeProducts = context.ProductSaleSet.ToList();
            }

            return activeProducts;
        }

        public BitmapImage GetImageByProductName(string productName)
        {
            using (var context = new ItalianPizzaServerBDEntities())
            {
                byte[] imageBytes = context.ProductSaleSet.Where(ps => ps.Name == productName).First().Picture;

                BitmapImage bitmapImage = new BitmapImage();

                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Freeze the image for performance benefits
                }

                return bitmapImage;
            }
        }

        public ProductSaleSet GetProductByName(string productName)
        {
            ProductSaleSet product = new ProductSaleSet();
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    product = context.ProductSaleSet.Where(p => p.Name == productName).FirstOrDefault();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return product;
        }

        public List<ProductSaleSet> GetSpecifiedProductsByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<ProductSaleSet> specifiedProducts = new List<ProductSaleSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Name")
                {
                    specifiedProducts = context.ProductSaleSet.Where(p => p.Name.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    specifiedProducts = context.ProductSaleSet.Where(p => p.IdentificationCode.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedProducts;
        }

        public int ModifyProduct(ProductSaleSet originalProduct, ProductSaleSet modifiedProduct)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ProductSaleSet productFound = context.ProductSaleSet.Where(p => p.Name == originalProduct.Name).FirstOrDefault();
                    if (productFound != null)
                    {
                        productFound.Name = modifiedProduct.Name;
                        productFound.Quantity = modifiedProduct.Quantity;
                        productFound.PricePerUnit = modifiedProduct.PricePerUnit;
                        productFound.Picture = modifiedProduct.Picture;
                        productFound.ProductTypeId = modifiedProduct.ProductTypeId;
                        productFound.EmployeeId = modifiedProduct.EmployeeId;
                        productFound.IdentificationCode = modifiedProduct.IdentificationCode;
                        productFound.Description = modifiedProduct.Description;
                        context.SaveChanges();
                        generatedID = (int)productFound.Id;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return generatedID;
        }

        public bool TheNameIsAlreadyRegistred(string productName)
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ProductSaleSet product = context.ProductSaleSet.Where(p => p.Name == productName).FirstOrDefault();
                    if (product != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }
        }
    }
}
