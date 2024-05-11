using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class FinancialTransactionDAOTest
    {
        private static FinancialTransactionDAO financialTransactionDAO;
        private static FinancialTransactionSet successFinancialTransaction;
        private static EmployeeSet employee;
        private static UserAccountSet userAccount;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            financialTransactionDAO = new FinancialTransactionDAO();
            successFinancialTransaction = new FinancialTransactionSet
            {
                Id = 10001,
                Type = "Entrada",
                Description = "Nada",
                FinancialTransactionDate = new DateTime(2001, 09, 11),
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
        public void AddFinancialTransactionTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = financialTransactionDAO.AddFinancialTransaction(successFinancialTransaction);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void GetAllActiveFinancialTransactionsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<FinancialTransactionSet> financialTransactions = financialTransactionDAO.GetFinancialTransactions();

                Assert.IsNotNull(financialTransactions);
                Assert.IsTrue(financialTransactions.Count > 0);
            }
        }

        [TestMethod]
        public void GetSpecifiedFinancialTransactionsByTransactionTypeAndRealizationDateTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string transactionType = "Entrada";
                DateTime realizationDate = new DateTime(2001, 09, 11);
                List<FinancialTransactionSet> specifiedFinancialTransactions = financialTransactionDAO.GetSpecifiedFinancialTransactionsByTransactionTypeAndRealizationDate(transactionType, realizationDate);

                Assert.IsNotNull(specifiedFinancialTransactions);
            }
        }

        [TestMethod]
        public void GetSpecifiedFinancialTransactionsByTransactionTypeAndRealizationDateTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidTransactionType = "Media";
                DateTime invalidRealizationDate = DateTime.Now;
                List<FinancialTransactionSet> specifiedFinancialTransactions = financialTransactionDAO.GetSpecifiedFinancialTransactionsByTransactionTypeAndRealizationDate(invalidTransactionType, invalidRealizationDate);

                Assert.IsTrue(specifiedFinancialTransactions.Count == 0);
            }
        }
    }
}