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

        public List<OrdenCliente> GetAllCustomerOrders()
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orders = context.OrdenClienteSet.ToList();
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

        public List<OrdenCliente> GetCustomerOrdersByDate(String date)
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orders = context.OrdenClienteSet.Where(o => o.Fecha == date).ToList();
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

        public List<OrdenCliente> GetCustomerOrdersByStatus(String status)
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    orders = context.OrdenClienteSet.Where(o => o.Estado == status).ToList();
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

        public List<Producto> GetOrderProducts(int orderCustomerID)
        {
            List<Producto> productsOrder = new List<Producto>();

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
