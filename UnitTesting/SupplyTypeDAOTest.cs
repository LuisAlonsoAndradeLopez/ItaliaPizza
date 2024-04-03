using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyTypeDAOTest
    {
        private static SupplyTypeDAO supplyTypeDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            supplyTypeDAO = new SupplyTypeDAO();
        }

        [TestMethod]
        public void GetAllSupplyTypesTest()
        {
            List<SupplyTypeSet> supplyTypes = supplyTypeDAO.GetAllSupplyTypes();

            Assert.IsNotNull(supplyTypes);
            Assert.IsTrue(supplyTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllSupplyTypesTestFail()
        {
            Assert.ThrowsException<Exception>(() => supplyTypeDAO.GetAllSupplyTypes());
        }

        [TestMethod]
        public void GetSupplyTypeByIdTest()
        {   
            int validId = 1;
            SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeById(validId);

            Assert.IsNotNull(supplyType);
            Assert.AreEqual(validId, supplyType.Id);
        }

        [TestMethod]
        public void GetSupplyTypeByIdTestFail()
        {   
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => supplyTypeDAO.GetSupplyTypeById(invalidId));
        }

        [TestMethod]
        public void GetSupplyTypeByNameTest()
        {   
            string validName = "Harina";
            SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeByName(validName);

            Assert.IsNotNull(supplyType);
            Assert.AreEqual(validName, supplyType.Type);
        }

        [TestMethod]
        public void GetSupplyTypeByNameTestFail()
        {
            string invalidName = "Boja";
            SupplyTypeSet supplyType = supplyTypeDAO.GetSupplyTypeByName(invalidName);

            Assert.IsNull(supplyType);
        }
    }
}