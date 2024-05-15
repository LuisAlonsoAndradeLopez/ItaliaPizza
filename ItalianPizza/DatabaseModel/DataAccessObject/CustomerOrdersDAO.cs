using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;


namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class CustomerOrdersDAO
    {
        public CustomerOrdersDAO() { }

        public int RegisterCustomerOrder(CustomerOrderSet customerOrder, List<ProductSaleSet> productsOrderCustomer, CustomerSet customer, DeliveryDriverSet deliveryDriver)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        context.CustomerOrderSet.Add(customerOrder);
                        result = context.SaveChanges();

                        if (customerOrder.OrderTypeId == 1)
                        {
                            CustomerOrderCustomerSet customerOrderCustomer = new CustomerOrderCustomerSet
                            {
                                CustomerId = customer.Id,
                                CustomerOrderId = customerOrder.Id
                            };
                            CustomerOrderDeliveryDriverSet customerOrderDeliveryDriver = new CustomerOrderDeliveryDriverSet
                            {
                                CustomerOrderId = customerOrder.Id,
                                DeliveryDriverId = deliveryDriver.Id
                            };
                            context.CustomerOrderCustomerSet.Add(customerOrderCustomer);
                            context.SaveChanges();
                            context.CustomerOrderDeliveryDriverSet.Add(customerOrderDeliveryDriver);
                            context.SaveChanges();
                        }

                        foreach (var product in productsOrderCustomer)
                        {
                            CustomerOrderDetailSet customerOrderDetail = new CustomerOrderDetailSet();
                            customerOrderDetail.CustomerOrderId = customerOrder.Id;
                            customerOrderDetail.ProductSaleId = product.Id;
                            customerOrderDetail.ProductQuantity = product.Quantity;
                            customerOrderDetail.PricePerUnit = product.PricePerUnit;
                            context.CustomerOrderDetailSet.Add(customerOrderDetail);
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
                    catch (DbUpdateException ex)
                    {
                        transaction.Rollback();
                        throw new DbUpdateException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }

        public int ModifyCustomerOrder(CustomerOrderSet customerOrder, List<ProductSaleSet> productsOrderCustomer)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CustomerOrderSet OriginalCustomerOrder = context.CustomerOrderSet
                                        .FirstOrDefault(CustomerOrder => CustomerOrder.Id == customerOrder.Id);

                        OriginalCustomerOrder.TotalAmount = customerOrder.TotalAmount;

                        var productIdsInCustomerOrder = productsOrderCustomer.Select(p => p.Id).ToList();
                        var productsToRemove = OriginalCustomerOrder.CustomerOrderDetailSet
                            .Where(detail => !productIdsInCustomerOrder.Contains(detail.ProductSaleId))
                            .ToList();

                        foreach (var productToRemove in productsToRemove)
                        {
                            context.CustomerOrderDetailSet.Remove(productToRemove);
                        }

                        foreach (var product in productsOrderCustomer)
                        {
                            var existingDetail = OriginalCustomerOrder.CustomerOrderDetailSet.FirstOrDefault(detail => detail.ProductSaleId == product.Id);
                            if (existingDetail != null)
                            {
                                existingDetail.ProductQuantity = product.Quantity;
                            }
                            else
                            {
                                var newDetail = new CustomerOrderDetailSet
                                {
                                    CustomerOrderId = OriginalCustomerOrder.Id,
                                    ProductSaleId = product.Id,
                                    ProductQuantity = product.Quantity,
                                    PricePerUnit = product.PricePerUnit
                                };
                                OriginalCustomerOrder.CustomerOrderDetailSet.Add(newDetail);
                            }
                        }

                        result = context.SaveChanges();
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
                    catch (NullReferenceException ex)
                    {
                        transaction.Rollback();
                        throw new DbUpdateException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }

        public int CancelCustomerOrder(CustomerOrderSet customerOrder)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    CustomerOrderSet OriginalCustomerOrder = context.CustomerOrderSet
                                            .FirstOrDefault(CustomerOrder => CustomerOrder.Id == customerOrder.Id);

                    OriginalCustomerOrder.OrderStatusId = customerOrder.OrderStatusId;
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
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public List<CustomerOrderSet> GetCustomerOrdersByDate(DateTime date)
        {
            List<CustomerOrderSet> customerOrders = new List<CustomerOrderSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    DateTime startDate = date.Date;
                    DateTime endDate = startDate.AddDays(1).AddTicks(-1);

                    customerOrders = context.CustomerOrderSet
                    .Include(customerOrder => customerOrder.OrderTypeSet)
                    .Include(customerOrder => customerOrder.OrderStatusSet)
                    .Where(customerOrder => customerOrder.OrderDate >= startDate && customerOrder.OrderDate <= endDate)
                    .OrderBy(customerOrder => customerOrder.OrderStatusId == 1 ? 0 :
                                              customerOrder.OrderStatusId == 3 ? 1 : 3) 
                    .ThenBy(customerOrder => customerOrder.OrderDate)
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

            return customerOrders;
        }

        public List<CustomerOrderSet> GetCustomerOrdersByStatus(OrderStatusSet status)
        {
            List<CustomerOrderSet> customerOrders = new List<CustomerOrderSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customerOrders = context.CustomerOrderSet
                                            .Include(customerOrder => customerOrder.OrderStatusSet)
                                            .Include(customerOrder => customerOrder.OrderTypeSet)
                                            .Where(customerOrder => customerOrder.OrderStatusSet.Status == status.Status)
                                            .OrderByDescending(customerOrder => customerOrder.OrderDate)
                                            .ToList();
                }
            }
            catch (Exception ex) when (ex is EntityException || ex is InvalidOperationException)
            {
                throw new Exception("Error al acceder a la base de datos.", ex);
            }

            return customerOrders;
        }


        public List<OrderStatusSet> GetOrderStatuses()
        {
            List<OrderStatusSet> orderStatuses;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orderStatuses = context.OrderStatusSet.ToList();
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
            return orderStatuses;
        }

        public List<OrderTypeSet> GetOrderTypes()
        {
            List<OrderTypeSet> orderTypes;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orderTypes = context.OrderTypeSet.OrderByDescending(st => st.Type).ToList();
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

            return orderTypes;
        }

        
        public int ModifyOrderStatus(int customerOrderID, int orderStatusID)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    CustomerOrderSet customerOrder = context.CustomerOrderSet.FirstOrDefault(cs => cs.Id == customerOrderID);
                    if (customerOrder != null)
                    {
                        customerOrder.OrderStatusId = orderStatusID;
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

        public int PayCustomerOrder(CustomerOrderSet customerOrder)
        {
            int generatedID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    CustomerOrderSet customerOrderFound = context.CustomerOrderSet.Where(co => co.Id == customerOrder.Id).FirstOrDefault();
                    if (customerOrderFound != null)
                    {
                        customerOrderFound.OrderStatusId = 5;
                        context.SaveChanges();
                        generatedID = (int)customerOrderFound.Id;
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
    }
}