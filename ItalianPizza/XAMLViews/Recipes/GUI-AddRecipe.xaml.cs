using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.Auxiliary;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using System.Windows.Media.Animation;
using ItalianPizza.SingletonClasses;

namespace ItalianPizza.XAMLViews.Suppliers
{
    /// <summary>
    /// Lógica de interacción para GUI_AddRecipe.xaml
    /// </summary>
    public partial class GUI_AddRecipe : Page
    {
        private List<SupplySet> supplies = new List<SupplySet>();
        private SupplyDAO supplyDAO = new SupplyDAO();
        private ProductSaleSet productSaleSet = new ProductSaleSet();
        private List<SupplySet> suppliesSelected = new List<SupplySet>();
        int active = 1;

        public GUI_AddRecipe(ProductSaleSet productSaleSet)
        {
            InitializeComponent();
            ShowActiveSupplies();
            SuppliesSummaryStackPanel.Children.Clear();
            this.productSaleSet = productSaleSet;
        }

        private void ManualQuantityIntegerUpDownPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_ProductSearch(object sender, TextChangedEventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            RecoverProducts(textSearch);
        }
        private void RecoverProducts(string textSearch)
        {
            List<SupplySet> filteredSupplies = supplies
                .Where(p => p.Name.ToLower().Contains(textSearch.ToLower())).ToList();
            AddVisualSuppliesToWindow(filteredSupplies);
        }

        private void ShowActiveSupplies()
        {
            List<SupplySet> activeSupplies;

            try
            {
                activeSupplies = supplyDAO.GetAllSupply();
                supplies = activeSupplies;
                AddVisualSuppliesToWindow(supplies);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                    "la conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                    Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un" +
                    " error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                    Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void AddVisualSuppliesToWindow(List<SupplySet> supplies)
        {
            string addSupplyIcon = Properties.Resources.ICON_AddImage; ;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath;
            string imagePath;

            SuppliesStackPanel.Children.Clear();

            foreach (var supply in supplies)
            {
                StackPanel supplypannel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                };

                Border supplyBorder = new Border
                {
                    Height = 136,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = (Brush)new BrushConverter().ConvertFrom("#7E1616")
                };

                Image supplyImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(26, 0, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                };

                if (new ImageManager().CheckSupplyImagePath(supply.Id))
                {
                    relativePath = $"..\\TempCache\\Supplies\\{supply.Id}.png";
                    imagePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, relativePath));
                    supplyImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                }

                Label supplyname = new Label
                {
                    Margin = new Thickness(26, 0, 0, 0),
                    Content = supply.Name,
                    FontFamily = new FontFamily("Segoe UI Variable Display"),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Foreground = Brushes.White,
                    Height = 36,
                    VerticalAlignment = VerticalAlignment.Center

                };

                StackPanel quantityStackPanel = new StackPanel
                {
                    Margin = new Thickness(40, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,

                };

                Label quantityLabel = new Label
                {
                    Margin = new Thickness(0, 0, 0, 10),
                    Content = "Cantidad" + "( " + supply.SupplyUnitSet.Unit + " )",
                    FontFamily = new FontFamily("Segoe UI Variable Display"),
                    FontWeight = FontWeights.Bold,
                    FontSize = 15,
                    Foreground = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center
                };

                IntegerUpDown quantityIntegerUpDown = new IntegerUpDown
                {
                    Value = 0,
                    Increment = 1,
                    Maximum = 10000,
                    Minimum = 0,
                    MaxLength = 7,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 36,
                    Width = 129,
                    FontSize = 24
                };

                quantityIntegerUpDown.PreviewTextInput += ManualQuantityIntegerUpDownPreviewTextInput;

                Image addImage = new Image
                {
                    Width = 38,
                    Height = 55,
                    Margin = new Thickness(70, 0, 0, 0),
                    Source = new BitmapImage(new Uri(addSupplyIcon, UriKind.RelativeOrAbsolute)),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                addImage.MouseLeftButtonDown += AddImage_MouseLeftButtonDown;


                quantityStackPanel.Children.Add(quantityLabel);
                quantityStackPanel.Children.Add(quantityIntegerUpDown);

                supplypannel.Children.Add(supplyImage);
                supplypannel.Children.Add(supplyname);
                supplypannel.Children.Add(quantityStackPanel);
                supplypannel.Children.Add(addImage);

                supplyBorder.Child = supplypannel;

                SuppliesStackPanel.Children.Add(supplyBorder);

            }
        }

        private void AddImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            SupplySet supplyToAdd = supplies.Find(s => s.Name == ((Label)((StackPanel)((Image)sender).Parent).Children[1]).Content.ToString());
            StackPanel stackPanel = (StackPanel)((Image)sender).Parent;
            StackPanel quantity = (StackPanel)VisualTreeHelper.GetChild(stackPanel, 2);
            IntegerUpDown integerUpDown = (IntegerUpDown)VisualTreeHelper.GetChild(quantity, 1);
            if (integerUpDown.Value != null && integerUpDown.Value != 0)
            {
                AddSupplyToRecipe(supplyToAdd, Convert.ToInt32(integerUpDown.Text));
            }
            else
            {
                new AlertPopup("Error", "La cantidad no puede ser 0",
                                                          Auxiliary.AlertPopupTypes.Error);
            }

        }

