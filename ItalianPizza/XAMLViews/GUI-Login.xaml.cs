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
            NavigationService.Navigate(new GUI_ConsultCustomerOrder());
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
