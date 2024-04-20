using CrystalDecisions.CrystalReports.Engine;
using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.XAMLViews.Articles;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit;
using Border = System.Windows.Controls.Border;
using Orientation = System.Windows.Controls.Orientation;
using TextBox = System.Windows.Controls.TextBox;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_InventoryReport.xaml
    /// </summary>
    public partial class GUI_InventoryReport : Page
    {
        public GUI_InventoryReport()
        {
            InitializeComponent();

            ShowArticles("");
        }

        private void TextForFindingArticleTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ShowArticles(TextForFindingArticleTextBox.Text);
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ManualQuantityIntegerUpDownPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BackButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new GUI_Inventory());
        }

        private void ExportToPDFButtonOnClick(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new InventoryReportPreview());
        }

        private void SaveJustificationButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Border> articlesBorders = new List<Border>();

                foreach (Border articleBorder in ArticlesStackPanel.Children)
                {
                    StackPanel articleBorderStackPanel = (StackPanel)VisualTreeHelper.GetChild(articleBorder, 0);
                    TextBlock articleNameTextBlock = (TextBlock)VisualTreeHelper.GetChild(articleBorderStackPanel, 1);
                    TextBlock articleTypeTextBlock = (TextBlock)VisualTreeHelper.GetChild(articleBorderStackPanel, 2);
                    IntegerUpDown articleManualQuantityIntegerUpDown = (IntegerUpDown)VisualTreeHelper.GetChild(articleBorderStackPanel, 4);
                    TextBox articleObservationsTextBox = (TextBox)VisualTreeHelper.GetChild(articleBorderStackPanel, 5);

                    if (articleTypeTextBlock.Text == ArticleTypes.Producto.ToString())
                    {
                        new ProductDAO().UpdateProductObservations(articleNameTextBlock.Text, articleObservationsTextBox.Text);
                        new ProductDAO().UpdateProductRegisteredQuantity(articleNameTextBlock.Text, int.Parse(articleManualQuantityIntegerUpDown.Text));
                    }

                    if (articleTypeTextBlock.Text == ArticleTypes.Insumo.ToString())
                    {
                        new SupplyDAO().UpdateSupplyObservations(articleNameTextBlock.Text, articleObservationsTextBox.Text);
                        new SupplyDAO().UpdateSupplyRegisteredQuantity(articleNameTextBlock.Text, int.Parse(articleManualQuantityIntegerUpDown.Text));
                    }
                }

                new AlertPopup("¡Muy bien!", "Justificación de inventario creada con éxito", AlertPopupTypes.Success);
            }
            catch (Exception ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ShowArticles(string textForFindingArticle)
        {
            List<SupplySet> supplies = new SupplyDAO().GetSpecifiedSuppliesByNameOrCode(textForFindingArticle, "Nombre");
            List<ProductSaleSet> products = new ProductDAO().GetSpecifiedProductsByNameOrCode(textForFindingArticle, "Nombre");

            ArticlesStackPanel.Children.Clear();

            foreach (var product in products)
            {
                Border articleBorder = new Border
                {
                    Height = 136,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(126, 22, 22)) // Equivalent to "#7E1616"
                };

                StackPanel articleStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Image articleImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(26, 0, 0, 0),
                    Source = new ProductDAO().GetImageByProductName(product.Name)
                };

                TextBlock articleNameTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(30, 0, 0, 0),
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
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 142,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = ArticleTypes.Producto.ToString()
                };

                TextBlock articleRegisteredQuantityTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(46, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = product.Quantity.ToString()                    
                };

                IntegerUpDown articleManualQuantityIntegerUpDown = new IntegerUpDown
                {
                    Margin = new Thickness(74, 0, 0, 0),
                    Value = 0,
                    Increment = 1,
                    Maximum = 10000,
                    Minimum = 0,
                    MaxLength = 5,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 36,
                    Width = 129,
                    FontSize = 24,
                    Text = product.Quantity.ToString()
                };

                articleManualQuantityIntegerUpDown.PreviewTextInput += ManualQuantityIntegerUpDownPreviewTextInput;

                TextBox articleObservationsTextBox = new TextBox
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(54, 0, 0, 0),
                    Width = 323,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 108,
                    Text = product.Observations
                };

                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleRegisteredQuantityTextBlock);
                articleStackPanel.Children.Add(articleManualQuantityIntegerUpDown);
                articleStackPanel.Children.Add(articleObservationsTextBox);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }

            foreach (var supply in supplies)
            {
                Border articleBorder = new Border
                {
                    Height = 136,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(126, 22, 22)) // Equivalent to "#7E1616"
                };

                StackPanel articleStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                Image articleImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(26, 0, 0, 0),
                    Source = new SupplyDAO().GetImageBySupplyName(supply.Name)
                };

                TextBlock articleNameTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(30, 0, 0, 0),
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
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 142,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = ArticleTypes.Insumo.ToString()
                };

                TextBlock articleRegisteredQuantityTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(46, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = supply.Quantity.ToString()
                };

                IntegerUpDown articleManualQuantityIntegerUpDown = new IntegerUpDown
                {
                    Margin = new Thickness(74, 0, 0, 0),
                    Value = 0,
                    Increment = 1,
                    Maximum = 10000,
                    Minimum = 0,
                    MaxLength = 5,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 36,
                    Width = 129,
                    FontSize = 24,
                    Text = supply.Quantity.ToString()
                };

                articleManualQuantityIntegerUpDown.PreviewTextInput += ManualQuantityIntegerUpDownPreviewTextInput;

                TextBox articleObservationsTextBox = new TextBox
                {
                    Foreground = Brushes.Black,
                    Margin = new Thickness(54, 0, 0, 0),
                    Width = 323,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 108,
                    Text = supply.Observations
                };

                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleRegisteredQuantityTextBlock);
                articleStackPanel.Children.Add(articleManualQuantityIntegerUpDown);
                articleStackPanel.Children.Add(articleObservationsTextBox);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }
        }
    }
}
