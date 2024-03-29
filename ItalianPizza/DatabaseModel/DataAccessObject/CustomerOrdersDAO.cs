using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class CustomerOrdersDAO
    {
        public CustomerOrdersDAO() { }

        public List<CustomerOrderSet> GetAllCustomerOrders()
        {
            List<CustomerOrderSet> orders = new List<CustomerOrderSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    //orders = context.CustomerOrderSetSet.ToList();
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

        public List<CustomerOrderSet> GetCustomerOrdersByDate(String date)
        {
            List<CustomerOrderSet> orders = new List<CustomerOrderSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
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

            return orders;
        }

        public List<ProductSaleSet> GetOrderProducts(int orderCustomerID)
        {
            List<ProductSaleSet> productsOrder = new List<ProductSaleSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    
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

            return productsOrder;
        }

    }
}
