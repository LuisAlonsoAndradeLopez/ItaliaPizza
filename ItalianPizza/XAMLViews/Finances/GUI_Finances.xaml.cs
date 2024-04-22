using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ItalianPizza.XAMLViews.Finances
{
    /// <summary>
    /// Lógica de interacción para GUI_Finances.xaml
    /// </summary>
    public partial class GUI_Finances : Page
    {
        //TODO:

        //Try-catch a todos los botonazos de todas las ventanas

        List<FinancialTransactionSet> financialTransactions;

        public GUI_Finances()
        {
            financialTransactions = new FinancialTransactionDAO().GetFinancialTransactions();

            InitializeComponent();
            ShowFinancialTransactions("", "");
        }

        private void TransactionTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowFinancialTransactions(TransactionTypeComboBox.Text, RealizationDatePicker.Text);
        }

        private void RealizationDatePickerSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowFinancialTransactions(TransactionTypeComboBox.Text, RealizationDatePicker.Text);
        }

        private void EraseSelectedDateButtonOnClick(object sender, RoutedEventArgs e)
        {
            RealizationDatePicker.Text = "";
        }

        private void AddFinancialTransactionButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddFinancialTransactionButton.IsEnabled = false;
            AddFinancialTransactionBorder.Visibility = Visibility.Visible;
        }

        private void MakeDailyBalanceButtonOnClick(object sender, RoutedEventArgs e)
        {
            new AlertPopup("¡No disponible!", "En desarrollo de software.", AlertPopupTypes.Error);

            //Método que hace el Álvaro
        }

        private void SaveTransactionButtonOnClick(object sender, RoutedEventArgs e)
        {
            string financialTransactionType;

            if (FinancialTransactionTypeComboBox.Text != null)
            {
                financialTransactionType = FinancialTransactionTypeComboBox.Text;
            }
            else
            {
                financialTransactionType = "";
            }

            double financialTransactionMonetaryValue;

            if (FinancialTransactionPriceDecimalUpDown.Value != null)
            {
                financialTransactionMonetaryValue = (double)FinancialTransactionPriceDecimalUpDown.Value;
            }
            else
            {
                financialTransactionMonetaryValue = 0;
            }

            FinancialTransactionSet financialTransaction = new FinancialTransactionSet
            {
                Type = financialTransactionType,
                Description = FinancialTransactionDescriptionTextBox.Text,
                FinancialTransactionDate = DateTime.Now,
                EmployeeId = 2,
                MonetaryValue = financialTransactionMonetaryValue,
                Context = ContextTextBox.Text
            };

            new FinancialTransactionDAO().AddFinancialTransaction(financialTransaction);

            new AlertPopup("¡Transacción exitosa!", "Transacción financiera registrada con éxito.", AlertPopupTypes.Success);

            AddFinancialTransactionBorder.Visibility = Visibility.Collapsed;
            AddFinancialTransactionButton.IsEnabled = true;
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddFinancialTransactionBorder.Visibility = Visibility.Collapsed;
            AddFinancialTransactionButton.IsEnabled = true;
        }

        private void ShowFinancialTransactions(string financialTransactionType, string realizationDate)
        {
            List<FinancialTransactionSet> selectedFinancialTransactions = financialTransactions.Where(ft => ft.Type == financialTransactionType)
                    .Where(ft => ft.FinancialTransactionDate == DateTime.Parse(realizationDate)).ToList();

            while(FinancialTransactionsStackPanel.Children.Count > 1)
            {
                FinancialTransactionsStackPanel.Children.RemoveAt(0);
            }

            foreach (var financialTransaction in selectedFinancialTransactions)
            {
                Border financialTransactionBorder = new Border
                {
                    Height = 73.7,
                    Margin = new Thickness(5, 4, 5, 0),
                    Background = new SolidColorBrush(Color.FromRgb(0x7E, 0x16, 0x16))
                };

                StackPanel financialTransactionStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };

                TextBlock financialTransactionTypeTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(0),
                    Width = 146,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = financialTransaction.Type
                };

                TextBlock financialTransactionContextTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(0),
                    Width = 126,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = financialTransaction.Context
                };

                TextBlock financialTransactionRealizationDateTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(0),
                    Width = 138,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = financialTransaction.FinancialTransactionDate.ToString()
                };

                TextBlock financialTransactionPriceTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(0),
                    Width = 152,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = financialTransaction.MonetaryValue.ToString()
                };

                TextBlock financialTransactionDescriptionTextBlock = new TextBlock
                {
                    Foreground = Brushes.White,
                    Margin = new Thickness(0),
                    Width = 821,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 18,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = financialTransaction.Description
                };

                financialTransactionStackPanel.Children.Add(financialTransactionTypeTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionContextTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionRealizationDateTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionPriceTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionDescriptionTextBlock);
                financialTransactionBorder.Child = financialTransactionStackPanel;

                int indexToInsert = FinancialTransactionsStackPanel.Children.Count - 1;
                FinancialTransactionsStackPanel.Children.Insert(indexToInsert, financialTransactionBorder);
            }
        }
    }
}
