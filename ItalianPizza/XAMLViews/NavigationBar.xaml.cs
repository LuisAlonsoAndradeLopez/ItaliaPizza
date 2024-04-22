using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using ItalianPizza.XAMLViews.Suppliers;
using ItalianPizza.Auxiliary;
using ItalianPizza.XAMLViews.Finances;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using Page = System.Windows.Controls.Page;
using Window = System.Windows.Window;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        Dictionary<string, Button> ButtonsNavegationBar = new Dictionary<string, Button>();

        public NavigationBar()
        {
            InitializeComponent();
            LoadButtons();
            ShowButtonsbyUserType();
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
        private void LoadButtons()
        {
            ButtonsNavegationBar.Add("UserModule", btnUsersModule);
            ButtonsNavegationBar.Add("InventoryModule", btnInventoryModule);
            ButtonsNavegationBar.Add("CustomerOrderModule", btnCustomerOrderModule);
            ButtonsNavegationBar.Add("FinanceModule", btnFinanceModule);
            ButtonsNavegationBar.Add("SupplierModule", btnSupplierModule);
        }

        private void ShowButtonsbyUserType()
        {
            EmployeePositionSet employeePosition = null;
            try
            {
                employeePosition = UserToken.GetEmployeePosition();
            }
            catch (System.Exception)
            {
            }
            
            if(employeePosition != null)
            {
                switch (employeePosition.Position)
                {
                    case "Mesero":
                        btnFinanceModule.Visibility = Visibility.Hidden;
                        btnInventoryModule.Visibility = Visibility.Hidden;
                        btnSupplierModule.Visibility = Visibility.Hidden;
                        btnUsersModule.Visibility = Visibility.Hidden;
                        break;
                    case "Personal Cocina":
                        btnFinanceModule.Visibility = Visibility.Hidden;
                        btnUsersModule.Visibility = Visibility.Hidden;
                        btnSupplierModule.Visibility = Visibility.Hidden;
                        break;
                    case "Recepcionista":
                        btnUsersModule.Visibility = Visibility.Hidden;
                        btnInventoryModule.Visibility = Visibility.Hidden;
                        break;
                }

                VerificarVisibilidad();
            }
        }

        public void VerificarVisibilidad()
        {
            double distanciaEntreElementos = 10;

            for (int i = 1; i < ButtonsNavegationBar.Count; i++) 
            {
                Button button = ButtonsNavegationBar.ElementAt(i).Value;

                Button buttonAnterior = ButtonsNavegationBar.ElementAt(i - 1).Value;
                double desplazamientoY = 0; 

                if (buttonAnterior.Visibility == Visibility.Hidden)
                {
                    desplazamientoY = buttonAnterior.Margin.Top + buttonAnterior.ActualHeight + distanciaEntreElementos;
                    button.Margin = new Thickness(button.Margin.Left, desplazamientoY, button.Margin.Right, button.Margin.Bottom);
                }
                else
                {
                    desplazamientoY = buttonAnterior.Margin.Top + 90;
                    button.Margin = new Thickness(button.Margin.Left, desplazamientoY, button.Margin.Right, button.Margin.Bottom);
                }
            }
        }

        public void BtnUsersModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_ReviewUsers());
        }

        public void BtnInventoryModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_Inventory());
        }

        public void BtnCustomerOrderModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_ConsultCustomerOrder());
        }

        public void BtnFinanceModule_Click(object sender, RoutedEventArgs e)
        {
            new AlertPopup("¡No disponible!", "En desarrollo de software.", AlertPopupTypes.Error);

            //ChangePage(new GUI_Finances());
        }

        public void BtnSupplierModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_SuppliersModule());
        }

        public void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_Login());
        }

        private void ChangePage(Page page)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Frame framePrincipal = mainWindow.FindName("frameContainer") as Frame;
            framePrincipal.Navigate(page);
        }
    }
}
