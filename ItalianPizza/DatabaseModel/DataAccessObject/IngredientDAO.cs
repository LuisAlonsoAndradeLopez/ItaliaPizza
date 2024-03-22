using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;
using System;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class IngredientDAO
    {
        public IngredientDAO() { }

        public int AddIngredient(Insumo ingredient)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.InsumoSet.Add(ingredient);
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
