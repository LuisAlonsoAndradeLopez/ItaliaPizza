using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

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

        public List<SupplierOrderSet> GetSupplierOrderbySupplier(int supplierId)
        {
            List<SupplierOrderSet> supplierOrders;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    supplierOrders = context.SupplierOrderSet
                        .Include(supplierOrder => supplierOrder.SupplierSet)
                        .Where(supplierOrder => supplierOrder.Supplier_Id == supplierId)
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

            return supplierOrders;

        }

        public int AddSupplierOrder(SupplierOrderSet supplierOrder, List<SupplySet> suppliesList)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.SupplierOrderSet.Add(supplierOrder);
                    context.SaveChanges();

                    List<SupplierOrderDetailsSet> supplierOrderDetails = new List<SupplierOrderDetailsSet>();
                    foreach (SupplySet supply in suppliesList)
                    {
                        SupplierOrderDetailsSet supplierOrderDetailsSet = new SupplierOrderDetailsSet
                        {
                            SupplyId = supply.Id,
                            SupplyQuantity = supply.Quantity,
                            PricePerUnit = supply.PricePerUnit,
                            SupplierOrderId = supplierOrder.Id
                        };
                        context.SupplierOrderDetailsSet.Add(supplierOrderDetailsSet);
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

        public int ModifySupplierOrder(SupplierOrderSet supplierOrder, List<SupplySet> suppliesList)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    SupplierOrderSet supplierOrderSetAux = context.SupplierOrderSet.FirstOrDefault(sp => sp.Id == supplierOrder.Id);
                    if (supplierOrderSetAux != null)
                    {
                        supplierOrderSetAux.Supplier_Id = supplierOrder.Supplier_Id;
                        supplierOrderSetAux.OrderStatusId = supplierOrder.OrderStatusId;
                        supplierOrderSetAux.TotalAmount = supplierOrder.TotalAmount;
                    }

                    context.SaveChanges();

                    List<SupplierOrderDetailsSet> inactiveSupplies = context.SupplierOrderDetailsSet.Where(s => s.SupplierOrderId == supplierOrder.Id).ToList();
                    context.SupplierOrderDetailsSet.RemoveRange(inactiveSupplies);

                    List<SupplierOrderDetailsSet> supplierOrderDetails = new List<SupplierOrderDetailsSet>();
                    foreach (SupplySet supply in suppliesList)
                    {
                        SupplierOrderDetailsSet supplierOrderDetailsSet = new SupplierOrderDetailsSet
                        {
                            SupplyId = supply.Id,
                            SupplyQuantity = supply.Quantity,
                            PricePerUnit = supply.PricePerUnit,
                            SupplierOrderId = supplierOrder.Id
                        };
                        context.SupplierOrderDetailsSet.Add(supplierOrderDetailsSet);
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
