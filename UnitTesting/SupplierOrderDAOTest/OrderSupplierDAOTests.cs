using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{

    [TestClass()]
    public class OrderSupplierDAOTests
    {
        OrderSupplierDAO orderSupplierDAO = new OrderSupplierDAO();

        [TestMethod()]
        public void AddSupplierOrderTest()
        {
            SupplierOrderSet supplierOrderSet = new SupplierOrderSet();
            supplierOrderSet.Id = 28;
            supplierOrderSet.OrderStatusId = 1;
            supplierOrderSet.OrderDate = DateTime.Now;
            supplierOrderSet.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            supplierOrderSet.TotalAmount = 390;
            supplierOrderSet.EmployeeId = 2;
            supplierOrderSet.Supplier_Id = 1;

            List<SupplySet> supplyList = new List<SupplySet>();
            SupplySet supply = new SupplySet();
            supply.Id = 20;
            supply.Quantity = 3;
            supply.PricePerUnit = 130;
            supplyList.Add(supply);

            int result = orderSupplierDAO.AddSupplierOrder(supplierOrderSet, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddSupplierOrderTestFail()
        {
            SupplierOrderSet supplierOrderSet = new SupplierOrderSet();

            List<SupplySet> supplyList = new List<SupplySet>();

            int result = orderSupplierDAO.AddSupplierOrder(supplierOrderSet, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void ModifySupplierOrderTest()
        {
            SupplierOrderSet supplierOrderSet = new SupplierOrderSet();
            supplierOrderSet.Id = 23;
            supplierOrderSet.OrderStatusId = 1;
            supplierOrderSet.OrderDate = DateTime.Now;
            supplierOrderSet.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            supplierOrderSet.TotalAmount = 50000;
            supplierOrderSet.EmployeeId = 2;
            supplierOrderSet.Supplier_Id = 1;

            List<SupplySet> supplyList = new List<SupplySet>();
            SupplySet supply = new SupplySet();
            supply.Id = 21;
            supply.Quantity = 10;
            supply.PricePerUnit = 2000;
            supplyList.Add(supply);

            int result = orderSupplierDAO.ModifySupplierOrder(supplierOrderSet, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void ModifySupplierOrderTestFail()
        {
            SupplierOrderSet supplierOrderSet = new SupplierOrderSet();
            supplierOrderSet.Id = -1;

            List<SupplySet> supplyList = new List<SupplySet>();

            int result = orderSupplierDAO.ModifySupplierOrder(supplierOrderSet, supplyList);
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void ModifyOrderStatusTest()
        {
            SupplierOrderSet supplierOrderSet = new SupplierOrderSet();
            supplierOrderSet.Id = 28;
            supplierOrderSet.OrderStatusId = 1;

            int result = orderSupplierDAO.ModifyOrderStatus(supplierOrderSet.Id, 3);
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void GetSupplierOrderbySupplierTest()
        {
            int result = orderSupplierDAO.GetSupplierOrderbySupplier(4).Count;
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void GetSupplierOrdersByDateTest()
        {
            DateTime fechaEspecifica = new DateTime(2024, 5, 15);
            int result = orderSupplierDAO.GetSupplierOrdersByDate(fechaEspecifica).Count;
            Assert.AreEqual(3, result);
        }
    }
}