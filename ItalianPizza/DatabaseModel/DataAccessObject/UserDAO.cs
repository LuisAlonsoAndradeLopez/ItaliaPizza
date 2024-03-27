using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class UserDAO
    {
        public UserDAO() { }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customerList = context.CustomerSet.ToList();
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

            return customerList;
        }

        public List<DeliveryDriver> GetAllDeliveryDriver()
        {
            List<DeliveryDriver> deliveryDriverList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    deliveryDriverList = context.DeliveryDriverSet.ToList();
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

            return deliveryDriverList;
        }

        public Customer GetCustomerByCustomerOrder(int customerOrderID)
        {
            Customer customer = new Customer();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrder = context.CustomerOrderSet
                        .Include(co => co.Customer)
                        .FirstOrDefault(co => co.Id == customerOrderID);

                    int count = customerOrder.Customer.Count;

                    customer = customerOrder.Customer.First();
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

            return customer;
        }

        public DeliveryDriver GetDeliveryDriverByCustomerOrder(int customerOrderID)
        {
            DeliveryDriver deliveryDriver = new DeliveryDriver();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrder = context.CustomerOrderSet
                        .Include(co => co.DeliveryDriver)
                        .FirstOrDefault(co => co.Id == customerOrderID);

                    deliveryDriver = customerOrder.DeliveryDriver.First();
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

            return deliveryDriver;
        }
    }
}
