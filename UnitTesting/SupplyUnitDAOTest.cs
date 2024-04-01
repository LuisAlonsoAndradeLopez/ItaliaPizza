namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class SupplyUnitDAOTest
    {
        [TestMethod]
        public void GetAllSupplyUnitsTest()
        {
            var dao = new SupplyUnitDAO();
            var supplyUnits = dao.GetAllSupplyUnits();

            Assert.IsNotNull(supplyUnits);
            Assert.IsTrue(supplyUnits.Count > 0);
        }

        [TestMethod]
        public void GetAllSupplyUnitsTestFail()
        {
            var dao = new SupplyUnitDAO();

            Assert.ThrowsException<Exception>(() => dao.GetAllSupplyUnits());
        }

        [TestMethod]
        public void GetSupplyUnitByIdTest()
        {
            var dao = new SupplyUnitDAO();
            int validId = 1;
            var supplyUnit = dao.GetSupplyUnitById(validId);

            Assert.IsNotNull(supplyUnit);
            Assert.AreEqual(validId, supplyUnit.Id);
        }

        [TestMethod]
        public void GetSupplyUnitByIdTestFail()
        {
            var dao = new SupplyUnitDAO();
            int invalidId = -1;

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetSupplyUnitById(invalidId));
        }

        [TestMethod]
        public void GetSupplyUnitByNameTest()
        {
            var dao = new SupplyUnitDAO();
            string validName = "Gramo";
            var supplyUnit = dao.GetSupplyUnitByName(validName);

            Assert.IsNotNull(supplyUnit);
            Assert.AreEqual(validName, supplyUnit.Unit);
        }

        [TestMethod]
        public void GetSupplyUnitByNameTestFail()
        {
            var dao = new SupplyUnitDAO();
            string invalidName = "Gayeta";
            var supplyUnit = dao.GetSupplyUnitByName(invalidName);

            Assert.IsNull(supplyUnit);
        }
    }
}