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

        public EmployeeSet CheckEmployeeExistencebyLogin(UserAccountSet employeeAccount)
        {
            EmployeeSet employee = null;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employee = context.EmployeeSet
                        .Include(employeeAux => employeeAux.AddressSet)
                        .Include(employeeAux => employeeAux.UserAccountSet)
                        .Include(employeeAux => employeeAux.EmployeePositionSet)
                        .Include(employeeAux => employeeAux.UserStatusSet)
                        .FirstOrDefault(employeeAux => employeeAux.UserAccountSet.UserName == employeeAccount.UserName && employeeAux.UserAccountSet.Password == employeeAccount.Password);
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

        public List<CustomerSet> GetAllCustomers()
        {
            List<CustomerSet> customerList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    customerList = context.CustomerSet
                        .Include(customer => customer.AddressSet)
                        .Include(customer => customer.EmployeeSet)
                        .Include(customer => customer.UserStatusSet)
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

        public CustomerSet GetCustomersByID(int customerID)
        {
            CustomerSet customer;

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

        public List<DeliveryDriverSet> GetAllDeliveryDriver()
        {
            List<DeliveryDriverSet> deliveryDriverList;

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

        public CustomerSet GetCustomerByCustomerOrder(int customerOrderID)
        {
            CustomerSet customer = new CustomerSet();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetail = context.CustomerOrderCustomerSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.CustomerSet)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    customer = customerOrderDetail.CustomerSet;
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

        public DeliveryDriverSet GetDeliveryDriverByCustomerOrder(int customerOrderID)
        {
            DeliveryDriverSet deliveryDriver = new DeliveryDriverSet();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetail = context.CustomerOrderDeliveryDriverSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.DeliveryDriverSet)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    deliveryDriver = customerOrderDetail.DeliveryDriverSet;
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
