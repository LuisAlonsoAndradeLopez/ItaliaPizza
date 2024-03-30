using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class CustomerOrdersDAO
    {
        public CustomerOrdersDAO() { }

        public int RegisterCustomerOrder(CustomerOrder customerOrder, List<ProductSale> productsOrderCustomer, Customer customer, DeliveryDriver deliveryDriver)
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
                        
                        if(customerOrder.OrderType.Type == "Pedido Domicilio")
                        {
                            CustomerOrderCustomer customerOrderCustomer = new CustomerOrderCustomer
                            {
                                CustomerId = customer.Id,
                                CustomerOrderId = customerOrder.Id
                            };
                            CustomerOrderDeliveryDriver customerOrderDeliveryDriver = new CustomerOrderDeliveryDriver
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
                            CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
                            customerOrderDetail.CustomerOrderId = customerOrder.Id;
                            customerOrderDetail.ProductSaleId = product.Id;
                            customerOrderDetail.ProductQuantity = product.Quantity;
                            customerOrderDetail.PricePerUnit = product.PricePerUnit;
                            context.CustomerOrderDetailSet.Add(customerOrderDetail);
                            context.SaveChanges();
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


        public List<CustomerOrder> GetAllCustomerOrders()
        {
            List<CustomerOrder> orders = new List<CustomerOrder>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orders = context.CustomerOrderSet.ToList();
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
           
            return orders;
        }

        public List<CustomerOrder> GetCustomerOrdersByDate(DateTime date)
        {
            List<CustomerOrder> customerOrders = new List<CustomerOrder>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    DateTime startDate = date.Date;
                    DateTime endDate = startDate.AddDays(1).AddTicks(-1);

                    customerOrders = context.CustomerOrderSet
                        .Include(customerOrder => customerOrder.OrderType)
                        .Include(customerOrder => customerOrder.OrderStatus)
                        .Where(customerOrder => customerOrder.OrderDate >= startDate && customerOrder.OrderDate <= endDate)
                        .ToList();
                    //orders = context.CustomerOrderSetSet.Where(o => o.Fecha == date).ToList();
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
            
            return orders;
        }

        public List<CustomerOrderSet> GetCustomerOrdersByStatus(String status)
        {
            List<CustomerOrderSet> orders = new List<CustomerOrderSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    //orders = context.CustomerOrderSet.Where(o => o.OrderStatusSet == status).ToList();
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

        public List<CustomerOrder> GetCustomerOrdersByStatus(OrderStatus status)
        {
            List<CustomerOrder> customerOrders = new List<CustomerOrder>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customerOrders = context.CustomerOrderSet
                                            .Include(customerOrder => customerOrder.OrderStatus)
                                            .Include(customerOrder => customerOrder.OrderType)
                                            .Where(customerOrder => customerOrder.OrderStatus.Status == status.Status)
                                            .ToList();
                }
            }
            catch (Exception ex) when (ex is EntityException || ex is InvalidOperationException)
            {
                throw new Exception("Error al acceder a la base de datos.", ex);
            }

            return customerOrders;
        }


        public List<OrderStatus> GetOrderStatuses()
        {
            List<OrderStatus> orderStatuses;

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

        public List<OrderType> GetOrderTypes()
        {
            List<OrderType> orderTypes;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orderTypes = context.OrderTypeSet.ToList();
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
    }
}
