using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class DailyClosingDAO
    {
        public int GetMaximumDailyClosingID()
        {
            int maximumDailyClosingID = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    maximumDailyClosingID = context.DailyClosingSet.Max(dailyClosing => (int?)dailyClosing.Id) ?? 0;
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return maximumDailyClosingID;
        }
    }
}
