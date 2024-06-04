using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using ItalianPizza.XAMLViews.Users;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_AddUser.xaml
    /// </summary>
    public partial class GUI_AddUser : Page
    {
        private UserDAO userDAO;
        private AddressSet address;

        public GUI_AddUser()
        {
            InitializeComponent();
            InitalizeComboBox();
            userDAO = new UserDAO();
        }

        private void InitalizeComboBox()
        {
            List<string> types = new List<string>
            {
                "Gerente",
                "Recepcionista",
                "Mesero",
                "Personal Cocina",
                "Repartidor"
            };
            cboUserRol.ItemsSource = types; ;
            cboUserRol.SelectedIndex = 0;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
            textBox.Text = string.Empty;
        }

        private void SelectImage_Clic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png)|*.png",
                Title = "Selecciona una imágen"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                BitmapImage imageSource = new BitmapImage(new Uri(openFileDialog.FileName));

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 10 * 1024 * 1024)
                {
                    userImage.Source = imageSource;
                }
                else
                {
                    new AlertPopup("¡Tamaño de imágen excedido!", "La imágen no debe pesar más de 1MB", AlertPopupTypes.Error);
                }
            }
        }


        private void RegisterButton_Clic(object sender, RoutedEventArgs e)
        {
            int recepcionistID = 5;
            int result;

            if (pnlForm.Visibility == Visibility.Visible)
            {
                btnRegister.Content = "Registrar";
                pnlForm.Visibility = Visibility.Hidden;
                pnlUser.Visibility = Visibility.Visible;
            }
            else
            {
                btnRegister.Content = "Siguiente";

                try
                {
                    int employeePositionId = userDAO.GetEmployeePosition(cboUserRol.SelectedItem?.ToString()).Id;
                    if (employeePositionId == recepcionistID)
                    {
                        if (!ValidateInputsForDeliveryDriver())
                        {
                            return;
                        }

                        DeliveryDriverSet deliveryDriver = new DeliveryDriverSet()
                        {
                            Names = txtName.Text,
                            LastName = txtLastName.Text,
                            SecondLastName = txtSecondLastName.Text,
                            Email = txtEmail.Text,
                            Phone = txtPhoneNumber.Text,
                            UserStatusId = 1,
                            EmployeeId = UserToken.GetEmployeeID(),
                        };

                        result = userDAO.AddDeliveryDriver(deliveryDriver);

                        if (result != -1)
                        {
                            new AlertPopup("¡Correcto!", "Usuario registrado con éxito", AlertPopupTypes.Success);
                            NavigationService.GoBack();
                        }
                    }
                    else
                    {
                        if (!ValidateInputsForEmployee())
                        {
                            return;
                        }

                        if (userImage.Source == null)
                        {
                            new AlertPopup("¡Error!", "Necesita seleccionar una imagen para poder continuar", AlertPopupTypes.Error);
                            return;
                        }

                        int addressId = userDAO.RegisterAddress(address);

                        UserAccountSet account = new UserAccountSet()
                        {
                            UserName = txtUser.Text,
                            Password = txtPassword.Text
                        };

                        EmployeeSet employee = new EmployeeSet()
                        {
                            Names = txtName.Text,
                            LastName = txtLastName.Text,
                            SecondLastName = txtSecondLastName.Text,
                            Email = txtEmail.Text,
                            Phone = txtPhoneNumber.Text,
                            ProfilePhoto = new ImageManager().GetBitmapImageBytes((BitmapImage)userImage.Source),
                            UserStatusId = 1,
                            EmployeePositionId = employeePositionId,
                            Address_Id = addressId
                        };

                        try
                        {
                            result = userDAO.RegisterUser(account, employee);

                            if (result == 3)
                            {
                                new AlertPopup("¡Correcto!", "Usuario registrado con éxito", AlertPopupTypes.Success);
                                NavigationService.GoBack();
                            }
                            else if (result == -1)
                            {
                                new AlertPopup("¡Error!", "El usuario no ha podido ser registrado con éxito", AlertPopupTypes.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            new AlertPopup("¡Error!", "No hay conexión en la base de datos", AlertPopupTypes.Error);
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    new AlertPopup("¡Error!", "No hay conexión en la base de datos", AlertPopupTypes.Error);
                    return;
                }

                
            }

        }

        private bool ValidateInputsForDeliveryDriver()
        {
            if (!RegexChecker.CheckEmail(txtEmail.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un correo electrónico válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckName(txtName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un nombre válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckLastName(txtLastName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un apellido válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckSecondLastName(txtSecondLastName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un segundo apellido válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckPhoneNumber(txtPhoneNumber.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un número de teléfono válido", AlertPopupTypes.Error);
                return false;
            }

            return true;
        }

        private bool ValidateInputsForEmployee()
        {
            
            if (txtPassword.Text != txtPasswordConfirmation.Text)
            {
                new AlertPopup("¡Error!", "Las contraseñas no coinciden", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckEmail(txtEmail.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un correo electrónico válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckPassword(txtPassword.Text))
            {
                new AlertPopup("¡Error!", "La contraseña no cumple con los requisitos. Debe contener al menos una letra minúscula, una letra mayúscula, un dígito y tener una longitud de entre 8 y 15 caracteres.", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckPassword(txtPasswordConfirmation.Text))
            {
                new AlertPopup("¡Error!", "La confirmación de la contraseña no cumple con los requisitos. Debe contener al menos una letra minúscula, una letra mayúscula, un dígito y tener una longitud de entre 8 y 15 caracteres.", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckName(txtName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un nombre válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckLastName(txtLastName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un apellido válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckSecondLastName(txtSecondLastName.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un segundo apellido válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckPhoneNumber(txtPhoneNumber.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un número de teléfono válido", AlertPopupTypes.Error);
                return false;
            }

            if (!RegexChecker.CheckUserName(txtUser.Text))
            {
                new AlertPopup("¡Error!", "Ingrese un usuario váido", AlertPopupTypes.Error);
                return false;
            }

            if (address == null)
            {
                new AlertPopup("¡Error!", "Tienes que ingresar una dirección para poder continuar", AlertPopupTypes.Error);
                return false;
            }

            try
            {
                if (userDAO.CheckUserExistence(new UserAccountSet()
                {
                    UserName = txtUser.Text
                }))
                {
                    new AlertPopup("¡Error!", "El correo electrónico ya está registrado", AlertPopupTypes.Error);
                    return false;
                }

                if (userDAO.CheckEmployeeExistence(new EmployeeSet
                {
                    Names = txtName.Text,
                    LastName = txtLastName.Text,
                    SecondLastName = txtSecondLastName.Text,
                }))
                {
                    new AlertPopup("¡Error!", "Ya existe un empleado registrado con esos nombres y apellidos", AlertPopupTypes.Error);
                    return false;
                }
                if (userDAO.CheckEmployeeEmailExistence(new EmployeeSet { Email = txtEmail.Text }))
                {
                    new AlertPopup("¡Error!", "Ya existe un empleado registrado con ese correo electrónico", AlertPopupTypes.Error);
                    return false;
                }
            }
            catch(Exception ex)
            {
                new AlertPopup("¡Error!", "No hay conexión en la base de datos", AlertPopupTypes.Error);
                return false;
            }            
            return true;
        }

        private void GoBack_Clic(object sender, RoutedEventArgs e)
        {
            if (pnlForm.Visibility == Visibility.Hidden)
            {
                btnRegister.Content = "Siguiente";
                pnlUser.Visibility = Visibility.Hidden;
                pnlForm.Visibility = Visibility.Visible;
            }
            else
            {
                System.Windows.Forms.DialogResult resultado = System.Windows.Forms.MessageBox.Show("¿Estás seguro de que deseas regresar?", "Confirmación", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information);
                if (resultado == System.Windows.Forms.DialogResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void cmbRolSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox userCmbBox = (System.Windows.Controls.ComboBox)sender;
            string selectedOption = userCmbBox.SelectedItem?.ToString();

            if(selectedOption == "Repartidor")
            {
                txtPassword.Visibility = Visibility.Hidden;
                txtPasswordConfirmation.Visibility = Visibility.Hidden;
                lblUser.Visibility = Visibility.Hidden;
                txtUser.Visibility = Visibility.Hidden;
                lblPassword.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                lblConfirmPassword.Visibility = Visibility.Hidden;
                txtPasswordConfirmation.Visibility = Visibility.Hidden;
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtPasswordConfirmation.Visibility = Visibility.Visible;
                lblUser.Visibility = Visibility.Visible;
                txtUser.Visibility = Visibility.Visible;
                lblPassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Visible;
                lblConfirmPassword.Visibility = Visibility.Visible;
                txtPasswordConfirmation.Visibility = Visibility.Visible;
            }
        }

        private void AddAddress_Clic(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UserAddress userAddress = new UserAddress();
            userAddress.RegisterAddress += UserAddress_RegisterAddress;
            userAddress.Cancelled += UserAddress_Cancelled;
            DynamicFormContainer.Children.Clear();
            DynamicFormContainer.Children.Add(userAddress);
            DynamicFormContainer.Visibility = Visibility.Visible;
        }

        private void UserAddress_Cancelled(object sender, EventArgs e)
        {
            DynamicFormContainer.Visibility = Visibility.Collapsed;
        }

        private void UserAddress_RegisterAddress(object sender, AddressSet address)
        {
            this.address = address;
            txtAddress.Text = address.StreetName + " " + address.StreetNumber + "...";
            DynamicFormContainer.Visibility = Visibility.Collapsed;
        }
    }
}
