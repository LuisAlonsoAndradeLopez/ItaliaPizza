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
                    suppliers = context.SupplierSet.ToList();
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

    }
}
