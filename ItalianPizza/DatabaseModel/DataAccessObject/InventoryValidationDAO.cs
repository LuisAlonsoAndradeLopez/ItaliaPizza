using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;
using System;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class InventoryValidationDAO
    {
        public int AddInventoryValidation(InventoryValidationSet inventoryValidation)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.InventoryValidationSet.Add(inventoryValidation);
                    context.SaveChanges();
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }
    }
}
