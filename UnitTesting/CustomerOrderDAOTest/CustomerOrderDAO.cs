using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Globalization;

namespace DAOsTests
{
    [TestClass()]
    public class CustomerOrdersDAOTests
    {
        [TestMethod()]
        public void RegisterOrderSuccessfulDomicileOrderTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet
            {
                OrderStatusId = 1,
                OrderTypeId = 1,
                OrderDate = DateTime.Now,
                RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                TotalAmount = 390,
                EmployeeId = 2
            };

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet
            {
                Id = 24,
                Quantity = 3,
                PricePerUnit = 130
            };
            productSaleSets.Add(productSaleSet);

            UserDAO userDAO = new UserDAO();
            CustomerSet customer = userDAO.GetCustomerByCustomerOrder(30);
            DeliveryDriverSet deliveryDriver = userDAO.GetDeliveryDriverByCustomerOrder(30);

            Assert.AreEqual(1, customerOrdersDAO.RegisterCustomerOrder(customerOrder, productSaleSets, customer, deliveryDriver));
        }

        [TestMethod()]
        public void RegisterLocalOrderSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet
            {
                OrderStatusId = 1,
                OrderTypeId = 2,
                OrderDate = DateTime.Now,
                RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                TotalAmount = 390,
                EmployeeId = 2
            };

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet
            {
                Id = 24,
                Quantity = 3,
                PricePerUnit = 130
            };
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
        [ExpectedException(typeof(DbUpdateException))]
        public void RegisterOfflineCustomerTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(0, customerOrdersDAO.RegisterCustomerOrder(customerOrder, null, null, null));
        }

        [TestMethod()]
        public void ModifyCustomerOrderSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 64;
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 1;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 1;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 30;
            productSaleSet.Quantity = 4;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            Assert.AreEqual(1, customerOrdersDAO.ModifyCustomerOrder(customerOrder, productSaleSets));
        }

        [TestMethod()]
        public void ModifyCustomerOrderLocaleSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 65;
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = 2;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 2;

            List<ProductSaleSet> productSaleSets = new List<ProductSaleSet>();
            ProductSaleSet productSaleSet = new ProductSaleSet();
            productSaleSet.Id = 26;
            productSaleSet.Quantity = 4;
            productSaleSet.PricePerUnit = 130;
            productSaleSets.Add(productSaleSet);

            Assert.AreEqual(1, customerOrdersDAO.ModifyCustomerOrder(customerOrder, productSaleSets));
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public void NullModifyCustomerOrderTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            CustomerOrderSet customerOrder = new CustomerOrderSet();

            Assert.AreEqual(3, customerOrdersDAO.ModifyCustomerOrder(customerOrder, null));
        }

        [TestMethod()]
        public void CancelCustomerOrderSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();

            CustomerOrderSet customerOrder = new CustomerOrderSet();
            customerOrder.Id = 65;
            customerOrder.OrderStatusId = 6;
            customerOrder.OrderTypeId = 2;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = 3990;
            customerOrder.EmployeeId = 2;

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
        public void GetCustomerOrdersByDateSuccessfulAlternTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByDate(new DateTime(2023, 4, 1));

            Assert.AreEqual(0, listAux.Count);
        }

        [TestMethod()]
        public void GetCustomerOrdersByStatusSuccessfulTest()
        {
            CustomerOrdersDAO customerOrdersDAO = new CustomerOrdersDAO();
            OrderStatusSet orderStatus = new OrderStatusSet()
            {
                Id = 15,
                Status = "Pedido Cancelado"
            };

            List<CustomerOrderSet> listAux = customerOrdersDAO.GetCustomerOrdersByStatus(orderStatus);

            Assert.AreEqual(12, listAux.Count);
        }

        [TestMethod()]
        public void GetCustomerOrdersByStatusNoExistTest()
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
