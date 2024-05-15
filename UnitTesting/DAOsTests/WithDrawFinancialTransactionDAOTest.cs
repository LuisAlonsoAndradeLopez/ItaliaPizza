using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class WithDrawFinancialTransactionDAOTest
    {
        private static WithDrawFinancialTransactionDAO withDrawFinancialTransactionDAO;
        private static WithDrawFinancialTransactionSet successWithDrawFinancialTransaction;
        private static EmployeeSet employee;
        private static UserAccountSet userAccount;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            withDrawFinancialTransactionDAO = new WithDrawFinancialTransactionDAO();
            successWithDrawFinancialTransaction = new WithDrawFinancialTransactionSet
            {
                Id = 10001,
                Description = "Nada",
                RealizationDate = new DateTime(2001, 09, 11),
                EmployeeId = 2
            };
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
        public void AddWithDrawFinancialTransactionTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = withDrawFinancialTransactionDAO.AddWithDrawFinancialTransaction(successWithDrawFinancialTransaction);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void GetAllActiveWithDrawFinancialTransactionsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<WithDrawFinancialTransactionSet> withDrawFinancialTransactions = withDrawFinancialTransactionDAO.GetWithDrawFinancialTransactions();

                Assert.IsNotNull(withDrawFinancialTransactions);
                Assert.IsTrue(withDrawFinancialTransactions.Count > 0);
            }
        }
    }
}