namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductTypeDAOTest
    {
        [TestMethod]
        public void GetAllProductTypesTest()
        {
            var dao = new ProductTypeDAO();
            var productTypes = dao.GetAllProductTypes();

            Assert.IsNotNull(productTypes);
            Assert.IsTrue(productTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllProductTypesTestFail()
        {
            var dao = new ProductTypeDAO();

            Assert.ThrowsException<Exception>(() => dao.GetAllProductTypes());
        }

        [TestMethod]
        public void GetProductTypeByIdTest()
        {
            var dao = new ProductTypeDAO();
            int validId = 1;
            var productType = dao.GetProductTypeById(validId);

            Assert.IsNotNull(productType);
            Assert.AreEqual(validId, productType.Id);
        }

        [TestMethod]
        public void GetProductTypeByIdTestFail()
        {
            var dao = new ProductTypeDAO();
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetProductTypeById(invalidId));
        }

        [TestMethod]
        public void GetProductTypeByNameTest()
        {
            var dao = new ProductTypeDAO();
            string validName = "Pizza";
            var productType = dao.GetProductTypeByName(validName);

            Assert.IsNotNull(productType);
            Assert.AreEqual(validName, productType.Type);
        }

        [TestMethod]
        public void GetProductTypeByNameTestTestFail()
        {
            var dao = new ProductTypeDAO();
            string invalidName = "Harina";
            var productType = dao.GetProductTypeByName(invalidName);

            Assert.IsNull(productType);
        }
    }
}