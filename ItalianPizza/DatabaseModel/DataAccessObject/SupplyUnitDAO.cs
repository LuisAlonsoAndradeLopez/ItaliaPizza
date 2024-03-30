using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class SupplyUnitDAO
    {
        public List<SupplyUnitSet> GetAllSupplyUnits()
        {
            List<SupplyUnitSet> supplyUnits = new List<SupplyUnitSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyUnits = context.SupplyUnitSet.ToList();
            }

            return supplyUnits;
        }

        public SupplyUnitSet GetSupplyUnitById(int supplyUnitId)
        {
            SupplyUnitSet supplyUnit = new SupplyUnitSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyUnit = context.SupplyUnitSet.Where(su => su.Id == supplyUnitId).FirstOrDefault();
            }

            return supplyUnit;
        }

        public SupplyUnitSet GetSupplyUnitByName(string supplyUnitName)
        {
            SupplyUnitSet supplyUnit = new SupplyUnitSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyUnit = context.SupplyUnitSet.Where(su => su.Unit == supplyUnitName).FirstOrDefault();
            }

            return supplyUnit;
        }
    }
}
