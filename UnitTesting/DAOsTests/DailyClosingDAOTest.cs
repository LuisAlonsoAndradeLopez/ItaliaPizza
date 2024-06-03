using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitTesting.DAOsTests
{


    [TestClass]
    public class DailyClosingDAOTest
    {
        private static DailyClosingDAO dailyClosingDAO;
        private static int validValue = 1;
        private static int invalidValue = -1;
        private static DailyClosingSet dailyClosingToRegister;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            dailyClosingDAO = new DailyClosingDAO();

            dailyClosingToRegister = new DailyClosingSet()
            {
                ClosingDate = DateTime.Now,
                InitialBalance = 1000,
                Description = "Todo correcto con este balance diario",
                EmployeeId = 2,
                BalanceIncome = 1000,
                BalanceWithdrawal = 500,
                FinalBalance = 1500
            };
                
        }

        [TestMethod]
        public void RegisterDailyClosingTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = dailyClosingDAO.RegisterDailyClosing(dailyClosingToRegister);
                Assert.IsTrue(result > 0);
            }
        }

        [TestMethod]

        public void GetMaximumDailyClosingIDTest()
        {
            int result = dailyClosingDAO.GetMaximumDailyClosingID();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetDailyIncomesTest()
        {
            List<IncomeFinancialTransactionSet> result = dailyClosingDAO.GetDailyIncomes();
            Assert.AreEqual(2, result.Count);
        }

        
    }


}
