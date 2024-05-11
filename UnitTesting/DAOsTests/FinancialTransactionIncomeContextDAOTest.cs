using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class FinancialTransactionIncomeContextDAOTest
    {
        private static FinancialTransactionIncomeContextDAO financialTransactionIncomeContextDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            financialTransactionIncomeContextDAO = new FinancialTransactionIncomeContextDAO();
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
        public void GetAllFinancialTransactionIncomeContextsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<FinancialTransactionIncomeContextSet> financialTransactionIncomeContexts = financialTransactionIncomeContextDAO.GetAllFinancialTransactionIncomeContexts();

                Assert.IsNotNull(financialTransactionIncomeContexts);
                Assert.IsTrue(financialTransactionIncomeContexts.Count > 0);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionIncomeContextByIdTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int validId = 1;
                FinancialTransactionIncomeContextSet financialTransactionIncomeContext = financialTransactionIncomeContextDAO.GetFinancialTransactionIncomeContextById(validId);

                Assert.IsNotNull(financialTransactionIncomeContext);
                Assert.AreEqual(validId, financialTransactionIncomeContext.Id);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionIncomeContextByIdTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int invalidId = -1;
                FinancialTransactionIncomeContextSet financialTransactionIncomeContext = financialTransactionIncomeContextDAO.GetFinancialTransactionIncomeContextById(invalidId);

                Assert.IsNull(financialTransactionIncomeContext);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionIncomeContextByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string validName = "Pago de Pedido";
                FinancialTransactionIncomeContextSet financialTransactionIncomeContext = financialTransactionIncomeContextDAO.GetFinancialTransactionIncomeContextByName(validName);

                Assert.IsNotNull(financialTransactionIncomeContext);
                Assert.AreEqual(validName, financialTransactionIncomeContext.Context);
            }
        }

        [TestMethod]
        public void GetFinancialTransactionIncomeContextByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidName = "Pago de Proveedor";
                FinancialTransactionIncomeContextSet financialTransactionIncomeContext = financialTransactionIncomeContextDAO.GetFinancialTransactionIncomeContextByName(invalidName);

                Assert.IsNull(financialTransactionIncomeContext);
            }
        }
    }
}