using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductTypeDAOTest
    {
        private static ProductTypeDAO productTypeDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            productTypeDAO = new ProductTypeDAO();
        }

        [TestMethod]
        public void GetAllProductTypesTest()
        {            
            List<ProductTypeSet> productTypes = productTypeDAO.GetAllProductTypes();

            Assert.IsNotNull(productTypes);
            Assert.IsTrue(productTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllProductTypesTestFail()
        {          
            Assert.ThrowsException<Exception>(() => productTypeDAO.GetAllProductTypes());
        }

        [TestMethod]
        public void GetProductTypeByIdTest()
        {
            int validId = 1;
            ProductTypeSet productType = productTypeDAO.GetProductTypeById(validId);

            Assert.IsNotNull(productType);
            Assert.AreEqual(validId, productType.Id);
        }

        [TestMethod]
        public void GetProductTypeByIdTestFail()
        {            
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => productTypeDAO.GetProductTypeById(invalidId));
        }

        [TestMethod]
        public void GetProductTypeByNameTest()
        {
            string validName = "Pizza";
            ProductTypeSet productType = productTypeDAO.GetProductTypeByName(validName);

            Assert.IsNotNull(productType);
            Assert.AreEqual(validName, productType.Type);
        }

        [TestMethod]
        public void GetProductTypeByNameTestTestFail()
        {            
            string invalidName = "Harina";
            ProductTypeSet productType = productTypeDAO.GetProductTypeByName(invalidName);

            Assert.IsNull(productType);
        }
    }
}