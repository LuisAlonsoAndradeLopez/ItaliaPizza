using ItalianPizza.Auxiliary;
using ItalianPizza.SingletonClasses;
using ItalianPizza.XAMLViews;
using System.ComponentModel;
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
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (CustomerOrderToken.TransactionInProcess())
            {
                new AlertPopup("¡Pedido En Proceso!",
                    "Tienes un pedido en proceso, no puedes cerrar la ventana",
                    AlertPopupTypes.Error);
                e.Cancel = true;
            }
        }
    }
}
