using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.IO;
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
        List<FinancialTransactionSet> financialTransactions;

        public GUI_Finances()
        {
            financialTransactions = new FinancialTransactionDAO().GetFinancialTransactions();

            InitializeComponent();

            string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
            RealizationDatePicker.Text = formattedDate;

            InitializeComboboxes();
        }

        private void TransactionTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ShowFinancialTransactions(TransactionTypeComboBox.SelectedItem?.ToString(), RealizationDatePicker.SelectedDate.Value);
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void RealizationDatePickerSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ShowFinancialTransactions(TransactionTypeComboBox.SelectedItem?.ToString(), RealizationDatePicker.SelectedDate.Value);
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void FinancialTransactionTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox financialTransactionTypesComboBox = (ComboBox)sender;
                string selectedOption = FinancialTransactionTypeComboBox.SelectedItem?.ToString();

                FinancialTransactionContextComboBox.Items.Clear();

                List<FinancialTransactionIncomeContextSet> financialTransactionIncomeContexts = new FinancialTransactionIncomeContextDAO().GetAllFinancialTransactionIncomeContexts();
                List<FinancialTransactionWithDrawContextSet> financialTransactionWithDrawContexts = new FinancialTransactionWithDrawContextDAO().GetAllFinancialTransactionWithDrawContexts();


                if (selectedOption == FinancialTransactionTypes.Entrada.ToString())
                {
                    foreach (var financialTransactionIncomeContext in financialTransactionIncomeContexts)
                    {
                        FinancialTransactionContextComboBox.Items.Add(financialTransactionIncomeContext.Context);
                    }
                }

                if (selectedOption == FinancialTransactionTypes.Salida.ToString())
                {
                    foreach (var financialTransactionWithDrawContext in financialTransactionWithDrawContexts)
                    {
                        FinancialTransactionContextComboBox.Items.Add(financialTransactionWithDrawContext.Context);
                    }
                }

                if (FinancialTransactionContextComboBox.Items.Count > 0)
                {
                    FinancialTransactionContextComboBox.SelectedItem = FinancialTransactionContextComboBox.Items[0];
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void AddFinancialTransactionButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddFinancialTransactionButton.IsEnabled = false;
            AddFinancialTransactionBorder.Visibility = Visibility.Visible;
        }

        private void MakeDailyBalanceButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                new AlertPopup("¡No disponible!", "En desarrollo de software con el Álvaro.", AlertPopupTypes.Error);
                //Método que hace el Álvaro

            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void SaveTransactionButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FinancialTransactionContextComboBox.Items.Count > 0)
                {
                    if (FinancialTransactionDescriptionTextBox.Text != "")
                    {
                        string financialTransactionType;
                        string financialTransactionContext;
                        double financialTransactionMonetaryValue;

                        if (FinancialTransactionTypeComboBox.SelectedItem?.ToString() != null)
                        {
                            financialTransactionType = FinancialTransactionTypeComboBox.SelectedItem?.ToString();
                        }
                        else
                        {
                            financialTransactionType = "";
                        }

                        if (FinancialTransactionContextComboBox.SelectedItem?.ToString() != null)
                        {
                            financialTransactionContext = FinancialTransactionContextComboBox.SelectedItem?.ToString();
                        }
                        else
                        {
                            financialTransactionContext = "";
                        }

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
                            EmployeeId = UserToken.GetEmployeeID(),
                            MonetaryValue = financialTransactionMonetaryValue
                        };

                        if (financialTransactionType == FinancialTransactionTypes.Entrada.ToString())
                        {
                            financialTransaction.IncomeContextId = new FinancialTransactionIncomeContextDAO().GetFinancialTransactionIncomeContextByName(financialTransactionContext).Id;
                        }

                        if (financialTransactionType == FinancialTransactionTypes.Salida.ToString())
                        {
                            financialTransaction.WithDrawContextId = new FinancialTransactionWithDrawContextDAO().GetFinancialTransactionWithDrawContextByName(financialTransactionContext).Id;
                        }

                        new FinancialTransactionDAO().AddFinancialTransaction(financialTransaction);

                        new AlertPopup("¡Transacción exitosa!", "Transacción financiera registrada con éxito.", AlertPopupTypes.Success);

                        AddFinancialTransactionBorder.Visibility = Visibility.Collapsed;
                        AddFinancialTransactionButton.IsEnabled = true;

                        financialTransactions = new FinancialTransactionDAO().GetFinancialTransactions();
                        ShowFinancialTransactions(TransactionTypeComboBox.SelectedItem?.ToString(), RealizationDatePicker.SelectedDate.Value);
                    }
                    else
                    {
                        new AlertPopup("¡Falta la descripción!", "Falta que introduzcas la descripción de la transacción financiera a realizar", AlertPopupTypes.Error);
                    }
                }
                else
                {
                    new AlertPopup("¡No se puede crear la transacción financiera!", "No se puede seleccionar un contexto para la transacción. Se tiene que agregar contextos en la base de datos.", AlertPopupTypes.Error);
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

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddFinancialTransactionBorder.Visibility = Visibility.Collapsed;
            AddFinancialTransactionButton.IsEnabled = true;
        }

        private void ShowFinancialTransactions(string financialTransactionType, DateTime realizationDate)
        {
            DateTime startDate = realizationDate.Date;
            DateTime endDate = startDate.AddDays(1);

            List<FinancialTransactionSet> selectedFinancialTransactions = financialTransactions
                .Where(ft => ft.Type == financialTransactionType &&
                             ft.FinancialTransactionDate >= startDate && ft.FinancialTransactionDate <= endDate)
                .ToList();

            while (FinancialTransactionsStackPanel.Children.Count > 1)
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
                    VerticalAlignment = VerticalAlignment.Center
                };

                if (financialTransaction.Type == FinancialTransactionTypes.Entrada.ToString())
                {
                    financialTransactionContextTextBlock.Text = new FinancialTransactionIncomeContextDAO().GetFinancialTransactionIncomeContextById(financialTransaction.IncomeContextId ?? 0).Context;
                }

                if (financialTransaction.Type == FinancialTransactionTypes.Salida.ToString())
                {
                    financialTransactionContextTextBlock.Text = new FinancialTransactionWithDrawContextDAO().GetFinancialTransactionWithDrawContextById(financialTransaction.WithDrawContextId ?? 0).Context;
                }

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
                    Text = "$" + financialTransaction.MonetaryValue.ToString()
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

        private void InitializeComboboxes()
        {
            string[] financialTransactionTypes = { FinancialTransactionTypes.Entrada.ToString(), FinancialTransactionTypes.Salida.ToString() };

            foreach (var financialTransactionType in financialTransactionTypes)
            {
                TransactionTypeComboBox.Items.Add(financialTransactionType);
                FinancialTransactionTypeComboBox.Items.Add(financialTransactionType);
            }

            TransactionTypeComboBox.SelectedItem = TransactionTypeComboBox.Items[0];
            FinancialTransactionTypeComboBox.SelectedItem = FinancialTransactionTypeComboBox.Items[0];
        }
    }
}
