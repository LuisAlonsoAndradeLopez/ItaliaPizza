using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.Resources;
using ItalianPizza.XAMLViews.Articles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Border = System.Windows.Controls.Border;
using Orientation = System.Windows.Controls.Orientation;

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

            //ShowArticles("");
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
                InventoryValidationSet inventoryValidation = new InventoryValidationSet
                {
                    InventoryValidationDate = DateTime.Now,
                    Description = InventoryValidationTextBox.Text,
                    EmployeeId = 2
                };

                new InventoryValidationDAO().AddInventoryValidation(inventoryValidation);

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
                    //Source = new BitmapImage(new Uri(Convert.ToBase64String(product.Picture), UriKind.RelativeOrAbsolute)),
                    //Stretch = Stretch.Fill
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

                TextBlock articleStatusTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = new ProductStatusDAO().GetProductStatusById(product.ProductStatusId).Status
                    
                };

                TextBlock articleQuantityTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = product.Quantity.ToString()
                };


                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleStatusTextBlock);
                articleStackPanel.Children.Add(articleQuantityTextBlock);

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

                TextBlock articleStatusTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = new ProductStatusDAO().GetProductStatusById(supply.ProductStatusId).Status
                };

                TextBlock articleQuantityTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(6, 0, 0, 0),
                    Width = 144,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = supply.Quantity.ToString()
                };


                articleStackPanel.Children.Add(articleImage);
                articleStackPanel.Children.Add(articleNameTextBlock);
                articleStackPanel.Children.Add(articleTypeTextBlock);
                articleStackPanel.Children.Add(articleStatusTextBlock);
                articleStackPanel.Children.Add(articleQuantityTextBlock);

                articleBorder.Child = articleStackPanel;

                ArticlesStackPanel.Children.Add(articleBorder);
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
