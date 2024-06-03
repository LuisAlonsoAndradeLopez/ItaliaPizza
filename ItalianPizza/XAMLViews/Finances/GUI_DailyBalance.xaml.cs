using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPizza.XAMLViews.Finances
{
    /// <summary>
    /// Lógica de interacción para GUI_DailyBalance.xaml
    /// </summary>
    public partial class GUI_DailyBalance : Page
    {
        private List<IncomeFinancialTransactionSet> incomeFinancialTransactions;
        private List<WithDrawFinancialTransactionSet> withDrawFinancialTransactions;
        DailyClosingSet dailyClosing;

        public GUI_DailyBalance()
        {
            InitializeComponent();
            GetIncomesAndWithDrawsOfCurrentDailyBalance();
            FillOutBalances();
            ShowFinancialTransactions(incomeFinancialTransactions,withDrawFinancialTransactions);
        }

        private void FillOutBalances()
        {
            double totalIncome = 0;
            double totalWithDraw = 0;
            DailyClosingDAO dailyClosingDAO = new DailyClosingDAO();
            DailyClosingSet dailyClosing = dailyClosingDAO.GetCurrentDailyClosing();
            
            lblInitialBalance.Content = "$" + dailyClosing.InitialBalance.ToString();
            
            foreach (var incomeFinancialTransaction in incomeFinancialTransactions)
            {
                totalIncome += incomeFinancialTransaction.MonetaryValue;
            }

            foreach (var withDrawFinancialTransaction in withDrawFinancialTransactions)
            {
                totalWithDraw += withDrawFinancialTransaction.MonetaryValue;
            }

            lblIncomes.Content = "$" + totalIncome.ToString();
            lblOutcomes.Content = "$" + totalWithDraw.ToString();

            lblFinalBalance.Content = "$" + (dailyClosing.InitialBalance + totalIncome - totalWithDraw).ToString();



        }

        private void GetIncomesAndWithDrawsOfCurrentDailyBalance()
        {
            incomeFinancialTransactions = new DailyClosingDAO().GetDailyIncomes();
            withDrawFinancialTransactions = new DailyClosingDAO().GetDailyWithdrawals();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void ShowFinancialTransactions(List<IncomeFinancialTransactionSet> dailyIncomes, List<WithDrawFinancialTransactionSet> dailyWithdrawals)
        {
            FinancialTransactionsStackPanel.Children.Clear();


            foreach ( var incomeFinancialTransaction in dailyIncomes)
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
                    Text = FinancialTransactionTypes.Entrada.ToString()
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
                    Text = new FinancialTransactionIncomeContextDAO().GetFinancialTransactionIncomeContextById(incomeFinancialTransaction.FinancialTransactionIncomeContextId ?? 0).Context
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
                    Text = incomeFinancialTransaction.RealizationDate.ToString()
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
                    Text = "$" + incomeFinancialTransaction.MonetaryValue.ToString()
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
                    Text = incomeFinancialTransaction.Description
                };

                financialTransactionStackPanel.Children.Add(financialTransactionTypeTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionContextTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionRealizationDateTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionPriceTextBlock);
                financialTransactionStackPanel.Children.Add(financialTransactionDescriptionTextBlock);
                financialTransactionBorder.Child = financialTransactionStackPanel;

                FinancialTransactionsStackPanel.Children.Add(financialTransactionBorder);
            }

            foreach (var withDrawFinancialTransaction in dailyWithdrawals)
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
                    Text = FinancialTransactionTypes.Salida.ToString()
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
                    Text = new FinancialTransactionWithDrawContextDAO().GetFinancialTransactionWithDrawContextById(withDrawFinancialTransaction.FinancialTransactionWithDrawContextId ?? 0).Context
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
                    Text = withDrawFinancialTransaction.RealizationDate.ToString()
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
                    Text = "$" + withDrawFinancialTransaction.MonetaryValue.ToString()
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
                    Text = withDrawFinancialTransaction.Description
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

        private void AddCashBalance_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CashBalanceForm cashBalanceForm = new CashBalanceForm();
            cashBalanceForm.BalanceCalculated += OnBalanceCalculated;
            cashBalanceForm.Cancelled += Form_Cancelled;
            DynamicFormContainer.Children.Clear();
            DynamicFormContainer.Children.Add(cashBalanceForm);
            DynamicFormContainer.Visibility = Visibility.Visible;
        }

        private void OnBalanceCalculated(object sender, double balance)
        {
            CashBalance.Text = "$" + balance.ToString();
            DynamicFormContainer.Visibility = Visibility.Collapsed;
        }

        private void RegisterDailyBalance_Clic(object sender, RoutedEventArgs e)
        {
            DailyClosingDAO dailyClosingDAO = new DailyClosingDAO();

            if(CashBalance.Text == "")
            {
                new AlertPopup("No se ha ingresado el saldo en caja", "Por favor, ingresa el saldo en caja dando clic en el ícono de billetes", AlertPopupTypes.Warning);
                return;
            }else if(NewCashBalance.Text == "")
            {
                new AlertPopup("No se ha ingresado el saldo inicial", "Por favor, ingresa el saldo inicial", AlertPopupTypes.Warning);
                return;
            }
            else if(CashBalance.Text != lblFinalBalance.Content.ToString())
            {
                dailyClosing = CreateDailyClosing();
                if (new AlertPopup("El balance calculado no coincide con el balance final", "Necesitas añadir una justificación del porqué no coincide esta cantidad. ¿Deseas continuar?", AlertPopupTypes.Decision).GetDecisionOfDecisionPopup())
                {
                    DailyBalanceJustification dailyBalanceJustification = new DailyBalanceJustification();
                    dailyBalanceJustification.JustificationRegistered += OnJustificationRegistered;
                    dailyBalanceJustification.Cancelled += Form_Cancelled;
                    DynamicFormContainerForJustification.Children.Clear();
                    DynamicFormContainerForJustification.Children.Add(dailyBalanceJustification);
                    DynamicFormContainerForJustification.Visibility = Visibility.Visible;
                }
                return;
            }
            else
            {
                dailyClosing = CreateDailyClosing();
                RegisterDailyBalance(dailyClosing);
            }
        }

        private DailyClosingSet CreateDailyClosing()
        {
            char[] charsToTrim = { '$' };

            DailyClosingSet dailyClosing = new DailyClosingSet
            {
                ClosingDate = DateTime.Now,
                Description = "Todo bien con el cierre.",
                EmployeeId = UserToken.GetEmployeeID(),
                BalanceIncome = double.Parse(lblIncomes.Content.ToString().Trim(charsToTrim)),
                BalanceWithdrawal = double.Parse(lblOutcomes.Content.ToString().Trim(charsToTrim)),
                FinalBalance = double.Parse(CashBalance.Text.Trim(charsToTrim)),
                InitialBalance = double.Parse(NewCashBalance.Text),
            };

            return dailyClosing;
        }

        private void OnJustificationRegistered(object sender, string justification)
        {
            dailyClosing.Description = justification;
            RegisterDailyBalance(dailyClosing);
        }

        private void Form_Cancelled(object sender, EventArgs e)
        {
            DynamicFormContainer.Visibility = Visibility.Collapsed;
            DynamicFormContainerForJustification.Visibility = Visibility.Collapsed;
        }



        private void RegisterDailyBalance(DailyClosingSet dailyClosing)
        {
            DailyClosingDAO dailyClosingDAO = new DailyClosingDAO();
            dailyClosingDAO.RegisterDailyClosing(dailyClosing);
            new AlertPopup("Cierre diario registrado con éxito", "El cierre diario ha sido registrado con éxito", AlertPopupTypes.Success);
            NavigationService.Navigate(new GUI_DailyBalance());
        }
    }
}
