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

        public Employee CheckEmployeeExistencebyLogin(UserAccount employeeAccount)
        {
            Employee employee = null;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employee = context.EmployeeSet
                        .Include(employeeAux => employeeAux.Address)
                        .Include(employeeAux => employeeAux.UserAccount)
                        .Include(employeeAux => employeeAux.EmployeePosition)
                        .Include(employeeAux => employeeAux.UserStatus)
                        .FirstOrDefault(employeeAux => employeeAux.UserAccount.UserName == employeeAccount.UserName && employeeAux.UserAccount.Password == employeeAccount.Password);
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

            return employee;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customerList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customerList = context.CustomerSet
                        .Include(customer => customer.Address)
                        .Include(customer => customer.Employee)
                        .Include(customer => customer.UserStatus)
                        .ToList();
                }

                customerList.RemoveAt(0);

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

        public Customer GetCustomersByID(int customerID)
        {
            Customer customer;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customer = context.CustomerSet
                        .Where(customerAux => customerAux.Id == customerID)
                        .First();
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
                    var customerOrderDetail = context.CustomerOrderCustomerSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.Customer)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    customer = customerOrderDetail.Customer;
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
                    var customerOrderDetail = context.CustomerOrderDeliveryDriverSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.DeliveryDriver)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    deliveryDriver = customerOrderDetail.DeliveryDriver;
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
