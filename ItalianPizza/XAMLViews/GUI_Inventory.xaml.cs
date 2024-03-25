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
using System.Windows.Input;

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
            ShowArticles(TextForFindingArticleTextBox.Text, ShowComboBox.SelectedItem?.ToString(), FindByComboBox.SelectedItem?.ToString());
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

        }

        private void SelectImageButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyArticleButtonOnClick1(object sender, RoutedEventArgs e)
        {

        }

        private void ConsultArticleRecipeButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {

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


            ArticlesScrollViewer.Content = null;

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
                    Source = new BitmapImage(new Uri(@"C:\Users\Luis Alonso\Pictures\boja.png"))
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
                    Text = product.Estado,
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
