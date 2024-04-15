using ItalianPizza.Auxiliary;
using ItalianPizza.XAMLViews.Finances;
using System.Collections.Generic;
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
            ChangePage(new GUI_Finances());
        }

        public void BtnSupplierModule_Click(object sender, RoutedEventArgs e)
        {
            new AlertPopup("¡No disponible!", "En desarrollo de software.", AlertPopupTypes.Error);
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
