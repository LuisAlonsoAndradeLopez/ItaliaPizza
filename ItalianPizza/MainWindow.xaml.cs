using ItalianPizza.XAMLViews;
using ItalianPizza.XAMLViews.Finances;
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
            frameContainer.Navigate(new GUI_Login());
        }
    }
}
