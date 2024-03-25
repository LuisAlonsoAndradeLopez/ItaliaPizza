using ItalianPizza.XAMLViews;
using System.Windows;
using System.Windows.Navigation;

namespace ItalianPizza
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            NavigationService navigationService = MainFrame.NavigationService;
            navigationService.Navigate(new GUI_Inventory());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //GUI_CustomerOrders VENTANA = new GUI_CustomerOrders();

            NavigationService navigationService = MainFrame.NavigationService;
            navigationService.Navigate(new GUI_AddArticle());
        }
    }
}
