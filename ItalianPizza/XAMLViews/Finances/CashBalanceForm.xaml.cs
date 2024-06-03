using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
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
    /// Lógica de interacción para CashBalanceForm.xaml
    /// </summary>
    public partial class CashBalanceForm : UserControl
    {

        public event EventHandler<double> BalanceCalculated;
        public event EventHandler Cancelled;

        public CashBalanceForm()
        {
            InitializeComponent();
        }

        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
                Cancelled?.Invoke(this, EventArgs.Empty);

        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            SetTextBoxTo0IfEmpty();
            double cashBalance = CalculateCashBalance();

            if (new AlertPopup("El total es de: " + cashBalance, "¿Deseas continuar?", AlertPopupTypes.Decision).GetDecisionOfDecisionPopup())
            {
                BalanceCalculated?.Invoke(this, cashBalance);

            }

        }

        private double CalculateCashBalance()
        {
            double cashBalance = 0;

            double bill500 = Convert.ToDouble(txt500bill.Text);
            double bill200 = Convert.ToDouble(txt200bill.Text);
            double bill100 = Convert.ToDouble(txt100bill.Text);
            double bill50 = Convert.ToDouble(txt50bill.Text);
            double bill20 = Convert.ToDouble(txt20bill.Text);
            double coin10 = Convert.ToDouble(txt10coin.Text);
            double coin5 = Convert.ToDouble(txt5coin.Text);
            double coin2 = Convert.ToDouble(txt2coin.Text);
            double coin1 = Convert.ToDouble(txt1coin.Text);
            double coin05 = Convert.ToDouble(txt50centscoin.Text);

            cashBalance = bill500 * 500 + bill200 * 200 + bill100 * 100 + bill50 * 50 + bill20 * 20 + coin10 * 10 + coin5 * 5 + coin2 * 2 + coin1 + coin05 * 0.5;

            return cashBalance;
        }

        private void SetTextBoxTo0IfEmpty()
        {
            if (txt500bill.Text == "")
            {
                txt500bill.Text = "0";
            }
            if (txt200bill.Text == "")
            {
                txt200bill.Text = "0";
            }
            if (txt100bill.Text == "")
            {
                txt100bill.Text = "0";
            }
            if (txt50bill.Text == "")
            {
                txt50bill.Text = "0";
            }
            if (txt20bill.Text == "")
            {
                txt20bill.Text = "0";
            }
            if (txt10coin.Text == "")
            {
                txt10coin.Text = "0";
            }
            if (txt5coin.Text == "")
            {
                txt5coin.Text = "0";
            }
            if (txt2coin.Text == "")
            {
                txt2coin.Text = "0";
            }
            if (txt1coin.Text == "")
            {
                txt1coin.Text = "0";
            }
            if (txt50centscoin.Text == "")
            {
                txt50centscoin.Text = "0";
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
