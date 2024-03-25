using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class IngredientDAO
    {
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

        public List<Insumo> GetSpecifiedIngredientsByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<Insumo> specifiedIngredients = new List<Insumo>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Nombre")
                {
                    specifiedIngredients = context.InsumoSet.Where(p => p.Nombre.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    //specifiedIngredients = context.InsumoSet.Where(p => p.Código.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedIngredients;
        }
    }
}
