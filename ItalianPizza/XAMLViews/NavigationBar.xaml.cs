using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
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
        Dictionary<string, Image> PicturesButtons = new Dictionary<string, Image>();

        public NavigationBar()
        {
            InitializeComponent();
            LoadButtons();
            ShowButtonsbyUserType();
        }

        private void LoadButtons()
        {
            ButtonsNavegationBar.Add("UserModule", btnUsersModule);
            ButtonsNavegationBar.Add("InventoryModule", btnInventoryModule);
            ButtonsNavegationBar.Add("CustomerOrderModule", btnCustomerOrderModule);
            ButtonsNavegationBar.Add("FinanceModule", btnFinanceModule);
            ButtonsNavegationBar.Add("SupplierModule", btnSupplierModule);
            PicturesButtons.Add("UserModule", imgUsersModule);
            PicturesButtons.Add("InventoryModule", imgInventoryModule);
            PicturesButtons.Add("CustomerOrderModule", imgCustomerOrderModule);
            PicturesButtons.Add("FinanceModule", imgFinanceModule);
            PicturesButtons.Add("SupplierModule", imgSupplierModule);
        }

        private void ShowButtonsbyUserType()
        {
            EmployeePositionSet employeePosition = UserToken.GetEmployeePosition();
            switch(employeePosition.Position)
            {
                case "Mesero": 
                    btnFinanceModule.Visibility = Visibility.Hidden;
                    imgFinanceModule.Visibility= Visibility.Hidden;
                    btnInventoryModule.Visibility = Visibility.Hidden;
                    imgInventoryModule.Visibility= Visibility.Hidden;
                    btnSupplierModule.Visibility = Visibility.Hidden;
                    imgSupplierModule.Visibility= Visibility.Hidden;
                    btnUsersModule.Visibility = Visibility.Hidden;
                    imgUsersModule.Visibility= Visibility.Hidden;
                    break;
                case "Personal Cocina":
                    btnFinanceModule.Visibility = Visibility.Hidden;
                    imgFinanceModule.Visibility = Visibility.Hidden;
                    btnUsersModule.Visibility= Visibility.Hidden;
                    imgUsersModule.Visibility = Visibility.Hidden;
                    btnSupplierModule.Visibility= Visibility.Hidden;
                    imgSupplierModule.Visibility= Visibility.Hidden;
                    break;
                case "Recepcionista":
                    btnUsersModule.Visibility = Visibility.Hidden;
                    imgUsersModule.Visibility= Visibility.Hidden;
                    btnInventoryModule.Visibility= Visibility.Hidden;
                    imgInventoryModule.Visibility= Visibility.Hidden;
                    break;
            }

            VerificarVisibilidad();
        }

        public void VerificarVisibilidad()
        {
            double distanciaEntreElementos = 10;

            for (int i = 1; i < ButtonsNavegationBar.Count; i++) 
            {
                Button button = ButtonsNavegationBar.ElementAt(i).Value;
                Image image = PicturesButtons.ElementAt(i).Value;

                Button buttonAnterior = ButtonsNavegationBar.ElementAt(i - 1).Value;
                Image imageAnterior = PicturesButtons.ElementAt(i - 1).Value;
                double desplazamientoY = 0; 

                if (buttonAnterior.Visibility == Visibility.Hidden || imageAnterior.Visibility == Visibility.Hidden)
                {
                    desplazamientoY = buttonAnterior.Margin.Top + buttonAnterior.ActualHeight + distanciaEntreElementos;
                    button.Margin = new Thickness(button.Margin.Left, desplazamientoY, button.Margin.Right, button.Margin.Bottom);
                    image.Margin = new Thickness(image.Margin.Left, desplazamientoY + 10, image.Margin.Right, image.Margin.Bottom);
                }
                else
                {
                    desplazamientoY = buttonAnterior.Margin.Top + 90;
                    button.Margin = new Thickness(button.Margin.Left, desplazamientoY, button.Margin.Right, button.Margin.Bottom);
                    image.Margin = new Thickness(image.Margin.Left, desplazamientoY + 10, image.Margin.Right, image.Margin.Bottom);
                }
            }
        }

        public void BtnUsersModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_ReviewUsers());
            btnUsersModule.Background = new SolidColorBrush(Colors.Yellow);
        }

        public void BtnInventoryModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_Inventory());
            btnInventoryModule.Background = new SolidColorBrush(Colors.Turquoise);
        }

        public void BtnCustomerOrderModule_Click(object sender, RoutedEventArgs e)
        {
            ChangePage(new GUI_ConsultCustomerOrder());
            btnCustomerOrderModule.Background = new SolidColorBrush(Colors.Aqua);
        }

        public void BtnFinanceModule_Click(object sender, RoutedEventArgs e)
        {
            btnFinanceModule.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        public void BtnSupplierModule_Click(object sender, RoutedEventArgs e)
        {
            btnSupplierModule.Background = new SolidColorBrush(Colors.Aquamarine);
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
