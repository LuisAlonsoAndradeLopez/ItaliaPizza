using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
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
                    var result = context.EmployeeSet
                        .Include(s => s.EmployeePositionSet)
                        .Include(s => s.UserAccountSet)
                        .Include(s => s.UserStatusSet)
                        .Where(e => e.UserAccountSet.UserName == employeeAccount.UserName && e.UserAccountSet.Password == employeeAccount.Password)
                        .Select(e => new
                        {
                            e.Id,
                            e.Names,
                            e.LastName,
                            e.SecondLastName,
                            e.UserAccount_Id,
                            e.UserStatusId,
                            e.EmployeePositionId,
                            e.Address_Id,
                            e.UserStatusSet,
                            e.EmployeePositionSet,
                            e.UserAccountSet
                        })
                        .FirstOrDefault();

                    if (result != null)
                    {
                        employee = new EmployeeSet
                        {
                            Id = result.Id,
                            Names = result.Names,
                            LastName = result.LastName,
                            SecondLastName = result.SecondLastName,
                            UserAccount_Id = result.UserAccount_Id,
                            UserStatusId = result.UserStatusId,
                            EmployeePositionId = result.EmployeePositionId,
                            Address_Id = result.Address_Id,
                            UserStatusSet = result.UserStatusSet,
                            EmployeePositionSet = result.EmployeePositionSet,
                            UserAccountSet = result.UserAccountSet
                        };
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Error al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error inesperado.", ex);
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
                    context.EmployeePictureSet.Add(new EmployeePictureSet { Employee_Id = employee.Id, EmployeeImage = employee.ProfilePhoto });
                    result = context.SaveChanges();
                }

            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            return result;
        }

        public bool CheckUserExistence(UserAccountSet account)
        {
            bool result = false;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    UserAccountSet userAccount = context.UserAccountSet.Where(u => u.UserName == account.UserName).FirstOrDefault();
                    if (userAccount != null)
                    {
                        result = true;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            return result;
        }

        public bool CheckEmployeeExistence(EmployeeSet employee)
        {
            bool result = false;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    EmployeeSet employeeToCheck = context.EmployeeSet.Where(e => e.Names == employee.Names && e.LastName == employee.LastName && e.SecondLastName == employee.SecondLastName).FirstOrDefault();
                    if (employeeToCheck != null)
                    {
                        result = true;
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            return result;
        }

        public bool CheckEmployeeEmailExistence(EmployeeSet employee)
        {
            bool result = false;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    EmployeeSet employeeToCheck = context.EmployeeSet.Where(e => e.Email == employee.Email).FirstOrDefault();
                    if (employeeToCheck != null)
                    {
                        result = true;
                    }
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

            if (data != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    memoryStream.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
            }

            return bitmapImage;
        }

        public int ModifyEmployee(UserAccountSet account, EmployeeSet employee)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    UserAccountSet userAccountToModify = context.UserAccountSet.Where(u => u.UserName == account.UserName).FirstOrDefault();
                    EmployeeSet employeeToModify = context.EmployeeSet.Where(e => e.Email == employee.Email).FirstOrDefault();
                    EmployeePictureSet employeePicture = context.EmployeePictureSet.Where(p => p.Employee_Id == employee.Id).FirstOrDefault();

                    if (userAccountToModify != null && employeeToModify != null)
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
                        employeePicture.EmployeeImage = employee.ProfilePhoto;
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


        public int ModifyUser(UserAccountSet account, EmployeeSet employee)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    UserAccountSet userAccountToModify = context.UserAccountSet.Where(u => u.UserName == account.UserName).FirstOrDefault();
                    EmployeeSet employeeToModify = context.EmployeeSet.Where(e => e.Email == employee.Email).FirstOrDefault();
                    EmployeePictureSet employeePicture = context.EmployeePictureSet.Where(p => p.Employee_Id == employee.Id).FirstOrDefault();

                    if(employeePicture == null)
                    {
                        employeePicture = new EmployeePictureSet();
                        employeePicture.Employee_Id = employeeToModify.Id;
                        employeePicture.EmployeeImage = employee.ProfilePhoto;
                        context.EmployeePictureSet.Add(employeePicture);
                    }

                    if (userAccountToModify != null && employeeToModify != null)
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

        public List<EmployeeSet> GetAllEmployeesByStatus(string status)
        {
            List<EmployeeSet> employees = new List<EmployeeSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employees = context.EmployeeSet.Where(employee => employee.UserStatusSet.Status == status)
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

        public List<EmployeeSet> GetAllEmployeesByPosition(string position)
        {
            List<EmployeeSet> employees = new List<EmployeeSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    employees = context.EmployeeSet.Where(employee => employee.EmployeePositionSet.Position == position)
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
                        .OrderBy(c => c.Names)
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
                        .Include(customerAux => customerAux.AddressSet)
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
            CustomerSet customer = null;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetail = context.CustomerOrderCustomerSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.CustomerSet)
                        .Include(customerOrderDetailAux => customerOrderDetailAux.CustomerSet.AddressSet)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    if (customerOrderDetail != null)
                    {
                        customer = customerOrderDetail.CustomerSet;
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

            return customer;
        }

        public DeliveryDriverSet GetDeliveryDriverByCustomerOrder(int customerOrderID)
        {
            DeliveryDriverSet deliveryDriver = null;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetail = context.CustomerOrderDeliveryDriverSet
                        .Include(customerOrderDetailAux => customerOrderDetailAux.DeliveryDriverSet)
                        .FirstOrDefault(customerOrderDetailAux => customerOrderDetailAux.CustomerOrderId == customerOrderID);

                    if (customerOrderDetail != null)
                    {
                        deliveryDriver = customerOrderDetail.DeliveryDriverSet;
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

            return deliveryDriver;
        }

        public List<UserStatusSet> GetAllUserStatus()
        {
            List<UserStatusSet> statusList = new List<UserStatusSet>();
            using (var context = new ItalianPizzaServerBDEntities())
            {
                statusList = context.UserStatusSet.ToList();
            }
            return statusList;
        }

        public EmployeePictureSet GetUserPicturebyID(int userID)
        {
            EmployeePictureSet userPicture;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    userPicture = context.EmployeePictureSet.FirstOrDefault(p => p.Employee_Id == userID);
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

            return userPicture;
        }

        public int AddCustomer(CustomerSet customer, AddressSet customerAddress)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.CustomerSet.Add(customer);
                    context.AddressSet.Add(customerAddress);
                    context.SaveChanges();
                    result = 1;
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

        public int UpdateCustomer(CustomerSet customerSet, AddressSet customerAddress)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {

                    CustomerSet customer = context.CustomerSet.FirstOrDefault(dD => dD.Id == customerSet.Id);
                    customer.Names = customerSet.Names;
                    customer.LastName = customerSet.LastName;
                    customer.SecondLastName = customerSet.SecondLastName;
                    customer.Email = customerSet.Email;
                    customer.Phone = customerSet.Phone;

                    AddressSet address = context.AddressSet.FirstOrDefault(a => a.Id == customerAddress.Id);
                    address.StreetNumber = customerAddress.StreetNumber;
                    address.City = customerAddress.City;
                    address.State = customerAddress.State;
                    address.StreetName = customerAddress.StreetName;
                    address.Colony = customerAddress.Colony;
                    address.ZipCode = customerAddress.ZipCode;

                    context.SaveChanges();
                    result = 1;
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

        public int AddDeliveryDriver(DeliveryDriverSet deliveryDriver)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.DeliveryDriverSet.Add(deliveryDriver);
                    context.SaveChanges();
                    result = 1;
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

        public int UpdateDeliveryDriver(DeliveryDriverSet deliveryDriver)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {

                    DeliveryDriverSet deliveryDriverSet = context.DeliveryDriverSet.FirstOrDefault(dD => dD.Id == deliveryDriver.Id);
                    deliveryDriverSet.Names = deliveryDriver.Names;
                    deliveryDriverSet.LastName = deliveryDriver.LastName;
                    deliveryDriverSet.SecondLastName = deliveryDriver.SecondLastName;
                    deliveryDriverSet.Email = deliveryDriver.Email;
                    deliveryDriverSet.Phone = deliveryDriver.Phone;
                    context.SaveChanges();
                    result = 1;
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

        public int DeleteDeliveryDriver(DeliveryDriverSet deliveryDriver)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.DeliveryDriverSet.Remove(deliveryDriver);
                    context.SaveChanges();
                    result = 1;
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

        public int RegisterAddress(AddressSet address)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.AddressSet.Add(address);
                    context.SaveChanges();
                    result = address.Id;
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
