using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace UnitTesting
{

    [TestClass]
    public class UserDAOTest
    {
        private static UserDAO userDAO;
        private static Empleado employeeToRegist;
        private static Empleado employeeAlreadyRegistered;
        private static Empleado employeeToGet;
        private static Cuenta accountToRegist;
        private static Cuenta accountAlreadyRegistered;
        private static Cuenta accountToGet;
        private static int validValue = 1;
        private static int invalidValue = -1;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            userDAO = new UserDAO();
            employeeToRegist = new Empleado()
            {
            };
            employeeAlreadyRegistered = new Empleado()
            {
            };
            employeeToGet = new Empleado()
            {
            };
            accountToRegist = new Cuenta()
            {
            };
            accountAlreadyRegistered = new Cuenta()
            {
            };
            accountToGet = new Cuenta()
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
