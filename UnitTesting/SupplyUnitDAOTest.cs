using ItalianPizza.DatabaseModel.DatabaseMapping;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyUnitDAOTest
    {
        private static SupplyUnitDAO supplyUnitDAO;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            supplyUnitDAO = new SupplyUnitDAO();
        }

        [TestMethod]
        public void GetAllSupplyUnitsTest()
        {
            List<SupplyUnitSet> supplyUnits = supplyUnitDAO.GetAllSupplyUnits();

            Assert.IsNotNull(supplyUnits);
            Assert.IsTrue(supplyUnits.Count > 0);
        }

        [TestMethod]
        public void GetAllSupplyUnitsTestFail()
        {  
            Assert.ThrowsException<Exception>(() => supplyUnitDAO.GetAllSupplyUnits());
        }

        [TestMethod]
        public void GetSupplyUnitByIdTest()
        { 
            int validId = 1;
            SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitById(validId);

            Assert.IsNotNull(supplyUnit);
            Assert.AreEqual(validId, supplyUnit.Id);
        }

        [TestMethod]
        public void GetSupplyUnitByIdTestFail()
        {   
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => supplyUnitDAO.GetSupplyUnitById(invalidId));
        }

        [TestMethod]
        public void GetSupplyUnitByNameTest()
        {   
            string validName = "Gramo";
            SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitByName(validName);

            Assert.IsNotNull(supplyUnit);
            Assert.AreEqual(validName, supplyUnit.Unit);
        }

        [TestMethod]
        public void GetSupplyUnitByNameTestFail()
        {
            string invalidName = "Gayeta";
            SupplyUnitSet supplyUnit = supplyUnitDAO.GetSupplyUnitByName(invalidName);

            Assert.IsNull(supplyUnit);
        }
    }
}