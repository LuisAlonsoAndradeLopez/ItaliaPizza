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
            *Validaciones de que si el nombre y el código ya están usados al registrar o modificar artículo
            *Mismas validaciones en guiaddarticle y guiinventory para agregar y modificar artículos
          
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
                string selectedImage = Convert.ToBase64String(new ImageManager().GetBitmapImageBytes((BitmapImage)ModifySelectedArticleImage.Source));

                if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                {
                    Insumo ingredient = new Insumo
                    {
                        Nombre = ModifySelectedArticleNameTextBox.Text,
                        Costo = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                        Descripcion = ModifySelectedArticleDescriptionTextBox.Text,
                        Tipo = ModifySelectedArticleTypeComboBox.SelectedItem?.ToString(),
                        Cantidad = ModifySelectedArticleQuantityIntegerUpDown.Value ?? 0,
                        Foto = selectedImage,
                        Estado = ArticleStatus.Activo.ToString(),
                        EmpleadoId = 12
                    };

                    new IngredientDAO().ModifyIngredient(ingredient);
                }

                if (SelectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                {
                    Producto product = new Producto
                    {
                        Nombre = ModifySelectedArticleNameTextBox.Text,
                        Costo = (double)ModifySelectedArticlePriceDecimalUpDown.Value,
                        Descripcion = ModifySelectedArticleDescriptionTextBox.Text,
                        Categoria = ModifySelectedArticleTypeComboBox.SelectedItem?.ToString(),
                        //Cantidad = QuantityIntegerUpDown.Value ?? 0,
                        Foto = selectedImage,
                        Estado = ArticleStatus.Activo.ToString(),
                        EmpleadoId = 12
                    };

                    new ProductDAO().ModifyProduct(product);
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
                ModifySelectedArticleTypeComboBox.Text = ArticleTypes.Insumo.ToString();
                ModifySelectedArticleQuantityIntegerUpDown.Text = ingredient.Cantidad.ToString();
                ModifySelectedArticleCodeTextBox.Text = "Pendiente";
                ModifySelectedArticlePriceDecimalUpDown.Text = "N/A";
                ModifySelectedArticleDescriptionTextBox.Text = ingredient.Descripcion;
            }

            if (product != null)
            {
                ModifySelectedArticleImage.Source = new ProductDAO().GetImageByProductName(product.Nombre);
                ModifySelectedArticleNameTextBox.Text = product.Nombre;
                ModifySelectedArticleTypeComboBox.Text = ArticleTypes.Producto.ToString();
                ModifySelectedArticleQuantityIntegerUpDown.Text = "Pendiente";
                ModifySelectedArticleCodeTextBox.Text = "Pendiente";
                ModifySelectedArticlePriceDecimalUpDown.Text = product.Costo.ToString();
                ModifySelectedArticleDescriptionTextBox.Text = product.Descripcion;
            }
        }
    }
}
