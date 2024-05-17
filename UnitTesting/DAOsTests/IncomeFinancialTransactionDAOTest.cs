using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class IncomeFinancialTransactionDAOTest
    {
        private static IncomeFinancialTransactionDAO incomeFinancialTransactionDAO;
        private static IncomeFinancialTransactionSet successIncomeFinancialTransaction;
        private static EmployeeSet employee;
        private static UserAccountSet userAccount;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            incomeFinancialTransactionDAO = new IncomeFinancialTransactionDAO();
            successIncomeFinancialTransaction = new IncomeFinancialTransactionSet
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
        public void AddIncomeFinancialTransactionTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = incomeFinancialTransactionDAO.AddIncomeFinancialTransaction(successIncomeFinancialTransaction);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void GetIncomeFinancialTransactionsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<IncomeFinancialTransactionSet> incomeFinancialTransactions = incomeFinancialTransactionDAO.GetIncomeFinancialTransactions();

                Assert.IsNotNull(incomeFinancialTransactions);
                Assert.IsTrue(incomeFinancialTransactions.Count > 0);
            }
        }
    }
}