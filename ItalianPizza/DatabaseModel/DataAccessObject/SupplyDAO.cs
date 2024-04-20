using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class SupplyDAO
    {
        public int AddSupply(SupplySet supply)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.SupplySet.Add(supply);
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

        public int DisableSupply(string supplyName)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supplyToDisable = context.SupplySet.Where(s => s.Name == supplyName).FirstOrDefault();
                    if (supplyToDisable != null)
                    {
                        supplyToDisable.ProductStatusId = 2;
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

        public BitmapImage GetImageBySupplyName(string supplyName)
        {         
            using (var context = new ItalianPizzaServerBDEntities())
            {
                byte[] imageBytes = context.SupplySet.Where(s => s.Name == supplyName).First().Picture;             

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

        public SupplySet GetSupplyByName(string supplyName)
        {
            SupplySet supply = new SupplySet();
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    supply = context.SupplySet.Where(s => s.Name == supplyName).FirstOrDefault();
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

            return supply;
        }

        public List<SupplySet> GetSpecifiedSuppliesByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<SupplySet> specifiedSupplies = new List<SupplySet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Nombre")
                {
                    specifiedSupplies = context.SupplySet.Where(s => s.Name.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    specifiedSupplies = context.SupplySet.Where(s => s.IdentificationCode.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedSupplies;
        }

        public int ModifySupply(SupplySet originalSupply, SupplySet modifiedSupply)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supplyFound = context.SupplySet.Where(s => s.Name == originalSupply.Name).FirstOrDefault();
                    if (supplyFound != null)
                    {
                        supplyFound.Name = modifiedSupply.Name;
                        supplyFound.Quantity = modifiedSupply.Quantity;
                        supplyFound.PricePerUnit = modifiedSupply.PricePerUnit;
                        supplyFound.Picture = modifiedSupply.Picture;
                        supplyFound.SupplyUnitId = modifiedSupply.SupplyUnitId;
                        supplyFound.SupplyTypeId = modifiedSupply.SupplyTypeId;
                        supplyFound.EmployeeId = modifiedSupply.EmployeeId;
                        supplyFound.IdentificationCode = modifiedSupply.IdentificationCode;
                        context.SaveChanges();
                        generatedID = (int)supplyFound.Id;
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

        public bool TheCodeIsAlreadyRegistred(string supplyCode)
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supply = context.SupplySet.Where(s => s.IdentificationCode == supplyCode).FirstOrDefault();
                    if (supply != null)
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

        public bool TheNameIsAlreadyRegistred(string supplyName)
        {
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supply = context.SupplySet.Where(s => s.Name == supplyName).FirstOrDefault();
                    if (supply != null)
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

        public List<SupplySet> GetAllSupplyWithoutPhoto()
        {
            List<SupplySet> supplyList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var supplyProperties = context.SupplySet
                        .Select(s => new
                        {
                            s.Id,
                            s.Name,
                            s.Quantity,
                            s.PricePerUnit,
                            s.SupplyUnitId,
                            s.ProductStatusId,
                            s.SupplyTypeId,
                            s.EmployeeId,
                            s.IdentificationCode
                        })
                        .ToList();

                    supplyList = supplyProperties.Select(sp => new SupplySet
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Quantity = sp.Quantity,
                        PricePerUnit = sp.PricePerUnit,
                        SupplyUnitId = sp.SupplyUnitId,
                        ProductStatusId = sp.ProductStatusId,
                        SupplyTypeId = sp.SupplyTypeId,
                        EmployeeId = sp.EmployeeId,
                        IdentificationCode = sp.IdentificationCode
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

            return supplyList;
        }


        public List<SupplySet> GetAllSupply()
        {
            List<SupplySet> supplyList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    supplyList = context.SupplySet
                        .Include(supply => supply.SupplyUnitSet)
                        .ToList();
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

            return supplyList;
        }

        public List<int> GetAllSuppliesBySupplier(int supplierID)
        {
            List<int> suppliers;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    suppliers = context.SupplySet
                        .Where(supplier => supplier.SupplierSet.Any(supply => supply.Id == supplierID))
                        .Select(supplyAux => supplyAux.Id)
                        .ToList();
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

            return suppliers;
        }

        public List<SupplySet> GetAllSuppliesBySupplierOrder(SupplierOrderSet supplierOrder)
        {
            List<SupplySet> suppliesListOrder;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetails = context.SupplierOrderDetailsSet
                                            .Include(d => d.SupplySet)
                                            .Include(d => d.SupplySet.SupplyUnitSet)
                                            .Where(d => d.SupplierOrderId == supplierOrder.Id)
                                            .ToList();

                    suppliesListOrder = customerOrderDetails.Select(detalle =>
                        new SupplySet
                        {
                            Id = detalle.SupplySet.Id,
                            Name = detalle.SupplySet.Name,
                            Quantity = detalle.SupplyQuantity,
                            SupplyUnitId = detalle.SupplySet.SupplyUnitId,
                            PricePerUnit = detalle.PricePerUnit,
                            SupplyUnitSet = detalle.SupplySet.SupplyUnitSet
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

            return suppliesListOrder;
        }

        public SupplyPictureSet GetSupplyPicturebyID(int supplyID)
        {
            SupplyPictureSet supplyPicture;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    supplyPicture = context.SupplyPictureSet.FirstOrDefault(p => p.Supply_Id == supplyID);
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

            return supplyPicture;
        }
    }
}
