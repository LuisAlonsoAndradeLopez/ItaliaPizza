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

        public int DeleteSupply(SupplySet supply)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.SupplySet.Remove(supply);
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

        public List<SupplySet> GetSuppliesForInventoryReport()
        {          
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var activeSupplies = context.SupplySet
                        .Where(s => s.ProductStatusSet.Status == ArticleStatus.Activo.ToString())
                        .ToList()
                        .Select(s => new SupplySet
                        {
                            IdentificationCode = s.IdentificationCode,
                            Name = s.Name,
                            PricePerUnit = s.PricePerUnit,
                            Quantity = s.Quantity,
                            ProductStatusSet = s.ProductStatusSet,
                            SupplyTypeSet = s.SupplyTypeSet,
                            SupplyUnitSet = s.SupplyUnitSet
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

        public BitmapImage GetImageBySupplyName(string supplyName)
        {         
            using (var context = new ItalianPizzaServerBDEntities())
            {
                byte[] imageBytes = context.SupplySet.AsNoTracking().FirstOrDefault(s => s.Name == supplyName).Picture;             

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
                    supply = context.SupplySet.AsNoTracking().FirstOrDefault(s => s.Name == supplyName);
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
                    specifiedSupplies = context.SupplySet.AsNoTracking().Where(s => s.Name.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    specifiedSupplies = context.SupplySet.AsNoTracking().Where(s => s.IdentificationCode.StartsWith(textForFindingArticle)).ToList();
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
                    SupplySet supply = context.SupplySet.AsNoTracking().FirstOrDefault(s => s.IdentificationCode == supplyCode);
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
                    SupplySet supply = context.SupplySet.AsNoTracking().FirstOrDefault(s => s.Name == supplyName);
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
    }
}
