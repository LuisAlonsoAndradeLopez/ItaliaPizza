using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItalianPizza.DatabaseModel.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Globalization;
using System.Data.Entity.Core;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass()]
    public class CustomerOrdersDAOTests
    {
        [TestMethod()]
        public void RegisterOrderSuccessfulDomicileOrderTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 1;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 390;
            customerOrder.EmployeeId = 1;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 2;
            productSaleSet.Quantity = 3;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            UserDAO userDAO = new UserDAO();
            CustomerSet customer = userDAO.GetCustomerByCustomerOrder(14);
            DeliveryDriverSet deliveryDriver = userDAO.GetDeliveryDriverByCustomerOrder(14);

            Assert.AreEqual(1, customerOrdersDAO.RegisterCustomerOrder(customerOrder, productSaleSets, customer, deliveryDriver));
        }

        [TestMethod()]
        public void RegisterLocalOrderSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 2;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 390;
            customerOrder.EmployeeId = 1;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 2;
            productSaleSet.Quantity = 3;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            Assert.AreEqual(1, customerOrdersDAO.RegisterCustomerOrder(customerOrder, productSaleSets, null, null));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void NullCustomerOrderRecordTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(0, customerOrdersDAO.RegisterCustomerOrder(customerOrder, null, null, null));
        }

        [TestMethod()]
        [ExpectedException(typeof(EntityException))]
        public void RegisterOfflineCustomerTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(0, customerOrdersDAO.RegisterCustomerOrder(customerOrder, null, null, null));
        }

        [TestMethod()]
        public void ModifyCustomerOrderTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 20;
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 1;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 1;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 3;
            productSaleSet.Quantity = 4;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            UserDAO userDAO = new UserDAO();
            CustomerSet customer = userDAO.GetCustomerByCustomerOrder(14);
            DeliveryDriverSet deliveryDriver = userDAO.GetDeliveryDriverByCustomerOrder(14);

            Assert.AreEqual(3, customerOrdersDAO.ModifyCustomerOrder(customerOrder, productSaleSets, customer, deliveryDriver));
        }

        [TestMethod()]
        public void ModifyCustomerOrderTest2()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 20;
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 2;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 1;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 3;
            productSaleSet.Quantity = 4;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            Assert.AreEqual(3, customerOrdersDAO.ModifyCustomerOrder(customerOrder, productSaleSets, null, null));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
        public void ModifyCustomerOrderTest3()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(3, customerOrdersDAO.ModifyCustomerOrder(customerOrder, null, null, null));
        }

        [TestMethod()]
        public void CancelCustomerOrderTest2()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 20;
            customerOrder.OrderStatusId = 15;
            customerOrder.OrderTypeId = 2;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 1;

            Assert.AreEqual(1, customerOrdersDAO.CancelCustomerOrder(customerOrder));
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void CancelCustomerOrderTestNull()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(1, customerOrdersDAO.CancelCustomerOrder(customerOrder));
        }

        [TestMethod()]
        public void list()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            List<CustomerOrderSet> orders = new List<CustomerOrderSet>();
            CustomerOrderSet customerOrder = new CustomerOrderSet()
            {
                Id = 17,
                OrderDate = new DateTime(2024 - 04 - 01),
                TotalAmount = 390,
                OrderStatusId = 2,
                OrderTypeId = 1,
                EmployeeId = 1,

            };
            orders.Add(customerOrder);
            CustomerOrderSet customerOrder2 = new CustomerOrderSet()
            {
                Id = 18,
                OrderDate = new DateTime(2024 - 04 - 01),
                TotalAmount = 600,
                OrderStatusId = 2,
                OrderTypeId = 2,
                EmployeeId = 1,

            };
            orders.Add(customerOrder2);

            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByDate(new DateTime(2024, 4, 1));

            Assert.AreEqual(listAux.Count, orders.Count);
        }

        [TestMethod()]
        public void list1()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByDate(new DateTime(2023, 4, 1));

            Assert.AreEqual(0, listAux.Count);
        }

        [TestMethod()]
        public void OrderStatus()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            OrderStatusSet orderStatus = new OrderStatusSet()
            {
                Id = 15,
                Status = "Cancelado"
            };

            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByStatus(orderStatus);

            Assert.AreEqual(2,listAux.Count);
        }

        [TestMethod()]
        public void OrderStatus2()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            OrderStatusSet orderStatus = new OrderStatusSet()
            {
                Id = 1000,
                Status = "No existe"
            };

            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByStatus(orderStatus);

            Assert.AreEqual(0, listAux.Count);
        }




    }
}