        private void AddSupplyToRecipe(SupplySet supply, int quantity)
        {
            SupplySet supplySelected = suppliesSelected.Find(s => s.Id == supply.Id); 

            if (supplySelected == null)
            {
                supply.Quantity = quantity;
                suppliesSelected.Add(supply);
                AddVisualSuppliesToRecipe(supply);
            }
            else
            {
                new AlertPopup("Error", "El producto ya se encuentra en la lista de productos seleccionados",
                                       Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void AddVisualSuppliesToRecipe(SupplySet supplySelected)
        {
            string deleteSupplyIcon = Properties.Resources.ICON_DeleteImage;

            StackPanel IndividualSupply = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            Border summaryBorder = new Border
            {
                Height = 50,
                Margin = new Thickness(5, 4, 5, 0),
                CornerRadius = new CornerRadius(10),
                Background = (Brush)new BrushConverter().ConvertFrom("#7E1616"),
                Width = 531
            };

            StackPanel summaryStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center
            };

            Label supplyName = new Label
            {
                Margin = new Thickness(15, 0, 0, 0),
                Content = supplySelected.Name,
                FontFamily = new FontFamily("Segoe UI Variable Display"),
                FontWeight = FontWeights.Bold,
                FontSize = 18,
                Foreground = Brushes.White
            };

            Label supplyQuantity = new Label
            {
                Margin = new Thickness(130, 0, 0, 0),
                Content = supplySelected.Quantity,
                FontFamily = new FontFamily("Segoe UI Variable Display"),
                FontWeight = FontWeights.Bold,
                FontSize = 18,
                Foreground = Brushes.White
            };

            Image deleteImage = new Image
            {
                Width = 38,
                Height = 55,
                Margin = new Thickness(15, 0, 0, 0),
                Source = new BitmapImage(new Uri(deleteSupplyIcon, UriKind.RelativeOrAbsolute)),
                VerticalAlignment = VerticalAlignment.Center
            };

            deleteImage.MouseLeftButtonDown += DeleteSupply_MouseLeftButtonDown;

            summaryStackPanel.Children.Add(supplyName);
            summaryStackPanel.Children.Add(supplyQuantity);

            IndividualSupply.Children.Add(summaryBorder);
            IndividualSupply.Children.Add(deleteImage);

            summaryBorder.Child = summaryStackPanel;

            SuppliesSummaryStackPanel.Children.Add(IndividualSupply);

        }

        private void DeleteSupply_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label productName = (Label)((StackPanel)((Border)((StackPanel)((Image)sender).Parent).Children[0]).Child).Children[0];
            SupplySet supplyToDelete = suppliesSelected.Find(s => s.Name == productName.Content.ToString());
            suppliesSelected.Remove(supplyToDelete);
            SuppliesSummaryStackPanel.Children.Remove((StackPanel)((Image)sender).Parent);
        }

        private void GoToRecipeInstructions(object sender, RoutedEventArgs e)
        {
            if (suppliesSelected.Count == 0)
            {
                new AlertPopup("Error", "Debe seleccionar al menos un ingrediente para la receta",
                                                          Auxiliary.AlertPopupTypes.Error);
            }
            else
            {
                GrdIngredients.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(GrdIngredientsSummary, 2);
                Grid.SetColumn(GrdIngredientsSummary, 0);
                GrdIngredientsSummary.HorizontalAlignment = HorizontalAlignment.Left;
                GrdIngredientsSummary.Height = 841;
                GrdIngredientsSummary.Margin = new Thickness(299, 48, 0, 0);
                GrdIngredientsSummary.Width = 762;
                stckInstructions.Visibility = Visibility.Visible;
                ChangeNextButtonAction();
            }
        }

        private void ChangeNextButtonAction()
        {
            btnNext.Click -= GoToRecipeInstructions;
            btnNext.Content = "Regresar";
            btnNext.Click += GoToIngredients;

        }

        private void GoToIngredients(object sender, RoutedEventArgs e)
        {
            GrdIngredientsSummary.Visibility = Visibility.Visible;
            GrdIngredientsSummary.HorizontalAlignment = HorizontalAlignment.Left;
            GrdIngredientsSummary.Height = 841;
            GrdIngredientsSummary.Margin = new Thickness(379, 48, 0, 0);
            GrdIngredientsSummary.Width = 627;
            Grid.SetColumn(GrdIngredientsSummary, 1);
            GrdIngredients.Visibility = Visibility.Visible;
            stckInstructions.Visibility = Visibility.Collapsed;
            btnNext.Click -=  GoToIngredients;
            btnNext.Content = "Siguiente";
            btnNext.Click += GoToRecipeInstructions; ;
        }

        private void RegisterRecipe(object sender, RoutedEventArgs e)
        {
            RecipeDAO recipeDAO = new RecipeDAO();
            RecipeSet recipe = new RecipeSet
            {
                Name = productSaleSet.Name,
                Instructions = txtInstructions.Text,
                Status = active.ToString(),
                ProductSale_Id = productSaleSet.Id,
                EmployeeId = UserToken.GetEmployeeID()
            };

            List<RecipeDetailsSet> recipeDetails = new List<RecipeDetailsSet>();

            for (int i = 0; i < suppliesSelected.Count; i++)
            {
                RecipeDetailsSet recipeDetail = new RecipeDetailsSet
                {
                    SupplyId = suppliesSelected[i].Id,
                    Quantity = suppliesSelected[i].Quantity
                };
                recipeDetails.Add(recipeDetail);
            }


            int result = recipeDAO.AddRecipe(recipe, recipeDetails);

            if (result == 0)
            {
                new AlertPopup("Error", "La receta ya se encuentra registrada",
                                                                             Auxiliary.AlertPopupTypes.Error);
            }
            else if (result == -1)
            {
                new AlertPopup("Error", "Error con la base de datos",
                                                                             Auxiliary.AlertPopupTypes.Error);
            }
            else
            {
                new AlertPopup("Receta registrada", "La receta ha sido registrada con exito",
                                                                             Auxiliary.AlertPopupTypes.Success);
                NavigationService.Navigate(new GUI_Inventory());
            }
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtInstructions != null && Placeholder != null)
            {
                Placeholder.Visibility = string.IsNullOrEmpty(txtInstructions.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Placeholder != null)
            Placeholder.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Placeholder != null)
            if (string.IsNullOrEmpty(txtInstructions.Text))
            {
                Placeholder.Visibility = Visibility.Visible;
            }
        }

    }
}
