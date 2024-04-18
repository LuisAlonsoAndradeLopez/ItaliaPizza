using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_ModifyUser.xaml
    /// </summary>
    public partial class GUI_ModifyUser : Page
    {
        private UserDAO userDAO;

        public GUI_ModifyUser(EmployeeSet employeeToModify, UserAccountSet userAccount)
        {
            InitializeComponent();
            InitalizeComboBox();
            userDAO = new UserDAO();
            FillFields(employeeToModify, userAccount);         
        }

        private void FillFields(EmployeeSet employeeToModify, UserAccountSet userAccount)
        {
            txtName.Text = employeeToModify.Names;
            txtLastName.Text = employeeToModify.LastName;
            txtSecondLastName.Text = employeeToModify.SecondLastName;
            txtEmail.Text = employeeToModify.Email;
            txtPhoneNumber.Text = employeeToModify.Phone;
            cboUserRol.SelectedItem = employeeToModify.EmployeePositionSet.Position;
            if (employeeToModify.ProfilePhoto != null) { 
            userImage.Source = userDAO.GetUserImage(employeeToModify.ProfilePhoto); }
            txtPassword.Text = userAccount.Password;
            txtUser.Text = userAccount.UserName;
            cboUserStatus.SelectedItem = employeeToModify.UserStatusSet.Status;
        }

        private void InitalizeComboBox()
        {
            List<string> types = new List<string>
            {
                "Gerente",
                "Recepcionista",
                "Mesero",
                "Personal Cocina"
            };
            cboUserRol.ItemsSource = types; ;
            cboUserRol.SelectedIndex = 0;

            List<string> status = new List<string>
            {
                "Activo",
                "Inactivo"
            };
            cboUserStatus.ItemsSource = status;
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
            if (pnlForm.Visibility == Visibility.Visible)
            {
                btnRegister.Content = "Registrar";
                pnlForm.Visibility = Visibility.Hidden;
                pnlUser.Visibility = Visibility.Visible;
            }
            else
            {
                btnRegister.Content = "Siguiente";
                if (!ValidateInputs())
                {
                    return;
                }
                else if (userImage.Source == null)
                {
                    new AlertPopup("¡Error!", "Necesita seleccionar una imagen para poder continuar", AlertPopupTypes.Error);
                }
                else
                {
                    userDAO = new UserDAO();
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
                        UserStatusId = userDAO.GetUserStatus(cboUserStatus.SelectedItem?.ToString()).Id,
                        EmployeePositionId = userDAO.GetEmployeePosition(cboUserRol.SelectedItem?.ToString()).Id,
                        Address_Id = 1
                    };
                    int result = userDAO.ModifyEmployee(account, employee);
                    if (result != -1)
                    {
                        new AlertPopup("¡Correcto!", "Usuario registrado con éxito", AlertPopupTypes.Success);
                        GUI_ReviewUsers VENTANA = new GUI_ReviewUsers();
                        this.NavigationService.Navigate(VENTANA);
                    }
                    else
                    {
                        new AlertPopup("¡Error!", "El usuario no ha podido ser registrado con éxito", AlertPopupTypes.Error);
                    }
                }
            }


        }

        private bool ValidateInputs()
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
    }
}
