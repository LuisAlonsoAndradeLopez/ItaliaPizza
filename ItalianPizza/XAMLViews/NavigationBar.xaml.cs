using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Controls.Button;
using Window = System.Windows.Window;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        Dictionary<string, Button> diccionarioBotones = new Dictionary<string, Button>();

        public NavigationBar()
        {
            InitializeComponent();
            LoadButtons();
        }

        public void LoadButtons()
        {
            diccionarioBotones.Add("UserModule", btnUsersModule);
            diccionarioBotones.Add("InventoryModule", btnInventoryModule);
            diccionarioBotones.Add("CustomerOrderModule", btnCustomerOrderModule);
            diccionarioBotones.Add("FinanceModule", btnFinanceModule);
            diccionarioBotones.Add("SupplierModule", btnSupplierModule);
            diccionarioBotones.Add("LogOut", btnLogOut);
        }

        public void ReorganizarGrillas(Grid grillaOculta)
        {
            int indiceOculta = Background.Children.IndexOf(grillaOculta);

            if (indiceOculta == -1)
                return;

            for (int i = indiceOculta + 1; i < Background.Children.Count; i++)
            {
                if (Background.Children[i] is Grid grilla)
                {
                    grilla.Margin = new Thickness(
                        grilla.Margin.Left,
                        grilla.Margin.Top - grillaOculta.ActualHeight - 89,
                        grilla.Margin.Right,
                        grilla.Margin.Bottom
                    );
                }
            }
        }

        public void SelectModuleButton(string buttonName)
        {

        }

        public static Frame FindFrame(DependencyObject start)
        {
            DependencyObject parent = start;

            while (parent != null)
            {
                if (parent is MainWindow mainWindow)
                {
                    return FindFrameInMainWindow(mainWindow);
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static Frame FindFrameInMainWindow(MainWindow mainWindow)
        {
            if (mainWindow != null)
            {
                return mainWindow.FindName("framePrincipal") as Frame;
            }

            return null;
        }

        public void BtnUsersModule_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Frame framePrincipal = mainWindow.FindName("frameContainer") as Frame;
            GUI_CustomerOrderManagementForm pagina = new GUI_CustomerOrderManagementForm(new List<ProductSale>());
            framePrincipal.Navigate(pagina);
        }

        public void BtnInventoryModule_Click(object sender, RoutedEventArgs e)
        {
            btnInventoryModule.Background = new SolidColorBrush(Colors.Yellow);
        }

        public void BtnCustomerOrderModule_Click(object sender, RoutedEventArgs e)
        {
            btnCustomerOrderModule.Background = new SolidColorBrush(Colors.Turquoise);
        }

        public void BtnFinanceModule_Click(object sender, RoutedEventArgs e)
        {
            btnFinanceModule.Background = new SolidColorBrush(Colors.Aqua);
        }

        public void BtnSupplierModule_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Frame framePrincipal = mainWindow.FindName("frameContainer") as Frame;
            GUI_CustomerOrderManagementForm pagina = new GUI_CustomerOrderManagementForm(new List<ProductSale>());
            framePrincipal.Navigate(pagina);
        }


        public void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            btnLogOut.Background = new SolidColorBrush(Colors.AliceBlue);
        }
    }
}
