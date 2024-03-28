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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                "Administrador",
                "Empleado",
                "Cocinero",
                "Repartidor",
                "Recepcionista",
            };
            cboStatusCustomerOrders.ItemsSource = types; ;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
        }

        private void SelectImage_Clic(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ventanaSeleccionDeImagen = new OpenFileDialog
                {
                    Title = "Seleccione una imagen de jugador",
                    Filter = "Todos los formatos permitidos|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
                };
                if (ventanaSeleccionDeImagen.ShowDialog() == true)
                {
                    userImage.Source = new BitmapImage(new Uri(ventanaSeleccionDeImagen.FileName));
                    route = ventanaSeleccionDeImagen.FileName;
                }
        }

        private byte[] ConvertImageToBytes()
        {
            MemoryStream ms = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            BitmapImage imagenFinal = new BitmapImage();
            imagenFinal.BeginInit();
            imagenFinal.UriSource = new Uri(route);
            imagenFinal.DecodePixelHeight = 150;
            imagenFinal.DecodePixelWidth = 150;
            imagenFinal.EndInit();
            encoder.Frames.Add(BitmapFrame.Create(imagenFinal));
            encoder.Save(ms);
            return ms.ToArray();
        }

        private void RegisterButton_Clic(object sender, RoutedEventArgs e)
        {
            if(pnlForm.Visibility == Visibility.Visible)
            {
                pnlForm.Visibility = Visibility.Hidden;
                pnlUser.Visibility = Visibility.Visible;
            }
            else
            {
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Por favor, llene todos los campos.");
                }
                else
                {
                       userDAO = new UserDAO();
                        Cuenta account = new Cuenta()
                        {
                            Usuario = txtEmail.Text,
                            Contraseña = txtPassword.Text
                        };
                        Empleado employee = new Empleado()
                        {
                            Id = RandomNumberGenerator.GenerateRandomNumber(1000, 9999),
                            Nombres = txtName.Text,
                            ApellidoPaterno = txtLastName.Text,
                            ApellidoMaterno = txtSecondLastName.Text,
                            Telefono = txtPhoneNumber.Text,
                            DatosImagen = ConvertImageToBytes()
                        };
                        int result = userDAO.RegisterUser(account, employee);
                        if (result == 1)
                        {
                            MessageBox.Show("Usuario registrado con éxito.");
                            NavigationService.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el usuario.");
                        }
                }
            }
        }

        private void GoBack_Clic(object sender, RoutedEventArgs e)
        {
            if (pnlForm.Visibility == Visibility.Hidden)
            {
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
