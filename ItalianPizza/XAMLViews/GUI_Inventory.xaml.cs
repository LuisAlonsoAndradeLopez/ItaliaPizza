using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Forms;
using Cursors = System.Windows.Input.Cursors;
using Orientation = System.Windows.Controls.Orientation; 
using System.Data.Entity.Core;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;
using System.IO;
using System.Globalization;


namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_Inventory.xaml
    /// </summary>
    public partial class GUI_Inventory : Page
    {
        public GUI_Inventory()
        {
            InitializeComponent();
            InitializeSearchComboBoxes();

            ShowArticles("", ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
        }

        private void UsersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void FinanceButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ProvidersButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSessionButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void TextForFindingArticleTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ShowComboBox != null)
                {
                    ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ShowComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void FindByComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (FindByComboBox.SelectedItem?.ToString() == "Nombre")
                {
                    ArticleNameOrCodeLabel.Content = "Nombre del artículo: ";
                }
                
                if (FindByComboBox.SelectedItem?.ToString() == "Código")
                {
                    ArticleNameOrCodeLabel.Content = "Código del artículo: ";
                }


                ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
            }
            catch (EntityException ex)
            { 
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error); 
                new ExceptionLogger().LogException(ex); 
            }
        }

        private void AddArticleButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_AddArticle());
        }

        private void GenerateInventoryReportOnClick(object sender, RoutedEventArgs e)
        {
            //NavigationService navigationService = NavigationService.GetNavigationService(this);
            //navigationService.Navigate(new GUI_InventoryReport());
        }

        private void ArticleButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Border border)
                {
                    StackPanel borderStackPanelChild = (StackPanel)VisualTreeHelper.GetChild(border, 0);
                    TextBlock selectedArticleNameTextBlock = (TextBlock)VisualTreeHelper.GetChild(borderStackPanelChild, 1);
                    TextBlock selectedArticleTypeTextBlock = (TextBlock)VisualTreeHelper.GetChild(borderStackPanelChild, 2);

                    UpdateSelectedArticleDetailsStackPanel(selectedArticleNameTextBlock.Text, selectedArticleTypeTextBlock.Text);

                    SelectAnArticleTextBlock1.Visibility = Visibility.Collapsed;
                    SelectAnArticleTextBlock2.Visibility = Visibility.Collapsed;
                    SelectAnArticleTextBlock3.Visibility = Visibility.Collapsed;

                    SelectedArticleImageStackPanel.Visibility = Visibility.Visible;
                    SelectedArticleDetailsStackPanel.Visibility = Visibility.Visible;
                    SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void SelectImageButtonOnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png)|*.png",
                Title = "Selecciona una imágen"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                BitmapImage imageSource = new BitmapImage(new Uri(openFileDialog.FileName));

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 10 * 1024 * 1024)
                {
                    ModifySelectedArticleImage.Source = imageSource;
                }
                else
                {
                    new AlertPopup("¡Tamaño de imágen excedido!", "La imágen no debe pesar más de 1MB", AlertPopupTypes.Error);
                }
            }
        }

        private void ModifyArticleButtonOnClick1(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateModifySelectedArticleDetailsStackPanel();
                InitializeComboboxesForModifySomeSelectedArticleData();

                if (SelectedArticleStatusTextBlock.Text == ArticleStatus.Activo.ToString())
                {
                    DisableArticleButton.Visibility = Visibility.Visible;
                }

                if (SelectedArticleStatusTextBlock.Text == ArticleStatus.Inactivo.ToString())
                {
                    DisableArticleButton.Visibility = Visibility.Hidden;
                }

                SelectedArticleImageStackPanel.Visibility = Visibility.Collapsed;
                SelectedArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
                SelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

                ModifySelectedArticleImageStackPanel.Visibility = Visibility.Visible;
                ModifySelectedArticleDetailsStackPanel.Visibility = Visibility.Visible;
                ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ConsultArticleRecipeButtonOnClick(object sender, RoutedEventArgs e)
        {
            //Este método lo hace el Álvaro
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {
            ModifySelectedArticleImageStackPanel.Visibility = Visibility.Collapsed;
            ModifySelectedArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
            ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

            SelectedArticleImageStackPanel.Visibility = Visibility.Visible;
            SelectedArticleDetailsStackPanel.Visibility = Visibility.Visible;
            SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private void DisableArticleButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if(new AlertPopup("¿Está seguro?", "¿Está seguro que quiere desactivar este artículo?", AlertPopupTypes.Decision).GetDecisionOfDecisionPopup())
                {
                    if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                    {
                        new SupplyDAO().DisableSupply(SelectedArticleNameTextBlock.Text);
                    }
                
                    if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                    {
                        new ProductDAO().DisableProduct(SelectedArticleNameTextBlock.Text);
                    }
                
                    new AlertPopup("¡Muy bien!", "Artículo desactivado con éxito", AlertPopupTypes.Success);
                
                    UpdateSelectedArticleDetailsStackPanel(SelectedArticleNameTextBlock.Text, SelectedArticleTypeTextBlock.Text);
                    ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());

                    ModifySelectedArticleImageStackPanel.Visibility = Visibility.Collapsed;
                    ModifySelectedArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
                    ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

                    SelectedArticleImageStackPanel.Visibility = Visibility.Visible;
                    SelectedArticleDetailsStackPanel.Visibility = Visibility.Visible;
                    SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ModifyArticleButtonOnClick2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InvalidValuesInTextFieldsTextGenerator() == "")
                {
                    if (new ImageManager().GetBitmapImageBytes((BitmapImage)ModifySelectedArticleImage.Source) != null)
                    {
                        if ((!new SupplyDAO().TheNameIsAlreadyRegistred(ModifySelectedArticleNameTextBox.Text) &&
                            !new ProductDAO().TheNameIsAlreadyRegistred(ModifySelectedArticleNameTextBox.Text)) ||
                            SelectedArticleNameTextBlock.Text == ModifySelectedArticleNameTextBox.Text)
                        {
                            if ((!new SupplyDAO().TheCodeIsAlreadyRegistred(ModifySelectedArticleCodeTextBox.Text) &&
                                !new ProductDAO().TheCodeIsAlreadyRegistred(ModifySelectedArticleCodeTextBox.Text)) ||
                                SelectedArticleCodeTextBlock.Text == ModifySelectedArticleCodeTextBox.Text)
                            {
                                if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                                {
                                    SupplySet originalSupply = new SupplyDAO().GetSupplyByName(SelectedArticleNameTextBlock.Text);

                                    SupplySet modifiedSupply = new SupplySet
                                    {
                                        Name = ModifySelectedArticleNameTextBox.Text,
                                        Quantity = ModifySelectedArticleQuantityIntegerUpDown.Value ?? 0,
                                        PricePerUnit = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                                        Picture = new ImageManager().GetBitmapImageBytes((BitmapImage)ModifySelectedArticleImage.Source),
                                        SupplyUnitId = new SupplyUnitDAO().GetSupplyUnitByName(ModifySelectedArticleUnitComboBox.SelectedItem?.ToString()).Id,
                                        ProductStatusId = new ProductStatusDAO().GetProductStatusByName(SelectedArticleStatusTextBlock.Text.ToString()).Id,
                                        SupplyTypeId = new SupplyTypeDAO().GetSupplyTypeByName(ModifySelectedArticleCategoryComboBox.SelectedItem?.ToString()).Id,
                                        EmployeeId = 1,
                                        IdentificationCode = ModifySelectedArticleCodeTextBox.Text
                                    };

                                    new SupplyDAO().ModifySupply(originalSupply, modifiedSupply);
                                }

                                if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                                {
                                    ProductSaleSet originalProduct = new ProductDAO().GetProductByName(SelectedArticleNameTextBlock.Text);

                                    ProductSaleSet modifiedProduct = new ProductSaleSet
                                    {
                                        Name = ModifySelectedArticleNameTextBox.Text,
                                        Quantity = ModifySelectedArticleQuantityIntegerUpDown.Value ?? 0,
                                        PricePerUnit = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                                        Picture = new ImageManager().GetBitmapImageBytes((BitmapImage)ModifySelectedArticleImage.Source),
                                        ProductStatusId = new ProductStatusDAO().GetProductStatusByName(SelectedArticleStatusTextBlock.Text.ToString()).Id,
                                        ProductTypeId = new ProductTypeDAO().GetProductTypeByName(ModifySelectedArticleCategoryComboBox.SelectedItem?.ToString()).Id,
                                        EmployeeId = 1,
                                        IdentificationCode = ModifySelectedArticleCodeTextBox.Text,
                                        Description = ModifySelectedArticleDescriptionTextBox.Text
                                    };

                                    new ProductDAO().ModifyProduct(originalProduct, modifiedProduct);
                                }

                                new AlertPopup("¡Muy bien!", "Artículo modificado con éxito", AlertPopupTypes.Success);

                                UpdateSelectedArticleDetailsStackPanel(ModifySelectedArticleNameTextBox.Text, SelectedArticleTypeTextBlock.Text);
                                UpdateModifySelectedArticleDetailsStackPanel();
                                InitializeComboboxesForModifySomeSelectedArticleData();
                                ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());

                                ModifySelectedArticleImageStackPanel.Visibility = Visibility.Collapsed;
                                ModifySelectedArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
                                ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

                                SelectedArticleImageStackPanel.Visibility = Visibility.Visible;
                                SelectedArticleDetailsStackPanel.Visibility = Visibility.Visible;
                                SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                new AlertPopup("¡Código ya usado!", "El código ya está usado, por favor introduzca otro", AlertPopupTypes.Error);
                            }
                        }
                        else
                        {
                            new AlertPopup("¡Nombre ya usado!", "El nombre ya está usado, por favor introduzca otro", AlertPopupTypes.Error);
                        }
                    }
                    else
                    {
                        new AlertPopup("¡Falta la imágen!", "Falta que selecciones la imágen", AlertPopupTypes.Error);
                    }
                }
                else
                {
                    new AlertPopup("¡Campos Incorrectos!", InvalidValuesInTextFieldsTextGenerator(), AlertPopupTypes.Error);
                }
            }
            catch (DbEntityValidationException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);

                string incompletePath = Path.GetFullPath("ValidationErrors.txt");
                string pathPartToDelete = "ItalianPizza\\bin\\Debug\\";
                string completePath = incompletePath.Replace(pathPartToDelete, "");

                using (StreamWriter writer = new StreamWriter(completePath))
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            writer.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void InitializeSearchComboBoxes()
        {
            string[] showTypes = { ArticleTypes.Insumo.ToString(), ArticleTypes.Producto.ToString() };

            foreach (var showType in showTypes)
            {
                ShowComboBox.Items.Add(showType);
            }

            ShowComboBox.SelectedItem = ShowComboBox.Items[0];


            string[] findByTypes = { "Nombre", "Código" };

            foreach (var findByType in findByTypes)
            {
                FindByComboBox.Items.Add(findByType);
            }
             
            FindByComboBox.SelectedItem = FindByComboBox.Items[0];
        }

        private void InitializeComboboxesForModifySomeSelectedArticleData()
        {
            List<SupplyTypeSet> supplyTypes = new SupplyTypeDAO().GetAllSupplyTypes();
            List<ProductTypeSet> productTypes = new ProductTypeDAO().GetAllProductTypes();
            List<SupplyUnitSet> supplyUnits = new SupplyUnitDAO().GetAllSupplyUnits();

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
            {
                foreach (var supplyType in supplyTypes)
                {
                    ModifySelectedArticleCategoryComboBox.Items.Add(supplyType.Type);
                }

                foreach (var supplyUnit in supplyUnits)
                {
                    ModifySelectedArticleUnitComboBox.Items.Add(supplyUnit.Unit);
                }

                ModifySelectedArticleCategoryComboBox.SelectedItem = new SupplyTypeDAO().GetSupplyTypeByName(SelectedArticleCategoryTextBlock.Text.ToString()).Type;
                ModifySelectedArticleUnitComboBox.SelectedItem = new SupplyUnitDAO().GetSupplyUnitByName(SelectedArticleUnitTextBlock.Text.ToString()).Unit;
            }

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
            {
                foreach (var productType in productTypes)
                {
                    ModifySelectedArticleCategoryComboBox.Items.Add(productType.Type);
                }

                ModifySelectedArticleCategoryComboBox.SelectedItem = new ProductTypeDAO().GetProductTypeByName(SelectedArticleCategoryTextBlock.Text.ToString()).Type;
            }
        }

        private void ShowArticles(string textForFindingArticle, string showType, string findByType)
        {
            List<SupplySet> supplies = new List<SupplySet>();
            List<ProductSaleSet> products = new List<ProductSaleSet>();

            if (showType == ArticleTypes.Insumo.ToString())
            {
                supplies = new SupplyDAO().GetSpecifiedSuppliesByNameOrCode(textForFindingArticle, findByType);
            }

            if (showType == ArticleTypes.Producto.ToString())
            {
                products = new ProductDAO().GetSpecifiedProductsByNameOrCode(textForFindingArticle, findByType);
            }

            ArticlesStackPanel.Children.Clear();

            foreach (var product in products)
            {
                Border articleBorder = new Border
                {
                    Cursor = Cursors.Hand,
                    Height = 142,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(126, 22, 22)) // Equivalent to "#7E1616"
                };

                articleBorder.MouseLeftButtonDown += ArticleButtonOnClick;

                StackPanel articleStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Image articleImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(40, 0, 0, 0),
                    Source = new ProductDAO().GetImageByProductName(product.Name)
                };

                TextBlock articleNameTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(47, 0, 0, 0),
                    Width = 290,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = product.Name
                };

                TextBlock articleTypeTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(10, 0, 0, 0),
                    Width = 142,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = ArticleTypes.Producto.ToString()
                };

                TextBlock articleStatusTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(10, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = new ProductStatusDAO().GetProductStatusById(product.ProductStatusId).Status
                };

                
                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleStatusTextBlock);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }

            foreach (var supply in supplies)
            {
                Border articleBorder = new Border
                {
                    Cursor = Cursors.Hand,
                    Height = 142,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(126, 22, 22)) // Equivalent to "#7E1616"
                };

                articleBorder.MouseLeftButtonDown += ArticleButtonOnClick;

                StackPanel articleStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Image articleImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(40, 0, 0, 0),
                    Source = new SupplyDAO().GetImageBySupplyName(supply.Name)
                };

                TextBlock articleNameTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(47, 0, 0, 0),
                    Width = 290,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = supply.Name
                };

                TextBlock articleTypeTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(10, 0, 0, 0),
                    Width = 142,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = ArticleTypes.Insumo.ToString()
                };

                TextBlock articleStatusTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(10, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = new ProductStatusDAO().GetProductStatusById(supply.ProductStatusId).Status
                };


                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleStatusTextBlock);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }
        }

        private void UpdateSelectedArticleDetailsStackPanel(string articleName, string articleType)
        {
            SupplySet supply = null;
            ProductSaleSet product = null;

            if (articleType == ArticleTypes.Insumo.ToString())
            {
                supply = new SupplyDAO().GetSupplyByName(articleName);
                SelectedArticleNameStackPanel.Margin = new Thickness(0, 66, 0, 0);
                SelectedArticleUnitStackPanel.Visibility = Visibility.Visible;
                SelectedArticleDescriptionStackPanel.Visibility = Visibility.Collapsed;
            }

            if (articleType == ArticleTypes.Producto.ToString())
            {
                product = new ProductDAO().GetProductByName(articleName);
                SelectedArticleNameStackPanel.Margin = new Thickness(0, 0, 0, 0);
                SelectedArticleUnitStackPanel.Visibility = Visibility.Collapsed;
                SelectedArticleDescriptionStackPanel.Visibility = Visibility.Visible;
            }

            if (supply != null)
            {
                SelectedArticleImage.Source = new SupplyDAO().GetImageBySupplyName(supply.Name);
                SelectedArticleNameTextBlock.Text = supply.Name;
                SelectedArticleTypeTextBlock.Text = ArticleTypes.Insumo.ToString();
                SelectedArticleCategoryTextBlock.Text = new SupplyTypeDAO().GetSupplyTypeById(supply.SupplyTypeId).Type;
                SelectedArticleUnitTextBlock.Text = new SupplyUnitDAO().GetSupplyUnitById(supply.SupplyUnitId).Unit;
                SelectedArticleQuantityTextBlock.Text = supply.Quantity.ToString();
                SelectedArticleStatusTextBlock.Text = new ProductStatusDAO().GetProductStatusById(supply.ProductStatusId).Status;
                SelectedArticleCodeTextBlock.Text = supply.IdentificationCode;
                SelectedArticlePriceTextBlock.Text = supply.PricePerUnit.ToString();
            }

            if (product != null)
            {
                SelectedArticleImage.Source = new ProductDAO().GetImageByProductName(product.Name);
                SelectedArticleNameTextBlock.Text = product.Name;
                SelectedArticleTypeTextBlock.Text = ArticleTypes.Producto.ToString();
                SelectedArticleCategoryTextBlock.Text = new ProductTypeDAO().GetProductTypeById(product.ProductTypeId).Type;
                SelectedArticleQuantityTextBlock.Text = product.Quantity.ToString();
                SelectedArticleStatusTextBlock.Text = new ProductStatusDAO().GetProductStatusById(product.ProductStatusId).Status; ;
                SelectedArticleCodeTextBlock.Text = product.IdentificationCode;
                SelectedArticlePriceTextBlock.Text = product.PricePerUnit.ToString();
                SelectedArticleDescriptionTextBlock.Text = product.Description;
            }
        }

        private void UpdateModifySelectedArticleDetailsStackPanel()
        {
            ModifySelectedArticlePriceDecimalUpDown.Text = "";

            SupplySet supply = null;
            ProductSaleSet product = null;

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
            {
                supply = new SupplyDAO().GetSupplyByName(SelectedArticleNameTextBlock.Text);
                ModifySelectedArticleNameStackPanel.Margin = new Thickness(0, 90, 0, 0);
                ModifySelectedArticleUnitStackPanel.Visibility = Visibility.Visible;
                ModifySelectedArticleDescriptionStackPanel.Visibility = Visibility.Collapsed;
            }

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
            {
                product = new ProductDAO().GetProductByName(SelectedArticleNameTextBlock.Text);
                SelectedArticleNameStackPanel.Margin = new Thickness(0, 0, 0, 0);
                SelectedArticleUnitStackPanel.Visibility = Visibility.Collapsed;
                SelectedArticleDescriptionStackPanel.Visibility = Visibility.Visible;
            }

            if (supply != null)
            {
                ModifySelectedArticleImage.Source = new SupplyDAO().GetImageBySupplyName(supply.Name);
                ModifySelectedArticleNameTextBox.Text = supply.Name;
                ModifySelectedArticleCategoryComboBox.SelectedItem = new SupplyTypeDAO().GetSupplyTypeById(supply.SupplyTypeId).Type;
                ModifySelectedArticleQuantityIntegerUpDown.Text = supply.Quantity.ToString();
                ModifySelectedArticleCodeTextBox.Text = supply.IdentificationCode;

                string supplyPrice = "$" + supply.PricePerUnit.ToString();
                if (!supplyPrice.Contains("."))
                {
                    supplyPrice += ".00";
                }

                ModifySelectedArticlePriceDecimalUpDown.Text = supplyPrice;
            }

            if (product != null)
            {
                ModifySelectedArticleImage.Source = new ProductDAO().GetImageByProductName(product.Name);
                ModifySelectedArticleNameTextBox.Text = product.Name;
                ModifySelectedArticleCategoryComboBox.SelectedItem = new ProductTypeDAO().GetProductTypeById(product.ProductTypeId).Type;
                ModifySelectedArticleQuantityIntegerUpDown.Text = product.Quantity.ToString();
                ModifySelectedArticleCodeTextBox.Text = product.IdentificationCode;

                string productPrice = "$" + product.PricePerUnit.ToString();
                if (!productPrice.Contains("."))
                {
                    productPrice += ".00";
                }

                ModifySelectedArticlePriceDecimalUpDown.Text = productPrice;
                ModifySelectedArticleDescriptionTextBox.Text = product.Description;
            }
        }

        private string InvalidValuesInTextFieldsTextGenerator()
        {
            int textFieldsWithIncorrectValues = 0;

            string finalText = "";

            string articleNamePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string quantityPattern = "^\\d+";
            string pricePerUnitPattern = "^\\d+(\\.\\d{2})?$";
            string codePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string descriptionPattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";

            Regex articleNameRegex = new Regex(articleNamePattern);
            Regex quantityRegex = new Regex(quantityPattern);
            Regex pricePerUnitRegex = new Regex(pricePerUnitPattern);
            Regex codeRegex = new Regex(codePattern);
            Regex descriptionRegex = new Regex(descriptionPattern);

            Match articleNameMatch = articleNameRegex.Match(ModifySelectedArticleNameTextBox.Text);
            Match quantityMatch = quantityRegex.Match(ModifySelectedArticleQuantityIntegerUpDown.Value.ToString());
            Match pricePerUnitMatch = pricePerUnitRegex.Match(ModifySelectedArticlePriceDecimalUpDown.Value.ToString());
            Match codeMatch = codeRegex.Match(ModifySelectedArticleCodeTextBox.Text);

            Match descriptionMatch = null;

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
            {
                descriptionMatch = descriptionRegex.Match("A");
            }

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
            {
                descriptionMatch = descriptionRegex.Match(ModifySelectedArticleDescriptionTextBox.Text);
            }


            if (!articleNameMatch.Success || !quantityMatch.Success || !pricePerUnitMatch.Success || !codeMatch.Success || !descriptionMatch.Success)
            {
                finalText += "Los campos con valores inválidos son: ";
            }

            if (!articleNameMatch.Success)
            {
                finalText = finalText + "Nombre del Artículo" + ".";
                textFieldsWithIncorrectValues++;
            }

            if (!quantityMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Cantidad" + ".";
                }
                else
                {
                    finalText = finalText + "Cantidad" + ".";
                }

                textFieldsWithIncorrectValues++;
            }

            if (!pricePerUnitMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Precio" + ".";
                }
                else
                {
                    finalText = finalText + "Precio" + ".";
                }

                textFieldsWithIncorrectValues++;
            }

            if (!codeMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Código" + ".";
                }
                else
                {
                    finalText = finalText + "Código" + ".";
                }

                textFieldsWithIncorrectValues++;
            }

            if (!descriptionMatch.Success)
            {
                if (textFieldsWithIncorrectValues >= 1)
                {
                    finalText = finalText.Substring(0, finalText.Length - 1);
                    finalText = finalText + ", " + "Descripción" + ".";
                }
                else
                {
                    finalText = finalText + "Descripción" + ".";
                }
            }

            return finalText;
        }
    }
}
