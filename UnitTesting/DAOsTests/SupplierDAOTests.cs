using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItalianPizza.DatabaseModel.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Globalization;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass()]
    public class SupplierDAOTests
    {
        SupplierDAO _supplierDAO = new SupplierDAO();
        [TestMethod()]
        public void ModifySupplierTestSuccessful()
        {
            SupplierSet supplier = new SupplierSet();
            supplier.Id = 12;
            supplier.Email = "Carter@gmail.com";
            supplier.CompanyName = "Abarrotes 'Perez'";
            supplier.EmployeeId = 2;
            supplier.SecondLastName = "Perez";
            supplier.Names = "Carter";
            supplier.LastName = "Moreno";
            supplier.Phone = "2731010078";
            supplier.UserStatusId = 1;

            List<SupplySet> supplyList = new List<SupplySet>();
            SupplySet supply = new SupplySet();
            supply.Id = 20;
            supply.Quantity = 3;
            supply.PricePerUnit = 130;
            supplyList.Add(supply);

            int result = _supplierDAO.AddSupplier(supplier, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void AddSupplierTestSuccessful()
        {
            SupplierSet supplier = new SupplierSet();
            supplier.Email = "babo@gmail.com";
            supplier.CompanyName = "Babilonia Studio";
            supplier.EmployeeId = 2;
            supplier.SecondLastName = "Perez";
            supplier.Names = "Raymon";
            supplier.LastName = "Garcia";
            supplier.Phone = "2203891213";
            supplier.UserStatusId = 1;

            List<SupplySet> supplyList = new List<SupplySet>();
            SupplySet supply = new SupplySet();
            supply.Id = 20;
            supply.Quantity = 3;
            supply.PricePerUnit = 130;
            supplyList.Add(supply);

            int result = _supplierDAO.AddSupplier(supplier, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void GetAllSuppliersBySupplyTestSuccessful()
        {
            int result = _supplierDAO.GetAllSuppliersBySupply("Lechuga Bola").Count;
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void GetAllSuppliersTestSuccessful()
        {
            int result = _supplierDAO.GetAllSuppliers().Count;
            Assert.AreEqual(8, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void ModifySupplierTestFail()
        {
            SupplierSet supplier = new SupplierSet();
            List<SupplySet> supplyList = new List<SupplySet>();
            int result = _supplierDAO.AddSupplier(supplier, supplyList);
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddSupplierTestFail()
        {
            SupplierSet supplier = new SupplierSet();
            List<SupplySet> supplyList = new List<SupplySet>();
            int result = _supplierDAO.AddSupplier(supplier, supplyList);
            Assert.AreEqual(1, result);
        }
    }
}