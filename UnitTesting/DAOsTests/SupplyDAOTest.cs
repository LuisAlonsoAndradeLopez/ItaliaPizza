using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
{
    [TestClass]
    public class SupplyDAOTests
    {
        private static SupplyDAO supplyDAO;
        private static SupplySet successSupplySet1;
        private static SupplySet successSupplySet2;
        private static SupplySet successSupplySet3;
        private static SupplySet failedSupplySet;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            supplyDAO = new SupplyDAO();
            successSupplySet1 = new SupplySet
            {
                Id = 10001,
                Name = "Success Supply 1",
                Quantity = 1,
                PricePerUnit = 1,
                Picture = new byte[1],
                SupplyUnitId = 1,
                ProductStatusId = 1,
                SupplyTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "99999999"
            };

            successSupplySet2 = new SupplySet
            {
                Id = 10002,
                Name = "Success Supply 2",
                Quantity = 2,
                PricePerUnit = 2,
                Picture = new byte[2],
                SupplyUnitId = 1,
                ProductStatusId = 1,
                SupplyTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "9999999"
            };

            successSupplySet3 = new SupplySet
            {
                Id = 10003,
                Name = "Success Supply 2",
                Quantity = 3,
                PricePerUnit = 3,
                Picture = new byte[3],
                SupplyUnitId = 1,
                ProductStatusId = 1,
                SupplyTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "99999"
            };

            failedSupplySet = new SupplySet
            {
                Id = 10004,
                Name = "Failed Supply",
                Quantity = 4,
                PricePerUnit = 4,
                Picture = new byte[4],
                ProductStatusId = 1,
                SupplyTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "9999"
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
        public void AddSupplyTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = supplyDAO.AddSupply(successSupplySet1);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void DisableSupplyTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string supplyName = "Success Supply 1";
                int result = supplyDAO.DisableSupply(supplyName);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void GetSupplyByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string supplyName = "Success Supply 1";
                SupplySet supply = supplyDAO.GetSupplyByName(supplyName);

                Assert.IsNotNull(supply);
            }
        }

        [TestMethod]
        public void GetSupplyByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidSupplyName = "Failed Supply";
                SupplySet supply = supplyDAO.GetSupplyByName(invalidSupplyName);

                Assert.IsNull(supply);
            }
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string textForFindingArticle = "Success Supply 1";
                string findByType = "Nombre";
                List<SupplySet> specifiedSupplies = supplyDAO.GetSpecifiedSuppliesByNameOrCode(textForFindingArticle, findByType);

                Assert.IsNotNull(specifiedSupplies);
            }
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidTextForFindingArticle = "Failed Supply";
                string findByType = "Texto Inválido";
                List<SupplySet> specifiedSupplies = supplyDAO.GetSpecifiedSuppliesByNameOrCode(invalidTextForFindingArticle, findByType);

                Assert.IsNull(supplyDAO.GetSpecifiedSuppliesByNameOrCode(invalidTextForFindingArticle, findByType));
            }
        }

        [TestMethod]
        public void ModifySupplyTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = supplyDAO.ModifySupply(successSupplySet2, successSupplySet3);

                Assert.IsTrue(result > 0);
            }
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string supplyCode = "99999999";
                bool result = supplyDAO.TheCodeIsAlreadyRegistred(supplyCode);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidSupplyCode = "9999";
                bool result = supplyDAO.TheCodeIsAlreadyRegistred(invalidSupplyCode);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string supplyName = "Success Supply 1";
                bool result = supplyDAO.TheNameIsAlreadyRegistred(supplyName);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidSupplyName = "Failed Supply";
                bool result = supplyDAO.TheNameIsAlreadyRegistred(invalidSupplyName);

                Assert.IsFalse(result);
            }
        }
    }
}
