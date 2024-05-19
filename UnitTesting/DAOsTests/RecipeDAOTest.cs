using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitTesting.DAOsTests
{
    [TestClass]
    public class RecipeDAOTest
    {
        private static RecipeDAO recipeDAO;
        private static RecipeSet recipeToRegist;
        private static RecipeSet recipeAlreadyRegistered;
        private static RecipeSet modifiedRecipe;
        private static List<RecipeDetailsSet> recipeDetailsToRegist;
        private static List<RecipeDetailsSet> modifiedRecipeDetails;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            recipeDAO = new RecipeDAO();
            recipeToRegist = new RecipeSet()
            {
                Name = "Hamburguesa Clasica",
                Instructions = "Cocinar 50 mil",
                Status = "1",
                EmployeeId = 3,
                ProductSale_Id = 28
            };

            recipeAlreadyRegistered = new RecipeSet()
            {
                Name = "Pizza Pepperoni",
                Instructions = "Cocinar 50 mil",
                Status = "1",
                EmployeeId = 3,
                ProductSale_Id = 24
            };

            modifiedRecipe = new RecipeSet()
            {
                Name = "Pizza Pepperoni",
                Instructions = "Cocinar 80 mil",
                Status = "1",
                EmployeeId = 4,
                ProductSale_Id = 24
            };

            recipeDetailsToRegist = new List<RecipeDetailsSet>
            {
                new RecipeDetailsSet()
                {
                    Quantity = 250,
                    SupplyId = 26
                },
                new RecipeDetailsSet()
                {
                    Quantity = 500,
                    SupplyId = 27
                },
                new RecipeDetailsSet()
                {
                    Quantity = 100,
                    SupplyId = 28
                },
                new RecipeDetailsSet()
                {
                    Quantity = 50,
                    SupplyId = 29
                }
            };

        }

        [TestMethod]
        public void AddRecipeTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = recipeDAO.AddRecipe(recipeToRegist, recipeDetailsToRegist);
                Assert.AreEqual(5, result);
            }
        }

        [TestMethod]
        public void AddRecipeTest_InvalidByRecipeAlreadyRegistered()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = recipeDAO.AddRecipe(recipeAlreadyRegistered, recipeDetailsToRegist);
                Assert.AreEqual(0, result);
            }
        }

        [TestMethod]
        public void ModifyRecipeTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = recipeDAO.ModifyRecipe(modifiedRecipe, recipeDetailsToRegist);
                Assert.AreEqual(9, result);
            }
        }
    }
}
