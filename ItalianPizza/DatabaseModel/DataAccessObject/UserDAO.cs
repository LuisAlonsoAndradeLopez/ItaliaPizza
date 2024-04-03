﻿using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

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

        public List<EmployeeSet> GetAllEmployees()
        {
            List<EmployeeSet> employees = new List<EmployeeSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employees = context.EmployeeSet
                        .Include(employee => employee.AddressSet)
                        .Include(employee => employee.EmployeePositionSet)
                        .Include(employee => employee.UserStatusSet)
                        .Include(employee => employee.UserAccountSet)
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

            return employees;
        }

        public UserAccountSet GetUserAccountByEmployeeID(int employeeID)
        {
            UserAccountSet userAccount = new UserAccountSet();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    EmployeeSet employee = context.EmployeeSet.Where(e => e.Id == employeeID).FirstOrDefault();
                    if (employee != null)
                    {
                        userAccount = context.UserAccountSet.Where(u => u.Id == employee.UserAccount_Id).FirstOrDefault();
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

            return userAccount;
        }   


        public int RegisterUser(UserAccountSet account, EmployeeSet employee)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.UserAccountSet.Add(account);
                    employee.UserAccount_Id = account.Id;
                    context.EmployeeSet.Add(employee);
                    result = context.SaveChanges();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            return result;
        }

        public EmployeePositionSet GetEmployeePosition(string employeePosition)
        {
            EmployeePositionSet position = new EmployeePositionSet();
            using (var context = new ItalianPizzaServerBDEntities())
            {
                position = context.EmployeePositionSet.Where(ep => ep.Position == employeePosition).FirstOrDefault();
            }
            return position;
        }

        public UserStatusSet GetUserStatus(string userStatus)
        {
            UserStatusSet status = new UserStatusSet();
            using (var context = new ItalianPizzaServerBDEntities())
            {
                status = context.UserStatusSet.Where(us => us.Status == userStatus).FirstOrDefault();
            }
            return status;
        }

        public BitmapImage GetUserImage(byte[] data)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }


        public int ModifyUser(UserAccountSet account, EmployeeSet employee)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    UserAccountSet userAccountToModify = context.UserAccountSet.Where(u => u.UserName == account.UserName).FirstOrDefault();
                    EmployeeSet employeeToModify = context.EmployeeSet.Where(e => e.Email == employee.Email).FirstOrDefault();

                    if(userAccountToModify != null && employeeToModify != null)
                    {
                        userAccountToModify.UserName = account.UserName;
                        userAccountToModify.Password = account.Password;
                        employeeToModify.Names = employee.Names;
                        employeeToModify.LastName = employee.LastName;
                        employeeToModify.SecondLastName = employee.SecondLastName;
                        employeeToModify.Email = employee.Email;
                        employeeToModify.Phone = employee.Phone;
                        employeeToModify.ProfilePhoto = employee.ProfilePhoto;
                        employeeToModify.UserStatusId = employee.UserStatusId;
                        employeeToModify.EmployeePositionId = employee.EmployeePositionId;
                        employeeToModify.Address_Id = employee.Address_Id;
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

        public String GetUserAddressByEmployeeID(int employeeID)
        {
            String address = "";
            using (var context = new ItalianPizzaServerBDEntities())
            {
                EmployeeSet employee = context.EmployeeSet.Where(e => e.Id == employeeID).FirstOrDefault();
                if (employee != null)
                {
                    AddressSet employeeAddress = context.AddressSet.Where(a => a.Id == employee.Address_Id).FirstOrDefault();
                    if (employeeAddress != null)
                    {
                        address = employeeAddress.StreetName + " " + employeeAddress.StreetNumber + ", " + employeeAddress.City + ", " + employeeAddress.State;
                    }
                }
            }
            return address;
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
