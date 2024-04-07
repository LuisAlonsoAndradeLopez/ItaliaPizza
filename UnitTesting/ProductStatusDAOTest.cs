using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductStatusDAOTest
    {
        private static ProductStatusDAO productStatusDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            productStatusDAO = new ProductStatusDAO();
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
        public void GetAllProductStatusesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ProductStatusSet> productStatuses = productStatusDAO.GetAllProductStatuses();

                Assert.IsNotNull(productStatuses);
                Assert.IsTrue(productStatuses.Count > 0);
            }
        }

        [TestMethod]
        public void GetProductStatusByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                ProductStatusSet productStatus = productStatusDAO.GetProductStatusById(validId);

                Assert.IsNotNull(productStatus);
                Assert.AreEqual(validId, productStatus.Id);
            }
        }

        [TestMethod]
        public void GetProductStatusByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                ProductStatusSet productStatus = productStatusDAO.GetProductStatusById(invalidId);

                Assert.IsNull(productStatus);
            }
        }

        [TestMethod]
        public void GetProductStatusByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Activo";
                ProductStatusSet productStatus = productStatusDAO.GetProductStatusByName(validName);

                Assert.IsNotNull(productStatus);
                Assert.AreEqual(validName, productStatus.Status);
            }
        }

        [TestMethod]
        public void GetProductStatusByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Proactivo";
                ProductStatusSet productStatus = productStatusDAO.GetProductStatusByName(invalidName);

                Assert.IsNull(productStatus);
            }
        }
    }
}