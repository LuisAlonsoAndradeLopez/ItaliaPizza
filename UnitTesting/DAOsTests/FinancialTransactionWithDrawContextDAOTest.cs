using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class FinancialTransactionWithDrawContextDAOTest
    {
        private static FinancialTransactionWithDrawContextDAO financialTransactionWithDrawContextDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            financialTransactionWithDrawContextDAO = new FinancialTransactionWithDrawContextDAO();
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
        public void GetAllFinancialTransactionWithDrawContextsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<FinancialTransactionWithDrawContextSet> financialTransactionWithDrawContexts = financialTransactionWithDrawContextDAO.GetAllFinancialTransactionWithDrawContexts();

                Assert.IsNotNull(financialTransactionWithDrawContexts);
                Assert.IsTrue(financialTransactionWithDrawContexts.Count > 0);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionWithDrawContextByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = financialTransactionWithDrawContextDAO.GetFinancialTransactionWithDrawContextById(validId);

                Assert.IsNotNull(financialTransactionWithDrawContext);
                Assert.AreEqual(validId, financialTransactionWithDrawContext.Id);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionWithDrawContextByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = financialTransactionWithDrawContextDAO.GetFinancialTransactionWithDrawContextById(invalidId);

                Assert.IsNull(financialTransactionWithDrawContext);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionWithDrawContextByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Pago de Proveedor";
                FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = financialTransactionWithDrawContextDAO.GetFinancialTransactionWithDrawContextByName(validName);

                Assert.IsNotNull(financialTransactionWithDrawContext);
                Assert.AreEqual(validName, financialTransactionWithDrawContext.Context);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionWithDrawContextByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Pago de Pedido";
                FinancialTransactionWithDrawContextSet financialTransactionWithDrawContext = financialTransactionWithDrawContextDAO.GetFinancialTransactionWithDrawContextByName(invalidName);

                Assert.IsNull(financialTransactionWithDrawContext);
            }
        }
    }
}