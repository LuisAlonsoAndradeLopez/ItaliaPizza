using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.XAMLViews.Suppliers;

namespace ItalianPizza.XAMLViews.Recipes
{
    /// <summary>
    /// Lógica de interacción para GUI_RecipeDetails.xaml
    /// </summary>
    public partial class GUI_RecipeDetails : Page
    {
        RecipeSet recipe;
        ProductSaleSet product;
        RecipeDAO recipeDAO = new RecipeDAO();
        public GUI_RecipeDetails(RecipeSet recipe, ProductSaleSet product)
        {
            InitializeComponent();
            this.recipe = recipe;
            this.product = product;
            FillData();
        }

        private void btnGoBackClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void FillData()
        {
            lblProductName.Content = product.Name;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath;
            string imagePath;

            if (new ImageManager().CheckSupplyImagePath(product.Id))
            {
                relativePath = $"..\\TempCache\\Products\\{product.Id}.png";
                imagePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relativePath));

                imgProduct.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }

            FillIngredientsTextbox();
            FillRecipeTextbox();

        }

        private void FillRecipeTextbox()
        {
            String recipeInstructions = recipe.Instructions;
            txtRecipeInstructions.Text = recipeInstructions;
        }

        private void FillIngredientsTextbox()
        {
           List<RecipeDetailsSet> recipeIngredients = recipeDAO.GetRecipeDetailsByProductSale(product);

            foreach (RecipeDetailsSet recipeDetail in recipeIngredients)
            {
                string ingredient = recipeDetail.SupplySet.Name;
                string quantity = recipeDetail.Quantity.ToString();
                string measure = recipeDetail.SupplySet.SupplyUnitSet.Unit;
                txtRecipeIngredients.Text += $". * {quantity} {measure} de {ingredient} \n";

            }
        }

        private void btnModifyRecipeClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GUI_ModifyRecipe(recipe, product));
        }

    }
}
