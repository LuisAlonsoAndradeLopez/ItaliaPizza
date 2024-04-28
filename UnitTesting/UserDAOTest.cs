using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;

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
                Names = "Alvaro",
                LastName = "Vazquez",
                SecondLastName = "Aguirre",
                Email = "alvaro@gmail.com",
                Phone = "1234567890",
                UserStatusId = validValue,
                EmployeePositionId = validValue,
                Address_Id = validValue,
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
                UserName = "alvaro@gmail.com",
                Password = "dadada"
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


        [TestMethod]
        public void RegisterUserTest_InvalidByUserAlreadyRegistered()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                bool result = userDAO.CheckUserExistence(accountAlreadyRegistered);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void RegisterEmployeeTest_InvalidByEmployeeAlreadyRegistered()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                bool result = userDAO.CheckEmployeeExistence(employeeAlreadyRegistered);
                Assert.AreEqual(true, result);
            }
        }

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

        [TestMethod]
        public void GetAllEmployeeByStatusTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<EmployeeSet> employees = userDAO.GetAllEmployeesByStatus("Activo");
                Assert.AreEqual(3, employees.Count);
            }
        }

        [TestMethod]
        public void ModifyEmployeeTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                employeeAlreadyRegistered.UserStatusId = 2;
                int result = userDAO.ModifyEmployee(accountAlreadyRegistered, employeeAlreadyRegistered);
                Assert.AreEqual(1, result);
            }
        }

    }
}
