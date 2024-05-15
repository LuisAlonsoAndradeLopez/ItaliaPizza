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
    public class SupplyDAO
    {
        public int AddSupply(SupplySet supply)
        {
            int result = 0;
            SupplyPictureSet pictureSet = new SupplyPictureSet
            {
                SupplyImage = supply.Picture
            };

            supply.Picture = null;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.SupplySet.Add(supply);
                    context.SaveChanges();
                    pictureSet.Supply_Id = supply.Id;
                    context.SupplyPictureSet.Add(pictureSet);
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
                            SupplyUnitSet = s.SupplyUnitSet,
                            Observations = s.Observations
                        })
                        .OrderBy(s => s.Name)
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

            SupplyPictureSet pictureSet = new SupplyPictureSet
            {
                SupplyImage = modifiedSupply.Picture
            };

            modifiedSupply.Picture = null;

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
                        pictureSet.Supply_Id = originalSupply.Id;
                        context.SupplyPictureSet.Add(pictureSet);
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
                        .OrderBy(s => s.Name)
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

        public int UpdateSupplyObservations(string supplyName, string observations)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supplyFound = context.SupplySet.Where(s => s.Name == supplyName).FirstOrDefault();
                    if (supplyFound != null)
                    {
                        supplyFound.Observations = observations;
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

        public int UpdateSupplyRegisteredQuantity(string supplyName, int quantity)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplySet supplyFound = context.SupplySet.Where(s => s.Name == supplyName).FirstOrDefault();
                    if (supplyFound != null)
                    {
                        supplyFound.Quantity = quantity;
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

        public int UpdateSupplyInInventory(List<SupplySet> supplySets)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    foreach (var supplySet in supplySets)
                    {
                        SupplySet supplyExisting = context.SupplySet.FirstOrDefault(s => s.Id == supplySet.Id);
                        if (supplyExisting != null)
                        {
                            supplyExisting.Quantity = supplyExisting.Quantity + supplySet.Quantity;
                        }
                    }
                    result = context.SaveChanges();
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

            return result;
        }
    }
}
