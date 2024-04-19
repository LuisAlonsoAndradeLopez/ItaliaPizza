using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

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

        public int DeleteProduct(ProductSaleSet product)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.ProductSaleSet.Remove(product);
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

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    activeProducts = context.ProductSaleSet.AsNoTracking().Where(p => p.ProductStatusId == 1).ToList();
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

            return activeProducts;
        }

        public int DecreaseSuppliesOnSale(List<RecipeDetailsSet> recipeIngredients)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in recipeIngredients)
                        {
                            SupplySet supply = context.SupplySet.FirstOrDefault(supplyAux => supplyAux.Id == product.SupplyId);
                            supply.Quantity -= product.Quantity;
                            if (supply.Quantity >= 0)
                            {
                                result = context.SaveChanges();
                            }
                            else
                            {
                                result = -1;
                                break;
                            }
                        }

                        if (result != -1)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (EntityException ex)
                    {
                        transaction.Rollback();
                        throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }

        public int RestoreSuppliesOnSale(List<RecipeDetailsSet> recipeIngredients)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in recipeIngredients)
                        {
                            SupplySet supply = context.SupplySet.FirstOrDefault(supplyAux => supplyAux.Id == product.SupplyId);
                            supply.Quantity += product.Quantity;
                            result = context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (EntityException ex)
                    {
                        transaction.Rollback();
                        throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }


        public BitmapImage GetImageByProductName(string productName)
        {
            using (var context = new ItalianPizzaServerBDEntities())
            {
                byte[] imageBytes = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().FirstOrDefault(ps => ps.Name == productName).Picture;

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

        public List<ProductSaleSet> GetProductsForInventoryReport()
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var activeSupplies = context.ProductSaleSet
                        .Where(p => p.ProductStatusSet.Status == ArticleStatus.Activo.ToString())
                        .ToList()
                        .Select(p => new ProductSaleSet
                        {
                            IdentificationCode = p.IdentificationCode,
                            Name = p.Name,
                            PricePerUnit = p.PricePerUnit,
                            Quantity = p.Quantity,
                            ProductStatusSet = p.ProductStatusSet,
                            ProductTypeSet = p.ProductTypeSet
                        })
                        .ToList();

                    return activeSupplies;
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

        public ProductSaleSet GetProductByName(string productName)
        {
            ProductSaleSet product = new ProductSaleSet();
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    product = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().FirstOrDefault(p => p.Name == productName);
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
                if (findByType == "Nombre")
                {
                    specifiedProducts = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().Where(p => p.Name.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    specifiedProducts = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().Where(p => p.IdentificationCode.StartsWith(textForFindingArticle)).ToList();
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

        public bool TheCodeIsAlreadyRegistred(string productCode)
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ProductSaleSet product = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().Where(p => p.IdentificationCode == productCode).FirstOrDefault();
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

        public bool TheNameIsAlreadyRegistred(string productName)
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    ProductSaleSet product = context.ProductSaleSet.Include(p => p.Picture).AsNoTracking().Where(p => p.Name == productName).FirstOrDefault();
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

        public List<ProductSaleSet> GetOrderProducts(CustomerOrderSet customerOrder)
        {
            List<ProductSaleSet> customerOrderProducts;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetails = context.CustomerOrderDetailSet
                                            .Include(d => d.ProductSaleSet)
                                            .Where(d => d.CustomerOrderId == customerOrder.Id)                                            
                                            .ToList();

                    customerOrderProducts = customerOrderDetails.Select(detalle =>
                        new ProductSaleSet
                        {
                            Id = detalle.ProductSaleSet.Id,
                            Name = detalle.ProductSaleSet.Name,
                            Quantity = detalle.ProductQuantity,
                            PricePerUnit = detalle.PricePerUnit
                        }).ToList();
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

            return customerOrderProducts;

        }

        public List<ProductTypeSet> GetAllProductTypes()
        {
            List<ProductTypeSet> productTypesList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    productTypesList = context.ProductTypeSet.AsNoTracking().ToList();
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

            return productTypesList;
        }

        public List<ProductStatusSet> GetAllProductStatuses()
        {
            List<ProductStatusSet> productStatusesList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    productStatusesList = context.ProductStatusSet.AsNoTracking().ToList();
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

            return productStatusesList;
        }

    }
}
