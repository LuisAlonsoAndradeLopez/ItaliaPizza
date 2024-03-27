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
using System.Reflection;
using System.Data.Entity.Core;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;
using System.IO;


namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_Inventory.xaml
    /// </summary>
    public partial class GUI_Inventory : Page
    {
        public GUI_Inventory()
        {/*
          TODO:
            *DAOS para categorias de insumo y producto
            *Crear Tablas para categorias de insumo y producto
            *Categorias de insumo y producto en comboboxes para agregar y modificar artículo
            *Preguntarle a camo si agregar el código o descartarlo, si se agrega agregar campos en la base
            *Cambiar la columna tipo a categoria a la tabla Insumo
            *Objeto "Precio" con sus enteros y centavos (como dice ocharán)
            *Bloquear DecimalCombobox para que solamente acepte dos decimales
            *Crear y conservar consultas para categorias de insumo y producto
          */


            InitializeComponent();
            InitializeComboBoxes();

            ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
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

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 1048576)
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
                        new IngredientDAO().DisableIngredient(SelectedArticleNameTextBlock.Text);
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
                        if ( (!new IngredientDAO().TheNameIsAlreadyRegistred(ModifySelectedArticleNameTextBox.Text) &&
                            !new ProductDAO().TheNameIsAlreadyRegistred(ModifySelectedArticleNameTextBox.Text)) ||
                            SelectedArticleNameTextBlock.Text == ModifySelectedArticleNameTextBox.Text)
                        {
                            string selectedImage = Convert.ToBase64String(new ImageManager().GetBitmapImageBytes((BitmapImage)ModifySelectedArticleImage.Source));

                            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                            {
                                Insumo originalIngredient = new IngredientDAO().GetIngredientByName(SelectedArticleNameTextBlock.Text);

                                Insumo modifiedIngredient = new Insumo
                                {
                                    Nombre = ModifySelectedArticleNameTextBox.Text,
                                    Costo = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                                    Descripcion = ModifySelectedArticleDescriptionTextBox.Text,
                                    //Tipo = ModifySelectedArticleTypeComboBox.SelectedItem?.ToString(),
                                    Cantidad = ModifySelectedArticleQuantityIntegerUpDown.Value ?? 0,
                                    Foto = selectedImage,
                                    Estado = ArticleStatus.Activo.ToString(),
                                    EmpleadoId = 12
                                };

                                new IngredientDAO().ModifyIngredient(originalIngredient, modifiedIngredient);
                            }

                            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                            {
                                Producto originalProduct = new ProductDAO().GetProductByName(SelectedArticleNameTextBlock.Text);

                                Producto modifiedProduct = new Producto
                                {
                                    Nombre = ModifySelectedArticleNameTextBox.Text,
                                    Costo = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                                    Descripcion = ModifySelectedArticleDescriptionTextBox.Text,
                                    Categoria = ModifySelectedArticleCategoryComboBox.SelectedItem?.ToString(),
                                    //Cantidad = QuantityIntegerUpDown.Value ?? 0,
                                    Foto = selectedImage,
                                    Estado = ArticleStatus.Activo.ToString(),
                                    EmpleadoId = 12
                                };

                                new ProductDAO().ModifyProduct(originalProduct, modifiedProduct);
                            }

                            new AlertPopup("¡Muy bien!", "Artículo modificado con éxito", AlertPopupTypes.Success);

                            UpdateSelectedArticleDetailsStackPanel(ModifySelectedArticleNameTextBox.Text, SelectedArticleTypeTextBlock.Text);
                            UpdateModifySelectedArticleDetailsStackPanel();
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

        private void InitializeComboBoxes()
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

        private void ShowArticles(string textForFindingArticle, string showType, string findByType)
        {
            List<Insumo> ingredients = new List<Insumo>();
            List<Producto> products = new List<Producto>();

            if (showType == ArticleTypes.Insumo.ToString())
            {
                ingredients = new IngredientDAO().GetSpecifiedIngredientsByNameOrCode(textForFindingArticle, findByType);
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
                    Source = new ProductDAO().GetImageByProductName(product.Nombre)
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
                    Text = product.Nombre
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
                    Text = product.Estado
                };

                
                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleStatusTextBlock);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }

            foreach (var ingredient in ingredients)
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
                    Source = new IngredientDAO().GetImageByIngredientName(ingredient.Nombre)
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
                    Text = ingredient.Nombre
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
                    Text = ingredient.Estado
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
            Insumo ingredient = null;
            Producto product = null;

            if (articleType == ArticleTypes.Insumo.ToString())
            {
                ingredient = new IngredientDAO().GetIngredientByName(articleName);
            }

            if (articleType == ArticleTypes.Producto.ToString())
            {
                product = new ProductDAO().GetProductByName(articleName);
            }

            if (ingredient != null)
            {
                SelectedArticleImage.Source = new IngredientDAO().GetImageByIngredientName(ingredient.Nombre);
                SelectedArticleNameTextBlock.Text = ingredient.Nombre;
                SelectedArticleTypeTextBlock.Text = ArticleTypes.Insumo.ToString();
                SelectedArticleCategoryTextBlock.Text = "Pendiente";
                SelectedArticleQuantityTextBlock.Text = ingredient.Cantidad.ToString();
                SelectedArticleStatusTextBlock.Text = ingredient.Estado;
                SelectedArticleCodeTextBlock.Text = "Pendiente";
                SelectedArticlePriceTextBlock.Text = "N/A";
                SelectedArticleDescriptionTextBlock.Text = ingredient.Descripcion;
            }

            if (product != null)
            {
                SelectedArticleImage.Source = new ProductDAO().GetImageByProductName(product.Nombre);
                SelectedArticleNameTextBlock.Text = product.Nombre;
                SelectedArticleTypeTextBlock.Text = ArticleTypes.Producto.ToString();
                SelectedArticleCategoryTextBlock.Text = product.Categoria;
                SelectedArticleQuantityTextBlock.Text = "Pendiente";
                SelectedArticleStatusTextBlock.Text = product.Estado;
                SelectedArticleCodeTextBlock.Text = "Pendiente";
                SelectedArticlePriceTextBlock.Text = product.Costo.ToString();
                SelectedArticleDescriptionTextBlock.Text = product.Descripcion;
            }
        }

        private void UpdateModifySelectedArticleDetailsStackPanel()
        {
            Insumo ingredient = null;
            Producto product = null;

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
            {
                ingredient = new IngredientDAO().GetIngredientByName(SelectedArticleNameTextBlock.Text);
            }

            if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
            {
                product = new ProductDAO().GetProductByName(SelectedArticleNameTextBlock.Text);
            }

            if (ingredient != null)
            {
                ModifySelectedArticleImage.Source = new IngredientDAO().GetImageByIngredientName(ingredient.Nombre);
                ModifySelectedArticleNameTextBox.Text = ingredient.Nombre;
                ModifySelectedArticleCategoryComboBox.SelectedItem = "Pendiente";
                ModifySelectedArticleQuantityIntegerUpDown.Text = ingredient.Cantidad.ToString();
                ModifySelectedArticleCodeTextBox.Text = "Pendiente";
                ModifySelectedArticlePriceDecimalUpDown.Text = "N/A";
                ModifySelectedArticleDescriptionTextBox.Text = ingredient.Descripcion;
            }

            if (product != null)
            {
                ModifySelectedArticleImage.Source = new ProductDAO().GetImageByProductName(product.Nombre);
                ModifySelectedArticleNameTextBox.Text = product.Nombre;
                ModifySelectedArticleCategoryComboBox.SelectedItem = product.Categoria;
                ModifySelectedArticleQuantityIntegerUpDown.Text = "Pendiente";
                ModifySelectedArticleCodeTextBox.Text = "Pendiente";
                ModifySelectedArticlePriceDecimalUpDown.Text = product.Costo.ToString();
                ModifySelectedArticleDescriptionTextBox.Text = product.Descripcion;
            }
        }

        private string InvalidValuesInTextFieldsTextGenerator()
        {
            int textFieldsWithIncorrectValues = 0;

            string finalText = "";

            string articleNamePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string codePattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";
            string descriptionPattern = "^[A-Za-z0-9áéíóúÁÉÍÓÚ\\s]+$";

            Regex articleNameRegex = new Regex(articleNamePattern);
            Regex codeRegex = new Regex(codePattern);
            Regex descriptionRegex = new Regex(descriptionPattern);

            Match articleNameMatch = articleNameRegex.Match(ModifySelectedArticleNameTextBox.Text);
            Match codeMatch = codeRegex.Match(ModifySelectedArticleCodeTextBox.Text);
            Match descriptionMatch = descriptionRegex.Match(ModifySelectedArticleDescriptionTextBox.Text);

            if (!articleNameMatch.Success || !codeMatch.Success || !descriptionMatch.Success)
            {
                finalText += "Los campos con valores inválidos son: ";
            }

            if (!articleNameMatch.Success)
            {
                finalText = finalText + "Nombre del Artículo" + ".";
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
