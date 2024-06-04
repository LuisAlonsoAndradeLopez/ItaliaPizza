using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ItalianPizza.XAMLViews.Users
{
    /// <summary>
    /// Lógica de interacción para UserAddress.xaml
    /// </summary>
    public partial class UserAddress : UserControl
    {
        public event EventHandler<AddressSet> RegisterAddress;
        public event EventHandler Cancelled;
        public AddressSet Address;

        public UserAddress()
        {
            InitializeComponent();
        }

        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            Cancelled?.Invoke(this, EventArgs.Empty);

        }

        private void RegisterAddress_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = CheckAddressFields();

            if (errorMessages.Count > 0)
            {
                string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                new AlertPopup("Campos invalidos", "Los campos " + WrongFields
                    + " no deben ser nulos, ni debe tener caracteres especiales",
                    Auxiliary.AlertPopupTypes.Warning);
            }
            else
            {
                Address = new AddressSet()
                {
                    StreetName = txtStreet.Text,
                    StreetNumber = int.Parse(txtStreetNumber.Text),
                    Colony = txtColony.Text,
                    ZipCode = int.Parse(txtZipCode.Text),
                    City = txtCity.Text,
                    State = txtState.Text,
                    Township = txtState.Text,
                };

                RegisterAddress?.Invoke(this, Address);
            }
        }

        private List<string> CheckAddressFields()
        {
            List<string> errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(txtCity.Text.Trim()))
            {
                txtCity.BorderBrush = Brushes.Red;
                errorMessages.Add("'Ciudad'");
            }

            if (string.IsNullOrWhiteSpace(txtColony.Text.Trim()))
            {
                txtColony.BorderBrush = Brushes.Red;
                errorMessages.Add("'Colonia'");
            }

            if (string.IsNullOrWhiteSpace(txtState.Text.Trim()))
            {
                txtState.BorderBrush = Brushes.Red;
                errorMessages.Add("'Estado'");
            }

            if (string.IsNullOrWhiteSpace(txtStreet.Text.Trim()))
            {
                txtStreet.BorderBrush = Brushes.Red;
                errorMessages.Add("'Calle'");
            }

            if (string.IsNullOrWhiteSpace(txtStreetNumber.Text.Trim()))
            {
                txtStreetNumber.BorderBrush = Brushes.Red;
                errorMessages.Add("'Numero de Calle'");
            }

            if (string.IsNullOrWhiteSpace(txtZipCode.Text.Trim()))
            {
                txtZipCode.BorderBrush = Brushes.Red;
                errorMessages.Add("'Codigo Postal'");
            }

            return errorMessages;
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
