using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Data.Entity.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_Login.xaml
    /// </summary>
    public partial class GUI_Login : Page
    {
        public GUI_Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoginValid())
            {
                UserAccountSet userAccount = new UserAccountSet
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text,
                };

                UserDAO userDAO = new UserDAO();

                try
                {
                    EmployeeSet employee = userDAO.CheckEmployeeExistencebyLogin(userAccount);
                    if (employee != null)
                    {
                        UserToken.GetInstance(employee);
                        NavigationService.Navigate(new GUI_ConsultCustomerOrder());
                    }
                    else
                    {
                        new AlertPopup("Usuario no encontrado", "El nombre de usuario y contraseña" +
                            " no coinciden, verifiquelos de nuevo por favor",
                            Auxiliary.AlertPopupTypes.Warning);
                    }
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la conexion a la base de datos, " +
                        "intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
                }
                catch (InvalidOperationException)
                {
                    new AlertPopup("Error con la base de datos",
                        "Lo siento, pero a ocurrido un error con la base de datos, " +
                        "verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
            }
        }

        private bool IsLoginValid()
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (txtUserName.Text.Length < 20 && txtPassword.Text.Length < 20)
                {
                    result = true;
                }
                else
                {
                    new AlertPopup("Nombres y contraseña largos",
                        "El nombre de usuario y la contraseña debe ser como maximo" +
                        " de 20 caracteres", Auxiliary.AlertPopupTypes.Warning);
                }
            }
            else
            {
                new AlertPopup("Campos nulos", "El campos de nombre de usuario y" +
                       " el de contraseña no debe ser nulo",
                       Auxiliary.AlertPopupTypes.Warning);
            }

            return result;
        }

        private void ViewPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pwPassword.Password = txtPassword.Text;
            pwPassword.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
            imgHidePasswordIcon.Visibility = Visibility.Visible;
            imgViewPasswordIcon.Visibility = Visibility.Collapsed;
        }

        private void HidePassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Visibility = Visibility.Visible;
            pwPassword.Visibility = Visibility.Collapsed;
            imgHidePasswordIcon.Visibility = Visibility.Collapsed;
            imgViewPasswordIcon.Visibility = Visibility.Visible;
        }

        private void MouseLeftButtonDown_RegisterNewUser(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new GUI_AddUser());
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = pwPassword.Password;
        }

        private void TxtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                pwPassword.Password = txtPassword.Text;
            }
        }

    }
}
