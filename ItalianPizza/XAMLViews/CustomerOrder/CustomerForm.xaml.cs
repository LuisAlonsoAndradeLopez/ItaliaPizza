using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;
using Panel = System.Windows.Controls.Panel;
using UserControl = System.Windows.Controls.UserControl;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : UserControl
    {
        private readonly UserDAO userDAO;
        private List<CustomerSet> customerList;
        private CustomerSet customerSet;
        public event EventHandler SelectCustomerEvent;

        public CustomerForm()
        {
            InitializeComponent();
            userDAO = new UserDAO();
            ShowCustomers();
            cboStatus.Items.Add("Activo");
            cboStatus.Items.Add("Inactivo");
        }

        private void ShowCustomers()
        {
            customerList = userDAO.GetAllCustomers();
            AddVisualCustomersToWindow(customerList);
        }
        public void AddVisualCustomersToWindow(List<CustomerSet> customerList)
        {
            wpCustomers.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            AddNewCustomerRegistrationbutton(stackPanelContainer);

            foreach (var customer in customerList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 55,
                    Width = 567,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(23, 32, 42)),
                };

                DropShadowEffect dropShadowEffect = new DropShadowEffect
                {
                    Color = Colors.Black,
                    Direction = 315,
                    ShadowDepth = 5,
                    Opacity = 0.5,
                };
                rectBackground.Effect = dropShadowEffect;
                grdContainer.Children.Add(rectBackground);

                Image imgIconCustomer = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Customer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-470, 0, 0, 0),
                };
                grdContainer.Children.Add(imgIconCustomer);

                Label lblFullnameCustomer = new Label
                {
                    Content = customer.Names + " " + customer.LastName + " " + customer.SecondLastName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(80, 10, 0, 0),
                };
                grdContainer.Children.Add(lblFullnameCustomer);

                Image imgSelectCustomer = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-SelectCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(400, 0, 0, 0),
                };
                imgSelectCustomer.PreviewMouseLeftButtonDown += (sender, e) => SelectCustomer_Click(sender, e, customer);
                grdContainer.Children.Add(imgSelectCustomer);

                Image imgEditCustomer = new Image
                {
                    Height = 30,
                    Width = 30,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-UpdateCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(500, 0, 0, 0),
                };
                imgEditCustomer.PreviewMouseLeftButtonDown += (sender, e) => ShowCustomerForm(customer);
                grdContainer.Children.Add(imgEditCustomer);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomers.Children.Add(scrollViewer);
        }
        private void AddNewCustomerRegistrationbutton(StackPanel stackPanel)
        {
            Grid grdButtonAdd = new Grid
            {
                Margin = new Thickness(0, 0, 0, 10),
            };

            Rectangle rectBackgroundButtonAdd = new Rectangle
            {
                Height = 55,
                Width = 567,
                RadiusX = 30,
                RadiusY = 30,
                Fill = new SolidColorBrush(Color.FromRgb(46, 64, 83)),
            };

            DropShadowEffect dropShadowEffectButton = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 315,
                ShadowDepth = 5,
                Opacity = 0.5,
            };

            rectBackgroundButtonAdd.Effect = dropShadowEffectButton;
            grdButtonAdd.Children.Add(rectBackgroundButtonAdd);

            Label lblTitleButton = new Label
            {
                Content = "Agregar nuevo cliente",
                Foreground = new SolidColorBrush(Color.FromRgb(253, 254, 254)),
                FontWeight = FontWeights.Bold,
                FontSize = 19,
                Margin = new Thickness(195, 10, 0, 0),
            };
            grdButtonAdd.Children.Add(lblTitleButton);

            Image iconButton = new Image
            {
                Height = 45,
                Width = 45,
                Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-AddCustomer.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill,
                Margin = new Thickness(-225, 0, 0, 0),
            };
            grdButtonAdd.Children.Add(iconButton);
            grdButtonAdd.PreviewMouseLeftButtonDown += (sender, e) => ShowCustomerForm(null);

            stackPanel.Children.Add(grdButtonAdd);
        }
        private void Click_CloseCustomerModule(object sender, MouseButtonEventArgs e)
        {
            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
               parentPanel.Children.Remove(this);
            }
        }
        private void ShowCustomerForm(CustomerSet customer)
        {
            grdCustomerModule.Visibility = Visibility.Hidden;
            grdCustomerForm.Visibility = Visibility.Visible;
            customerSet = customer;
            if (customer != null)
            {
                txtNames.Text = customer.Names;
                txtLastName.Text = customer.LastName;
                txtSecondLastName.Text = customer.SecondLastName;
                txtPhoneNumber.Text = customer.Phone;
                txtEmail.Text = customer.Email;
                txtCity.Text = customer.AddressSet.City;
                txtStreet.Text = customer.AddressSet.StreetName;
                txtStreetNumber.Text = customer.AddressSet.StreetNumber.ToString();
                txtZipCode.Text = customer.AddressSet.ZipCode.ToString();
                txtColony.Text = customer.AddressSet.Colony;
                txtState.Text = customer.AddressSet.State;

                btnRegisterCustomer.Visibility = Visibility.Hidden;
                btnUpdateCustomer.Visibility = Visibility.Visible;
            }
            else
            {
                btnRegisterCustomer.Visibility = Visibility.Visible;
                btnUpdateCustomer.Visibility = Visibility.Hidden;
            }
        }
        protected void SelectCustomer_Click(object sender, EventArgs e, CustomerSet customer)
        {
            customerSet = customer;
            SelectCustomerEvent?.Invoke(this, EventArgs.Empty);
        }
        public CustomerSet GetSelectCustomer()
        {
            return customerSet;
        }
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = CheckCustomerFields();
            errorMessages.AddRange(CheckAddressFields());

            if(errorMessages.Count == 0)
            {
                CustomerSet customerSet = GetDataCustomer();
                AddressSet customerAddress = GetClientAddress();

                try
                {
                    userDAO.AddCustomer(customerSet, customerAddress);
                    new AlertPopup("Registro Correcto", "El registro del nuevo cliente se realizo correctamente",
                        Auxiliary.AlertPopupTypes.Success);
                    ResetColorFields();
                    CleanFields();
                    grdCustomerModule.Visibility = Visibility.Visible;
                    grdCustomerForm.Visibility = Visibility.Hidden;
                    ShowCustomers();
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la conexion a la base de datos, " +
                        "intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
                }
                catch (Exception)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la base de datos, " +
                        "verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
            }
            else
            {
                string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                new AlertPopup("Campos invalidos", "Los campos " + WrongFields
                    + " no deben ser nulos, ni debe tener caracteres especiales",
                    Auxiliary.AlertPopupTypes.Warning);
            }
        }
        private void ModifyCustomer_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = CheckCustomerFields();
            errorMessages.AddRange(CheckAddressFields());

            if (errorMessages.Count == 0)
            {
                try
                {
                    CustomerSet customerSet = GetDataCustomer();
                    AddressSet customerAddress = GetClientAddress();
                    userDAO.UpdateCustomer(customerSet, customerAddress);
                    new AlertPopup("Registro Correcto", "El registro del nuevo cliente se realizo correctamente",
                        Auxiliary.AlertPopupTypes.Success);
                    ResetColorFields();
                    CleanFields();
                    grdCustomerModule.Visibility = Visibility.Visible;
                    grdCustomerForm.Visibility = Visibility.Hidden;
                    ShowCustomers();
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la conexion a la base de datos, " +
                        "intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
                }
                catch (Exception)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la base de datos, " +
                        "verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
                
            }
            else
            {
                string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                new AlertPopup("Campos invalidos", "Los campos " + WrongFields
                    + " no deben ser nulos, ni debe tener caracteres especiales",
                    Auxiliary.AlertPopupTypes.Warning);
            }
        }
        private CustomerSet GetDataCustomer()
        {
            CustomerSet customer = new CustomerSet
            {
                Names = txtNames.Text,
                LastName = txtLastName.Text,
                SecondLastName = txtSecondLastName.Text,
                Email = txtEmail.Text,
                Phone = txtPhoneNumber.Text,
                EmployeeId = UserToken.GetEmployeeID(),
            };

            if(customerSet != null)
            {
                customer.Id = customerSet.Id;
            }

            if(cboStatus.SelectedItem != "Activo")
            {
                customer.UserStatusId = 1;
            }
            else
            {
                customer.UserStatusId = 2;
            }

            return customer;
        }
        private AddressSet GetClientAddress()
        {
            AddressSet addressSet = new AddressSet
            {
                City = txtCity.Text,
                StreetNumber = int.Parse(txtStreetNumber.Text),
                StreetName = txtStreet.Text,
                State = txtState.Text,
                Colony = txtColony.Text,
                ZipCode = int.Parse(txtZipCode.Text),
                Township = txtColony.Text
            };

            if(customerSet != null)
            {
                addressSet.Id = customerSet.Address_Id;
            }

            return addressSet;
        }
        private List<string> CheckCustomerFields()
        {
            List<string> errorMessages = new List<string>();

            if (!RegexChecker.CheckName(txtNames.Text))
            {
                txtNames.BorderBrush = Brushes.Red;
                errorMessages.Add("'Nombres'");
            }

            if (!RegexChecker.CheckName(txtLastName.Text))
            {
                txtSecondLastName.BorderBrush = Brushes.Red;
                errorMessages.Add("'Apellido Paterno'");
            }

            if (!RegexChecker.CheckName(txtSecondLastName.Text))
            {
                txtSecondLastName.BorderBrush = Brushes.Red;
                errorMessages.Add("'Apellido Materno'");
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text.Trim()) && txtPhoneNumber.Text.Length == 10)
            {
                txtPhoneNumber.BorderBrush = Brushes.Red;
                errorMessages.Add("'Numero Telefonico'");
            }

            if (!RegexChecker.CheckEmail(txtEmail.Text))
            {
                txtEmail.BorderBrush = Brushes.Red;
                errorMessages.Add("'Correo'");
            }

            if (cboStatus.SelectedItem == null)
            {
                cboStatus.BorderBrush = Brushes.Red;
                errorMessages.Add("'Estatus'");
            }

            return errorMessages;
        }
        private void ResetColorFields()
        {
            txtNames.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969")); ;
            txtSecondLastName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtLastName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            cboStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtPhoneNumber.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtState.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtCity.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtColony.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtState.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtStreetNumber.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtZipCode.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtStreet.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
        }
        private void CleanFields()
        {
            txtNames.Text = string.Empty;
            txtSecondLastName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            cboStatus.SelectedIndex = -1;
            txtPhoneNumber.Text = string.Empty;
            txtState.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtColony.Text = string.Empty;
            txtState.Text = string.Empty;
            txtStreetNumber.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtStreet.Text = string.Empty;
        }
        private List<string> CheckAddressFields()
        {
            List<string> errorMessages = new List<string>();

            if (!RegexChecker.CheckName(txtCity.Text))
            {
                txtCity.BorderBrush = Brushes.Red;
                errorMessages.Add("'Ciudad'");
            }

            if (!RegexChecker.CheckName(txtColony.Text))
            {
                txtColony.BorderBrush = Brushes.Red;
                errorMessages.Add("'Colonia'");
            }

            if (!RegexChecker.CheckName(txtState.Text))
            {
                txtState.BorderBrush = Brushes.Red;
                errorMessages.Add("'Estado'");
            }

            if (!RegexChecker.CheckName(txtStreet.Text))
            {
                txtStreet.BorderBrush = Brushes.Red;
                errorMessages.Add("'Calle'");
            }

            if (string.IsNullOrWhiteSpace(txtStreetNumber.Text.Trim()) && txtStreetNumber.Text.Length == 3)
            {
                txtStreetNumber.BorderBrush = Brushes.Red;
                errorMessages.Add("'Numero de Calle'");
            }

            if (string.IsNullOrWhiteSpace(txtStreetNumber.Text.Trim()) && txtStreetNumber.Text.Length == 5)
            {
                txtZipCode.BorderBrush = Brushes.Red;
                errorMessages.Add("'Codigo Postal'");
            }

            return errorMessages;
        }
        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            grdCustomerForm.Visibility = Visibility.Hidden;
            grdCustomerModule.Visibility = Visibility.Visible;
            ResetColorFields();
            CleanFields();
            ShowCustomers();
        }
        private void TextBox_CustomerSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            FilterCustomer(textSearch);
        }
        private void FilterCustomer(string textSearch)
        {
            List<CustomerSet> filteredCustomers = customerList
            .Where(p =>
                p.Names.ToLower().Contains(textSearch.ToLower()) ||
                p.LastName.ToLower().Contains(textSearch.ToLower()) ||
                p.SecondLastName.ToLower().Contains(textSearch.ToLower())
            )
            .ToList();

            AddVisualCustomersToWindow(filteredCustomers);
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
