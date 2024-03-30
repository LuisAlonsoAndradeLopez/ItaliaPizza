using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
    /// Lógica de interacción para GUI_ReviewUsers.xaml
    /// </summary>
    public partial class GUI_ReviewUsers : Page
    {
        UserDAO userDAO = new UserDAO();
        public GUI_ReviewUsers()
        {
            InitializeComponent();
            InitalizeComboBoxes();
            GetAllUsers();
        }

        private void GoToAddUserWindows(object sender, RoutedEventArgs e)
        {
            GUI_AddUser VENTANA = new GUI_AddUser();
            this.NavigationService.Navigate(VENTANA);
        }

        private void ComboBox_TipeSelection(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GetAllUsers()
        {
            List<EmployeeSet> employees = new List<EmployeeSet>();
            try
            {
                employees = userDAO.GetAllEmployees();
                ShowAllUsers(employees);
            }
            catch (EntityException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ShowAllUsers(List<EmployeeSet> employees)
        {
            wpUsersRegistered.Children.Clear();
            ScrollViewer scroll = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stack = new StackPanel();

            foreach(var employee in employees)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 55,
                    Width = 857,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(23, 23, 33)),
                };
                grdContainer.Children.Add(rectBackground);

                Image image = new Image
                {
                    Height = 45,
                    Width = 45,
                    Source = new BitmapImage(new Uri("..\\Resources\\Pictures\\ICON-Domicilio.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-750, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblFullName = new Label
                {
                    Content = employee.Names + " " + employee.LastName + " " + employee.SecondLastName,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 20,
                    Margin = new Thickness(110, 0, 0, 0),
                };

                grdContainer.Children.Add(lblFullName);

                Label lblUserType = new Label
                {
                    Content = employee.UserStatusId,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 20,
                    Margin = new Thickness(326, 0, 0, 0),
                };
                grdContainer.Children.Add(lblUserType);

                Label lblUserStatus = new Label
                {
                    Content = employee.UserStatusId,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 20,
                    Margin = new Thickness(542, 0, 0, 0),
                };
                grdContainer.Children.Add(lblUserStatus);

            }
        }

        private void InitalizeComboBoxes()
        {
            List<string> status = new List<string>
            {
                "Activo",
                "Inactivo",
            };
            cboUserStatus.ItemsSource = status;

            List<string> type = new List<string>
            {
                "Administrador",
                "EmployeeSet",
            };
            cboUserType.ItemsSource = type;
        }

        private void ComboBox_StatusSelection(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
