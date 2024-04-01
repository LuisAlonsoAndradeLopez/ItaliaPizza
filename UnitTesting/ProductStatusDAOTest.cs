namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductStatusDAOTest
    {
        [TestMethod]
        public void GetAllProductStatusesTest()
        {
            var dao = new ProductStatusDAO();
            var productStatuses = dao.GetAllProductStatuses();

            Assert.IsNotNull(productStatuses);
            Assert.IsTrue(productStatuses.Count > 0);
        }

        [TestMethod]
        public void GetAllProductStatusesTestTestFail()
        {
            var dao = new ProductStatusDAO();

            Assert.ThrowsException<Exception>(() => dao.GetAllProductStatuses());
        }

        [TestMethod]
        public void GetProductStatusByIdTest()
        {
            var dao = new ProductStatusDAO();
            int validId = 1;
            var productStatus = dao.GetProductStatusById(validId);

            Assert.IsNotNull(productStatus);
            Assert.AreEqual(validId, productStatus.Id);
        }

        [TestMethod]
        public void GetProductStatusByIdTestFail()
        {
            var dao = new ProductStatusDAO();
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetProductStatusById(invalidId));
        }

        [TestMethod]
        public void GetProductStatusByNameTest()
        {
            var dao = new ProductStatusDAO();
            string validName = "Activo";
            var productStatus = dao.GetProductStatusByName(validName);

            Assert.IsNotNull(productStatus);
            Assert.AreEqual(validName, productStatus.Status);
        }

        [TestMethod]
        public void GetProductStatusByNameTestFail()
        {
            var dao = new ProductStatusDAO();
            string invalidName = "Proactivo";
            var productStatus = dao.GetProductStatusByName(invalidName);

            Assert.IsNull(productStatus);
        }
    }
}