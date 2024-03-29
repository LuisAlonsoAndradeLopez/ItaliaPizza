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
    }
}
