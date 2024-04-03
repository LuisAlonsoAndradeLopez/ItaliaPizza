using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductStatusDAOTest
    {
        private static ProductStatusDAO productStatusDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            productStatusDAO = new ProductStatusDAO();
        }

        [TestMethod]
        public void GetAllProductStatusesTest()
        {
            List<ProductStatusSet> productStatuses = productStatusDAO.GetAllProductStatuses();

            Assert.IsNotNull(productStatuses);
            Assert.IsTrue(productStatuses.Count > 0);
        }

        [TestMethod]
        public void GetAllProductStatusesTestTestFail()
        {
            Assert.ThrowsException<Exception>(() => productStatusDAO.GetAllProductStatuses());
        }

        [TestMethod]
        public void GetProductStatusByIdTest()
        {
            int validId = 1;
            ProductStatusSet productStatus = productStatusDAO.GetProductStatusById(validId);

            Assert.IsNotNull(productStatus);
            Assert.AreEqual(validId, productStatus.Id);
        }

        [TestMethod]
        public void GetProductStatusByIdTestFail()
        {            
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => productStatusDAO.GetProductStatusById(invalidId));
        }

        [TestMethod]
        public void GetProductStatusByNameTest()
        {            
            string validName = "Activo";
            ProductStatusSet productStatus = productStatusDAO.GetProductStatusByName(validName);

            Assert.IsNotNull(productStatus);
            Assert.AreEqual(validName, productStatus.Status);
        }

        [TestMethod]
        public void GetProductStatusByNameTestFail()
        {            
            string invalidName = "Proactivo";
            ProductStatusSet productStatus = productStatusDAO.GetProductStatusByName(invalidName);

            Assert.IsNull(productStatus);
        }
    }
}