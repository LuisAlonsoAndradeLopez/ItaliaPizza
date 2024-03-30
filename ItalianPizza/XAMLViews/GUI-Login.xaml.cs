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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            UserDAO userDAO = new UserDAO();
            char[] contraseñaCharArray = pwPassword.Password.ToCharArray();
            string contraseña = new string(contraseñaCharArray);
            UserAccount userAccount = new UserAccount();
            userAccount.UserName = txtUserName.Text;
            userAccount.Password = contraseña;
            Employee employee = userDAO.CheckEmployeeExistencebyLogin(userAccount);

            if(employee != null)
            {
                NavigationService.Navigate(new GUI_ConsultCustomerOrder());
            }
            else
            {
                new AlertPopup("Usuario No Existente", "Lo siento, pero el usuario y contraseña proporcionados, no son correctos, verifiquelos y envielos nuevamente", Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void ViewPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Text = pwPassword.Password;
            txtPassword.Visibility = Visibility.Visible;
            pwPassword.Visibility = Visibility.Collapsed;
            imgHidePasswordIcon.Visibility = Visibility.Visible;
            imgViewPasswordIcon.Visibility = Visibility.Collapsed;
        }

        private void HidePassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pwPassword.Password = txtPassword.Text;
            pwPassword.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Collapsed;
            imgHidePasswordIcon.Visibility = Visibility.Collapsed;
            imgViewPasswordIcon.Visibility = Visibility.Visible;
        }
    }
}
