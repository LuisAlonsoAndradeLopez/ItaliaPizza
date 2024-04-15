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
    public class OrderSupplierDAO
    {
        public OrderSupplierDAO() { }

        public List<SupplierOrderSet> GetSupplierOrdersByDate(DateTime date)
        {
            List<SupplierOrderSet> supplierOrdersList;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    DateTime startDate = date.Date;
                    DateTime endDate = startDate.AddDays(1).AddTicks(-1);

                    supplierOrdersList = context.SupplierOrderSet
                        .Include(supplierOrder => supplierOrder.SupplierSet)
                        .Where(supplierOrder => supplierOrder.OrderDate >= startDate && supplierOrder.OrderDate <= endDate)
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

            return supplierOrdersList;
        }
    }
}
