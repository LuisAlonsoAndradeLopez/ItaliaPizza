using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_ReviewUsers.xaml
    /// </summary>
    public partial class GUI_ReviewUsers : Page
    {
        UserDAO userDAO = new UserDAO();
        private List<EmployeeSet> employees;
        private EmployeeSet employeeSelected;
        UserAccountSet userAccountSelected;

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

        private void UserButtonOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Border border)
                {
                    EmployeeSet employee = (EmployeeSet)border.Tag;
                    employeeSelected = employee;
                    userAccountSelected = userDAO.GetUserAccountByEmployeeID(employee.Id);
                    grdVirtualWindowSelectUserAlert.Visibility = Visibility.Hidden;
                    grdDetailsUser.Visibility = Visibility.Visible;
                    ShowUserDetails(employee);
                }
            }
            catch (EntityException ex)
            {
                new AlertPopup("¡Ocurrió un problema!", "Comuniquese con los desarrolladores para solucionar el problema", AlertPopupTypes.Error);
                new ExceptionLogger().LogException(ex);
            }
        }

        private void ShowUserDetails(EmployeeSet employee)
        {
            txtFullName.Text = employee.Names + " " + employee.LastName + " " + employee.SecondLastName;
            txtEmail.Text = employee.Email;
            txtPhoneNumber.Text = employee.Phone;
            txtRol.Text = employee.EmployeePositionSet.Position;
            txtState.Text = employee.UserStatusSet.Status;
            txtAddress.Text = userDAO.GetUserAddressByEmployeeID(employee.Id);
            if (employee.ProfilePhoto != null)
                imgUser.Source = userDAO.GetUserImage(employee.ProfilePhoto);
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
                new AlertPopup("Error de conexión", "Error al acceder a la base de datos.", AlertPopupTypes.Error);
            }

        }

        private void ShowAllUsers(List<EmployeeSet> employees)
        {
            wpUsersRegistered.Children.Clear();


            foreach (var employee in employees)
            {
                Border userBorder = new Border
                {
                    Cursor = Cursors.Hand,
                    Height = 142,
                    Width = 835,
                    Margin = new Thickness(5, 4, 5, 0),
                    CornerRadius = new CornerRadius(10),
                    Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x33)),
                    Tag = employee,
                };

                userBorder.MouseLeftButtonDown += UserButtonOnClick;

                StackPanel usersStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };


                Image userImage = new Image
                {
                    Width = 74,
                    Height = 100,
                    Margin = new Thickness(40, 0, 0, 0),
                    Source = new UserDAO().GetUserImage(employee.ProfilePhoto),
                };

                TextBlock lblFullName = new TextBlock
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(20, 0, 0, 0),
                    Width = 337,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = employee.Names + " " + employee.LastName + " " + employee.SecondLastName,
                };

                TextBlock lblUserType = new TextBlock
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 184,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = employee.EmployeePositionSet.Position,
                };

                TextBlock lblUserStatus = new TextBlock
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    Margin = new Thickness(30, 0, 0, 0),
                    Width = 113,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 25,
                    TextAlignment = TextAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = employee.UserStatusSet.Status,
                };


                usersStackPanel.Children.Add(userImage);
                usersStackPanel.Children.Add(lblFullName);
                usersStackPanel.Children.Add(lblUserType);
                usersStackPanel.Children.Add(lblUserStatus);

                userBorder.Child = usersStackPanel;

                wpUsersRegistered.Children.Add(userBorder);



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
                "Gerente",
                "Recepcionista",
                "Mesero",
                "Personal Cocina"
            };
            cboUserType.ItemsSource = type;
        }

        private void ComboBox_StatusSelection(object sender, SelectionChangedEventArgs e)
        {
            string status = cboUserStatus.SelectedItem.ToString();
            employees = userDAO.GetAllEmployeesByStatus(status);
            if (employees.Count == 0)
            {
                new AlertPopup("No se encontraron usuarios", "No se encontraron usuarios con el estado seleccionado", AlertPopupTypes.Warning);
            }
            else
            {
                ShowAllUsers(employees);
            }
        }

        private void ComboBox_TipeSelection(object sender, SelectionChangedEventArgs e)
        {
            string position = cboUserType.SelectedItem.ToString();
            employees = userDAO.GetAllEmployeesByPosition(position);
            if (employees.Count == 0)
            {
                new AlertPopup("No se encontraron usuarios", "No se encontraron usuarios con el estado seleccionado", AlertPopupTypes.Warning);
            }
            else
            {
                ShowAllUsers(employees);
            }
        }

        private void GoToModifyUserWindows(object sender, RoutedEventArgs e)
        {
            GUI_ModifyUser VENTANA = new GUI_ModifyUser(employeeSelected, userAccountSelected);
            this.NavigationService.Navigate(VENTANA);
        }
    }
}
