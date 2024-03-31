using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class SupplyTypeDAO
    {
        public List<SupplyTypeSet> GetAllSupplyTypes()
        {
            List<SupplyTypeSet> supplyTypes = new List<SupplyTypeSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyTypes = context.SupplyTypeSet.ToList();
            }

            return supplyTypes;
        }

        public SupplyTypeSet GetSupplyTypeById(int supplyTypeId)
        {
            SupplyTypeSet supplyType = new SupplyTypeSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyType = context.SupplyTypeSet.Where(st => st.Id == supplyTypeId).FirstOrDefault();
            }

            return supplyType;
        }

        public SupplyTypeSet GetSupplyTypeByName(string supplyTypeName)
        {
            SupplyTypeSet supplyType = new SupplyTypeSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                supplyType = context.SupplyTypeSet.Where(st => st.Type == supplyTypeName).FirstOrDefault();
            }

            return supplyType;
        }
    }
}
