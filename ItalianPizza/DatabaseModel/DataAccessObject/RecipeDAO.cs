using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Security.RightsManagement;

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
                        .Include(s => s.RecipeSet)
                        .Include(s => s.SupplySet.SupplyUnitSet)
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

        public RecipeSet GetRecipeByProduct(string productName)
        {
            RecipeSet recipe = null;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    recipe = context.RecipeSet
                        .Include(s => s.ProductSaleSet)
                        .Where(r => r.ProductSaleSet.Name == productName)
                        .FirstOrDefault();
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
           
            return recipe;
        }

        public int AddRecipe(RecipeSet recipe, List<RecipeDetailsSet> recipedetails)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    RecipeSet recipeToValidate = context.RecipeSet
                        .Where(r => r.Name == recipe.Name)
                        .FirstOrDefault();
                    if (recipeToValidate != null)
                    {
                        return 0;
                    }
                    else
                    {
                        context.RecipeSet.Add(recipe);
                        foreach (RecipeDetailsSet recipeDetail in recipedetails)
                        {
                            recipeDetail.RecipeId = recipe.Id;
                            context.RecipeDetailsSet.Add(recipeDetail);
                        }
                    }
                    result = context.SaveChanges();
                };

            }
            catch (EntityException ex)
            {
                result = -1;
            }
            return result;

        }

        public int ModifyRecipe(RecipeSet recipe, List<RecipeDetailsSet> recipedetails)
        {
            int result = 0;
            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    RecipeSet recipeToModify = context.RecipeSet
                        .Where(r => r.Name == recipe.Name)
                        .FirstOrDefault();
                    if (recipeToModify != null)
                    {
                        recipeToModify.Instructions = recipe.Instructions;
                        recipeToModify.EmployeeId = recipe.EmployeeId;

                        List<RecipeDetailsSet> recipeDetailsToDelete = context.RecipeDetailsSet
                            .Where(rd => rd.RecipeId == recipeToModify.Id)
                            .ToList();
                        if (recipeDetailsToDelete != null)
                        {
                            foreach (RecipeDetailsSet recipeDetail in recipeDetailsToDelete)
                            {
                                context.RecipeDetailsSet.Remove(recipeDetail);
                            }

                            foreach (RecipeDetailsSet recipeDetail in recipedetails)
                            {
                                recipeDetail.RecipeId = recipeToModify.Id;
                                context.RecipeDetailsSet.Add(recipeDetail);
                            }
                        }
                        result = context.SaveChanges();
                    }
                    else
                    {
                        result = -1;
                    }
                }
            }
            catch (EntityException ex)
            {
                result = -1;
            }
            return result;
        }
    }
}
