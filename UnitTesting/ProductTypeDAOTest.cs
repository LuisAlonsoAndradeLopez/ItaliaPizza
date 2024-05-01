using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductTypeDAOTest
    {
        private static ProductTypeDAO productTypeDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            productTypeDAO = new ProductTypeDAO();
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
        public void GetAllProductTypesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ProductTypeSet> productTypes = productTypeDAO.GetAllProductTypes();

                Assert.IsNotNull(productTypes);
                Assert.IsTrue(productTypes.Count > 0);
            }
        }

        [TestMethod]
        public void GetProductTypeByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                ProductTypeSet productType = productTypeDAO.GetProductTypeById(validId);

                Assert.IsNotNull(productType);
                Assert.AreEqual(validId, productType.Id);
            }
        }

        [TestMethod]
        public void GetProductTypeByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                ProductTypeSet productType = productTypeDAO.GetProductTypeById(invalidId);

                Assert.IsNull(productType);
            }
        }

        [TestMethod]
        public void GetProductTypeByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Pizzas";
                ProductTypeSet productType = productTypeDAO.GetProductTypeByName(validName);

                Assert.IsNotNull(productType);
                Assert.AreEqual(validName, productType.Type);
            }
        }

        [TestMethod]
        public void GetProductTypeByNameTestTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Harina";
                ProductTypeSet productType = productTypeDAO.GetProductTypeByName(invalidName);

                Assert.IsNull(productType);
            }
        }
    }
}