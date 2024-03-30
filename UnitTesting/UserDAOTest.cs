using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Transactions;

namespace UnitTesting
{

    [TestClass]
    public class UserDAOTest
    {
        private static UserDAO userDAO;
        private static EmployeeSet employeeToRegist;
        private static EmployeeSet employeeAlreadyRegistered;
        private static EmployeeSet employeeToGet;
        private static UserAccountSet accountToRegist;
        private static UserAccountSet accountAlreadyRegistered;
        private static UserAccountSet accountToGet;
        private static int validValue = 1;
        private static int invalidValue = -1;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            userDAO = new UserDAO();
            employeeToRegist = new EmployeeSet()
            {
            };
            employeeAlreadyRegistered = new EmployeeSet()
            {
            };
            employeeToGet = new EmployeeSet()
            {
            };
            accountToRegist = new UserAccountSet()
            {
            };
            accountAlreadyRegistered = new UserAccountSet()
            {
            };
            accountToGet = new UserAccountSet()
            {
            };
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using(var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }
    }
}
