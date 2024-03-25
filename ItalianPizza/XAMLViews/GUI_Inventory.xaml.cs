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
            *Manejo de excepciones en los daos de producto e insumos, GUI_Inventory y GUI_AddArticle
            *Cargar la imágen de la base de datos crashea en cualquier parte (no la warda bien)
            *Modificar la imágen del artículo seleccionado
            *Mostrar Insumos y productos en la tabla
          
          
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
            if (ShowComboBox != null)
            {
                ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
            }
        }

        private void ShowComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
        }

        private void FindByComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
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
            if (sender is Border border)
            {
                StackPanel borderStackPanelChild = (StackPanel)VisualTreeHelper.GetChild(border, 0);
                TextBlock selectedArticleNameTextBlock = (TextBlock)VisualTreeHelper.GetChild(borderStackPanelChild, 1);
                TextBlock selectedArticleTypeTextBlock = (TextBlock)VisualTreeHelper.GetChild(borderStackPanelChild, 2);

                Insumo ingredient = null;
                Producto product = null;

                if (selectedArticleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                {
                    ingredient = new IngredientDAO().GetIngredientByName(selectedArticleNameTextBlock.Text);
                }

                if (selectedArticleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                {
                    product = new ProductDAO().GetProductByName(selectedArticleNameTextBlock.Text);
                }

                if (ingredient != null)
                {
                    //SelectedArticleImage1.Source = new IngredientDAO().GetImageByIngredientName(ingredient.Nombre);
                    ArticleNameTextBlock.Text = ingredient.Nombre;
                    ArticleTypeTextBlock.Text = ArticleTypes.Insumo.ToString();
                    ArticleQuantityTextBlock.Text = ingredient.Cantidad.ToString();
                    ArticleStatusTextBlock.Text = ingredient.Estado;
                    ArticleCodeTextBlock.Text = "Pendiente";
                    ArticlePriceTextBlock.Text = "N/A";
                    ArticleDescriptionTextBlock.Text = ingredient.Descripcion;
                }

                if (product != null)
                {
                    //SelectedArticleImage1.Source = new ProductDAO().GetImageByProductName(product.Nombre);
                    ArticleNameTextBlock.Text = product.Nombre;
                    ArticleTypeTextBlock.Text = ArticleTypes.Producto.ToString();
                    ArticleQuantityTextBlock.Text = "Pendiente";
                    ArticleStatusTextBlock.Text = product.Estado;
                    ArticleCodeTextBlock.Text = "Pendiente";
                    ArticlePriceTextBlock.Text = product.Costo.ToString();
                    ArticleDescriptionTextBlock.Text = product.Descripcion;
                }

                SelectAnArticleTextBlock1.Visibility = Visibility.Collapsed;
                SelectAnArticleTextBlock2.Visibility = Visibility.Collapsed;
                SelectAnArticleTextBlock3.Visibility = Visibility.Collapsed;

                ArticleImageStackPanel.Visibility = Visibility.Visible;
                ArticleDetailsStackPanel.Visibility = Visibility.Visible;
                SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
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
                    SelectedArticleImage1.Source = imageSource;
                }
                else
                {
                    new AlertPopup("¡Tamaño de imágen excedido!", "La imágen no debe pesar más de 1MB", AlertPopupTypes.Error);
                }
            }
        }

        private void ModifyArticleButtonOnClick1(object sender, RoutedEventArgs e)
        {
            ArticleImageStackPanel.Visibility = Visibility.Collapsed;
            ArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
            SelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

            ModifyArticleImageStackPanel.Visibility = Visibility.Visible;
            ModifyArticleDetailsStackPanel.Visibility = Visibility.Visible;
            ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private void ConsultArticleRecipeButtonOnClick(object sender, RoutedEventArgs e)
        {
            //Este método lo hace el Álvaro
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {
            ModifyArticleImageStackPanel.Visibility = Visibility.Collapsed;
            ModifyArticleDetailsStackPanel.Visibility = Visibility.Collapsed;
            ModifySelectedArticleButtonsStackPanel.Visibility = Visibility.Collapsed;

            ArticleImageStackPanel.Visibility = Visibility.Visible;
            ArticleDetailsStackPanel.Visibility = Visibility.Visible;
            SelectedArticleButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private void DisableArticleButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyArticleButtonOnClick2(object sender, RoutedEventArgs e)
        {

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
                    //Source = new ProductDAO().GetImageByProductName(product.Nombre)
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
                    //Source = new ingredientDAO().GetImageByingredientName(ingredient.Nombre)
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
    }
}
