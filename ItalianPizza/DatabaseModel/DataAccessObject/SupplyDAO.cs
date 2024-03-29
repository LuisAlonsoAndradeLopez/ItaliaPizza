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
            string imageDataInString;

            using (var context = new ItalianPizzaServerBDEntities())
            {
                imageDataInString = context.SupplySet.Where(s => s.Name == supplyName).First().Picture.ToString();
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
                if (findByType == "Name")
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
    }
}
