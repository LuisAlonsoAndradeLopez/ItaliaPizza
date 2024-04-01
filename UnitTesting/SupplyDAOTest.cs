using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyDAOTests
    {
        [TestMethod]
        public void AddSupplyTest()
        {
            var dao = new SupplyDAO();
            var supply = new SupplySet();
            var result = dao.AddSupply(supply);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddSupplyTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupply = new SupplySet();

            Assert.ThrowsException<EntityException>(() => dao.AddSupply(invalidSupply));
        }

        [TestMethod]
        public void DisableSupplyTest()
        {
            var dao = new SupplyDAO();
            var supplyName = "ValidSupplyName";
            var result = dao.DisableSupply(supplyName);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DisableSupplyTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupplyName = "InvalidSupplyName";
            Assert.ThrowsException<EntityException>(() => dao.DisableSupply(invalidSupplyName));
        }

        [TestMethod]
        public void GetImageBySupplyNameTest()
        {
            var dao = new SupplyDAO();
            var supplyName = "ValidSupplyName"; 
            var bitmapImage = dao.GetImageBySupplyName(supplyName);
            Assert.IsNotNull(bitmapImage);
        }

        [TestMethod]
        public void GetImageBySupplyNameTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupplyName = "InvalidSupplyName"; 
            Assert.ThrowsException<ArgumentNullException>(() => dao.GetImageBySupplyName(invalidSupplyName));
        }

        [TestMethod]
        public void GetSupplyByNameTest()
        {
            var dao = new SupplyDAO();
            var supplyName = "ValidSupplyName"; 
            var supply = dao.GetSupplyByName(supplyName);
            Assert.IsNotNull(supply);
        }

        [TestMethod]
        public void GetSupplyByNameTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupplyName = "InvalidSupplyName";
            Assert.ThrowsException<EntityException>(() => dao.GetSupplyByName(invalidSupplyName));
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTest()
        {
            var dao = new SupplyDAO();
            var textForFindingArticle = "ValidText"; 
            var findByType = "Nombre"; 
            var specifiedSupplies = dao.GetSpecifiedSuppliesByNameOrCode(textForFindingArticle, findByType);
            Assert.IsNotNull(specifiedSupplies);
        }

        [TestMethod]
        public void GetSpecifiedSuppliesByNameOrCodeTestFail()
        {
            var dao = new SupplyDAO();
            var invalidTextForFindingArticle = "InvalidText"; 
            var findByType = "InvalidType"; 
            Assert.ThrowsException<ArgumentNullException>(() => dao.GetSpecifiedSuppliesByNameOrCode(invalidTextForFindingArticle, findByType));
        }

        [TestMethod]
        public void ModifySupplyTest()
        {
            var dao = new SupplyDAO();
            var originalSupply = new SupplySet(); 
            var modifiedSupply = new SupplySet(); 
            var result = dao.ModifySupply(originalSupply, modifiedSupply);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ModifySupplyTestFail()
        {
            var dao = new SupplyDAO();
            var originalSupply = new SupplySet(); 
            var invalidModifiedSupply = new SupplySet(); 
            Assert.ThrowsException<EntityException>(() => dao.ModifySupply(originalSupply, invalidModifiedSupply));
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {
            var dao = new SupplyDAO();
            var supplyCode = "ValidSupplyCode"; 
            var result = dao.TheCodeIsAlreadyRegistred(supplyCode);
            Assert.IsFalse(result); 
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupplyCode = "InvalidSupplyCode"; 
            Assert.ThrowsException<ArgumentNullException>(() => dao.TheCodeIsAlreadyRegistred(invalidSupplyCode));
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {
            var dao = new SupplyDAO();
            var supplyName = "ValidSupplyName"; 
            var result = dao.TheNameIsAlreadyRegistred(supplyName);
            Assert.IsFalse(result); 
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {
            var dao = new SupplyDAO();
            var invalidSupplyName = "InvalidSupplyName"; 
            Assert.ThrowsException<ArgumentNullException>(() => dao.TheNameIsAlreadyRegistred(invalidSupplyName));
        }
    }
}
