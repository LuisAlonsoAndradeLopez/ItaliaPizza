using ItalianPizza.XAMLViews;
using System.Windows;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GUI_CustomerOrders VENTANA = new GUI_CustomerOrders();
            VENTANA.Show();
            this.Close();
        }
    }
}
