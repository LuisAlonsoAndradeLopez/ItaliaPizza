using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyTypeDAOTest
    {
        private static SupplyTypeDAO supplyTypeDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            supplyTypeDAO = new SupplyTypeDAO();
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
        public void GetAllSupplyTypesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<SupplyTypeSet> supplyTypes = supplyTypeDAO.GetAllSupplyTypes();

                Assert.IsNotNull(supplyTypes);
                Assert.IsTrue(supplyTypes.Count > 0);
            }
        }

        [TestMethod]
        public void GetSupplyTypeByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeById(validId);

                Assert.IsNotNull(supplyType);
                Assert.AreEqual(validId, supplyType.Id);
            }
        }

        [TestMethod]
        public void GetSupplyTypeByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeById(invalidId);

                Assert.IsNull(supplyType);
            }
        }

        [TestMethod]
        public void GetSupplyTypeByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Cereales";
                SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeByName(validName);

                Assert.IsNotNull(supplyType);
                Assert.AreEqual(validName, supplyType.Type);
            }
        }

        [TestMethod]
        public void GetSupplyTypeByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Boja";
                SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeByName(invalidName);

                Assert.IsNull(supplyType);
            }
        }
    }
}