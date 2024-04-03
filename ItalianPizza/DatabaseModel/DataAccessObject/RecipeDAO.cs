using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class RecipeDAO
    {
        public RecipeDAO() { }

        public List<RecipeDetailsSet> GetRecipeDetailsByProductSale(ProductSaleSet productSale)
        {
            List<RecipeDetailsSet> recipeDetails = null;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    recipeDetails = context.RecipeDetailsSet
                        .Where(recipeDetailsAux => recipeDetailsAux.RecipeSet.ProductSale_Id == productSale.Id)
                        .ToList();
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

            return recipeDetails;
        }
    }
}
