using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_AddUser.xaml
    /// </summary>
    public partial class GUI_AddUser : Page
    {
        private string route;
        private UserDAO userDAO;

        public GUI_AddUser()
        {
            InitializeComponent();
            InitalizeComboBox();
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

                if (new ImageManager().GetBitmapImageBytes(imageSource).Length <= 1048576)
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
            if(pnlForm.Visibility == Visibility.Visible)
            {
                btnRegister.Content = "Registrar";
                pnlForm.Visibility = Visibility.Hidden;
                pnlUser.Visibility = Visibility.Visible;
            }
            else
            {
                btnRegister.Content = "Siguiente";
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                                new AlertPopup("¡Error!", "Llene todos los campos", AlertPopupTypes.Error);
                }
                else
                {
                       userDAO = new UserDAO();
                        UserAccountSet account = new UserAccountSet()
                        {
                            UserName = txtEmail.Text,
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
                        EmployeePositionId = userDAO.GetEmployeePosition(cboUserRol.SelectedItem?.ToString()).Id,
                        Address_Id = 1
                        };
                        int result = userDAO.RegisterUser(account, employee);
                        if (result == 2)
                        {
                            new AlertPopup("¡Correcto!", "Usuario registrado con éxito", AlertPopupTypes.Success);
                            NavigationService.GoBack();
                        }
                        else
                        {
                                new AlertPopup("¡Error!", "El usuario no ha podido ser registrado con éxito", AlertPopupTypes.Error);
                        }
                }
            }

        
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
