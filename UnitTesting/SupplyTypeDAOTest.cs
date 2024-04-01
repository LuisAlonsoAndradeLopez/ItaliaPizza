namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyTypeDAOTest
    {
        [TestMethod]
        public void GetAllSupplyTypesTest()
        {
            var dao = new SupplyTypeDAO();
            var supplyTypes = dao.GetAllSupplyTypes();

            Assert.IsNotNull(supplyTypes);
            Assert.IsTrue(supplyTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllSupplyTypesTestFail()
        {
            var dao = new SupplyTypeDAO();

            Assert.ThrowsException<Exception>(() => dao.GetAllSupplyTypes());
        }

        [TestMethod]
        public void GetSupplyTypeByIdTest()
        {
            var dao = new SupplyTypeDAO();
            int validId = 1;
            var supplyType = dao.GetSupplyTypeById(validId);

            Assert.IsNotNull(supplyType);
            Assert.AreEqual(validId, supplyType.Id);
        }

        [TestMethod]
        public void GetSupplyTypeByIdTestFail()
        {
            var dao = new SupplyTypeDAO();
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetSupplyTypeById(invalidId));
        }

        [TestMethod]
        public void GetSupplyTypeByNameTest()
        {
            var dao = new SupplyTypeDAO();
            string validName = "Harina";
            var supplyType = dao.GetSupplyTypeByName(validName);

            Assert.IsNotNull(supplyType);
            Assert.AreEqual(validName, supplyType.Type);
        }

        [TestMethod]
        public void GetSupplyTypeByNameTestFail()
        {
            var dao = new SupplyTypeDAO();
            string invalidName = "Boja";
            var supplyType = dao.GetSupplyTypeByName(invalidName);

            Assert.IsNull(supplyType);
        }
    }
}