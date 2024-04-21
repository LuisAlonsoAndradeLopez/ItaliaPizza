using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class SupplierDAO
    {
        public SupplierDAO() { }

        public List<SupplierSet> GetAllSuppliers()
        {
            List<SupplierSet> suppliers;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    suppliers = context.SupplierSet
                        .Include(supplier => supplier.UserStatusSet)
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

        public List<SupplierSet> GetAllSuppliersBySupply(string supplyName)
        {
            List<SupplierSet> suppliers;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    suppliers = context.SupplierSet
                        .Include(supplier => supplier.UserStatusSet)
                        .Where(supplier => supplier.SupplySet.Any(supply => supply.Name == supplyName))
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

        public int ModifySupplier(SupplierSet supplier, List<SupplySet> supplies)
        {
            int result = 0;
            SupplierSet supplierSet = null;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    supplierSet = context.SupplierSet.Include(s => s.SupplySet).FirstOrDefault(s => s.Id == supplier.Id);
                    if (supplierSet != null)
                    {
                        supplierSet.Names = supplier.Names;
                        supplierSet.LastName = supplier.LastName;
                        supplierSet.SecondLastName = supplier.SecondLastName;
                        supplierSet.CompanyName = supplier.CompanyName;
                        supplierSet.Phone = supplier.Phone;
                        supplierSet.Email = supplier.Email;
                        supplierSet.UserStatusId = supplier.UserStatusId;
                        supplierSet.SupplySet.Clear();

                        foreach (SupplySet supply in supplies)
                        {
                            SupplySet supplyAux = context.SupplySet.FirstOrDefault(s => s.Id == supply.Id);
                            supplierSet.SupplySet.Add(supplyAux);
                        }

                        result = context.SaveChanges();
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
            
            return result;

        }

        public int AddSupplier(SupplierSet supplier, List<SupplySet> supplies)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.SupplierSet.Add(supplier);

                    foreach (SupplySet supply in supplies)
                    {
                        SupplySet supplyAux = context.SupplySet.FirstOrDefault(s => s.Id == supply.Id);
                        supplier.SupplySet.Add(supplyAux);
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
