using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductDAO
    {
        public int AddProduct(Producto product)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.ProductoSet.Add(product);
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
                    Producto productToDisable = context.ProductoSet.Where(p => p.Nombre == productName).FirstOrDefault();
                    if (productToDisable != null)
                    {
                        productToDisable.Estado = ArticleStatus.Inactivo.ToString();
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

        public List<Producto> GetAllActiveProducts()
        {
            List<Producto> activeProducts = new List<Producto>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                activeProducts = context.ProductoSet.ToList();
            }

            return activeProducts;
        }

        public BitmapImage GetImageByProductName(string productName)
        {
            string imageDataInString;

            using (var context = new ItalianPizzaServerBDEntities())
            {
                imageDataInString = context.ProductoSet.Where(p => p.Nombre == productName).First().Foto;
            }

            byte[] imageData = Convert.FromBase64String(imageDataInString);

            BitmapImage imageSource = new BitmapImage();

            if (imageData != null)
            {
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(imageData);
                imageSource.EndInit();
            }

            return imageSource;
        }

        public Producto GetProductByName(string productName)
        {
            Producto product = new Producto();
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    product = context.ProductoSet.Where(p => p.Nombre == productName).FirstOrDefault();
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

        public List<Producto> GetSpecifiedProductsByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<Producto> specifiedProducts = new List<Producto>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Nombre")
                {
                    specifiedProducts = context.ProductoSet.Where(p => p.Nombre.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    //specifiedProducts = context.ProductoSet.Where(p => p.Código.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedProducts;
        }

        public int ModifyProduct(Producto originalProduct, Producto modifiedProduct)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    Producto productFound = context.ProductoSet.Where(p => p.Nombre == originalProduct.Nombre).FirstOrDefault();
                    if (productFound != null)
                    {
                        productFound.Nombre = modifiedProduct.Nombre;
                        productFound.Costo = modifiedProduct.Costo;
                        productFound.Descripcion = modifiedProduct.Descripcion;
                        productFound.Categoria = modifiedProduct.Categoria;
                        productFound.Foto = modifiedProduct.Foto;
                        //productFound.Empleado = modifiedProduct.Empleado;
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
                    Producto product = context.ProductoSet.Where(p => p.Nombre == productName).FirstOrDefault();
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
