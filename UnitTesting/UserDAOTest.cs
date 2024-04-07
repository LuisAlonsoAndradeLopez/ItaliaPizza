using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System;
using System.Collections.Generic;

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
                Names = "Test",
                LastName = "Test",
                SecondLastName = "Test",
                Email = "test@gmail.com",
                Phone = "1234567890",
                UserStatusId = validValue,
                EmployeePositionId = validValue,
                Address_Id = validValue,

            };
            employeeAlreadyRegistered = new EmployeeSet()
            {
            };
            employeeToGet = new EmployeeSet()
            {
            };
            accountToRegist = new UserAccountSet()
            {
                UserName = "Test",
                Password = "Test",
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
            using (var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = userDAO.RegisterUser(accountToRegist, employeeToRegist);
                Assert.AreEqual(2, result);
            }
        }

        /* Falta hacer bien esta prueba
        [TestMethod]
        public void RegisterUserTest_InvalidByUserAlreadyRegistered()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = userDAO.RegisterUser(accountAlreadyRegistered, employeeAlreadyRegistered);
                Assert.AreEqual(0, result);
            }
        }
        */

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<EmployeeSet> employees = userDAO.GetAllEmployees();
                Assert.IsTrue(employees.Count > 0);
            }
        }

        [TestMethod]
        public void GetUserAddressTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                String address = userDAO.GetUserAddressByEmployeeID(1);
                Assert.IsNotNull(address);
            }
        }

    }
}
