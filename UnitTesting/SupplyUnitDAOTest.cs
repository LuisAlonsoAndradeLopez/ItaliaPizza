using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyUnitDAOTest
    {
        private static SupplyUnitDAO supplyUnitDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            supplyUnitDAO = new SupplyUnitDAO();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using (var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }

        [TestMethod]
        public void GetAllSupplyUnitsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<SupplyUnitSet> supplyUnits = supplyUnitDAO.GetAllSupplyUnits();

                Assert.IsNotNull(supplyUnits);
                Assert.IsTrue(supplyUnits.Count > 0);
            }
        }

        [TestMethod]
        public void GetSupplyUnitByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitById(validId);

                Assert.IsNotNull(supplyUnit);
                Assert.AreEqual(validId, supplyUnit.Id);
            }
        }

        [TestMethod]
        public void GetSupplyUnitByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitById(invalidId);

                Assert.IsNull(supplyUnit);
            }
        }

        [TestMethod]
        public void GetSupplyUnitByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Gramo";
                SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitByName(validName);

                Assert.IsNotNull(supplyUnit);
                Assert.AreEqual(validName, supplyUnit.Unit);
            }
        }

        [TestMethod]
        public void GetSupplyUnitByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Gayeta";
                SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitByName(invalidName);

                Assert.IsNull(supplyUnit);
            }
        }
    }
}