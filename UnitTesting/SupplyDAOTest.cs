using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
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

            supplyDAO.AddSupply(successSupplySet2);
            supplyDAO.AddSupply(successSupplySet3);
            supplyDAO.AddSupply(failedSupplySet);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            supplyDAO.DeleteSupply(successSupplySet1);
            supplyDAO.DeleteSupply(successSupplySet2);
            supplyDAO.DeleteSupply(successSupplySet3);
            supplyDAO.DeleteSupply(failedSupplySet);
        }

        [TestMethod]
        public void AddSupplyTest()
        {
            int result = supplyDAO.AddSupply(successSupplySet1);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddSupplyTestFail()
        {
            Assert.ThrowsException<EntityException>(() => supplyDAO.AddSupply(failedSupplySet));
        }

        [TestMethod]
        public void DisableSupplyTest()
        {            
            string supplyName = "Success Supply 1";
            int result = supplyDAO.DisableSupply(supplyName);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DisableSupplyTestFail()
        {
            string invalidSupplyName = "Failed Supply";
            Assert.ThrowsException<EntityException>(() => supplyDAO.DisableSupply(invalidSupplyName));
        }

        [TestMethod]
        public void GetImageBySupplyNameTest()
        {   
            string supplyName = "Success Supply 1";
            //var bitmapImage = supplyDAO.GetImageBySupplyName(supplyName);
            //Assert.IsNotNull(bitmapImage);
        }

        [TestMethod]
        public void GetImageBySupplyNameTestFail()
        {   
            string invalidSupplyName = "Failed Supply";
            //Assert.ThrowsException<ArgumentNullException>(() => supplyDAO.GetImageBySupplyName(invalidSupplyName));
        }

        [TestMethod]
        public void GetSupplyByNameTest()
        {
            string supplyName = "Success Supply 1"; 
            SupplySet supply = supplyDAO.GetSupplyByName(supplyName);
            Assert.IsNotNull(supply);
        }

        [TestMethod]
        public void GetSupplyByNameTestFail()
        {
            string invalidSupplyName = "Failed Supply";
            Assert.ThrowsException<EntityException>(() => supplyDAO.GetSupplyByName(invalidSupplyName));
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTest()
        {
            string textForFindingArticle = "Success Supply 1"; 
            string findByType = "Nombre"; 
            List<SupplySet> specifiedSupplies = supplyDAO.GetSpecifiedSuppliesByNameOrCode(textForFindingArticle, findByType);
            Assert.IsNotNull(specifiedSupplies);
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTestFail()
        {
            string invalidTextForFindingArticle = "Failed Supply"; 
            string findByType = "Texto Inválido"; 
            Assert.ThrowsException<ArgumentNullException>(() => supplyDAO.GetSpecifiedSuppliesByNameOrCode(invalidTextForFindingArticle, findByType));
        }

        [TestMethod]
        public void ModifySupplyTest()
        {            
            int result = supplyDAO.ModifySupply(successSupplySet2, successSupplySet3);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ModifySupplyTestFail()
        {
            Assert.ThrowsException<EntityException>(() => supplyDAO.ModifySupply(successSupplySet3, failedSupplySet));
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {
            string supplyCode = "99999999"; 
            bool result = supplyDAO.TheCodeIsAlreadyRegistred(supplyCode);
            Assert.IsFalse(result); 
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {
            string invalidSupplyCode = "9999"; 
            Assert.ThrowsException<ArgumentNullException>(() => supplyDAO.TheCodeIsAlreadyRegistred(invalidSupplyCode));
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {
            string supplyName = "Success Supply 1"; 
            bool result = supplyDAO.TheNameIsAlreadyRegistred(supplyName);
            Assert.IsFalse(result); 
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {
            string invalidSupplyName = "Failed Supply"; 
            Assert.ThrowsException<ArgumentNullException>(() => supplyDAO.TheNameIsAlreadyRegistred(invalidSupplyName));
        }
    }
}
