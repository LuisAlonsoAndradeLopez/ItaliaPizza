using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPizza.XAMLViews.CustomerOrder
{
    /// <summary>
    /// Lógica de interacción para DeliveryDriverForm.xaml
    /// </summary>
    public partial class DeliveryDriverForm : UserControl
    {
        private readonly UserDAO userDAO;
        private List<DeliveryDriverSet> deliveryDriverList;
        private DeliveryDriverSet deliveryDriverSet;
        public event EventHandler SelectDeliveryDriverEvent;
        public DeliveryDriverForm()
        {
            InitializeComponent();
            userDAO = new UserDAO();
            ShowDeliveryDrivers();
        }

        public DeliveryDriverSet GetSelectDeliveryDriver()
        {
            return deliveryDriverSet;
        }
        private void ShowDeliveryDrivers()
        {
            deliveryDriverList = userDAO.GetAllDeliveryDriver();
            AddVisualDeliveryDriverToWindow(deliveryDriverList);
        }
        public void AddVisualDeliveryDriverToWindow(List<DeliveryDriverSet> deliveryDriverList)
        {
            wpDeliveryDriver.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            AddNewDeliveryDriverRegistrationbutton(stackPanelContainer);

            foreach (var deliveryDriver in deliveryDriverList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 55,
                    Width = 567,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(23, 32, 42)),
                };

                DropShadowEffect dropShadowEffect = new DropShadowEffect
                {
                    Color = Colors.Black,
                    Direction = 315,
                    ShadowDepth = 5,
                    Opacity = 0.5,
                };
                rectBackground.Effect = dropShadowEffect;
                grdContainer.Children.Add(rectBackground);

                Image imgIconDeliveryDriver = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-DeliveryDriver.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-470, 0, 0, 0),
                };
                grdContainer.Children.Add(imgIconDeliveryDriver);

                Label lblFullnameDeliveryDriver = new Label
                {
                    Content = deliveryDriver.Names + " " + deliveryDriver.LastName + " " + deliveryDriver.SecondLastName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(80, 10, 0, 0),
                };
                grdContainer.Children.Add(lblFullnameDeliveryDriver);

                Image imgSelectDeliveryDriver = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-SelectCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(500, 0, 0, 0),
                };
                imgSelectDeliveryDriver.PreviewMouseLeftButtonDown += (sender, e) => SelectDeliveryDriver_Click(sender, e, deliveryDriver);
                grdContainer.Children.Add(imgSelectDeliveryDriver);

                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpDeliveryDriver.Children.Add(scrollViewer);
        }
        private void AddNewDeliveryDriverRegistrationbutton(StackPanel stackPanel)
        {
            Grid grdButtonAdd = new Grid
            {
                Margin = new Thickness(0, 0, 0, 10),
            };

            Rectangle rectBackgroundButtonAdd = new Rectangle
            {
                Height = 55,
                Width = 567,
                RadiusX = 30,
                RadiusY = 30,
                Fill = new SolidColorBrush(Color.FromRgb(46, 64, 83)),
            };

            DropShadowEffect dropShadowEffectButton = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 315,
                ShadowDepth = 5,
                Opacity = 0.5,
            };

            rectBackgroundButtonAdd.Effect = dropShadowEffectButton;
            grdButtonAdd.Children.Add(rectBackgroundButtonAdd);

            Label lblTitleButton = new Label
            {
                Content = "Agregar nuevo repartidor",
                Foreground = new SolidColorBrush(Color.FromRgb(253, 254, 254)),
                FontWeight = FontWeights.Bold,
                FontSize = 19,
                Margin = new Thickness(170, 10, 0, 0),
            };
            grdButtonAdd.Children.Add(lblTitleButton);

            Image iconButton = new Image
            {
                Height = 45,
                Width = 45,
                Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-AddCustomer.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill,
                Margin = new Thickness(-245, 0, 0, 0),
            };
            grdButtonAdd.Children.Add(iconButton);
            grdButtonAdd.PreviewMouseLeftButtonDown += (sender, e) => ShowDeliveryDriverForm(null);

            stackPanel.Children.Add(grdButtonAdd);
        }
        private void CloseDeliveryDriverModule_Click(object sender, MouseButtonEventArgs e)
        {
            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Children.Remove(this);
            }
        }
        private void ShowDeliveryDriverForm(DeliveryDriverSet deliveryDriver)
        {
            grdDeliverymanModule.Visibility = Visibility.Hidden;
            grdDeliverymanForm.Visibility = Visibility.Visible;
            if (deliveryDriver != null)
            {

            }
        }
        private void DeleteDeliveryDriver(DeliveryDriverSet deliveryDriver)
        {

        }
        private DeliveryDriverSet GetDataDelivery()
        {
            DeliveryDriverSet deliveryDriver = new DeliveryDriverSet
            {
                Names = txtNames.Text,
                LastName = txtLastName.Text,
                SecondLastName = txtLastSecondName.Text,
                Email = txtEmail.Text,
                Phone = txtPhoneNumber.Text,
                EmployeeId = UserToken.GetEmployeeID(),
                UserStatusId = 1
            };

            return deliveryDriver;
        }
        protected void SelectDeliveryDriver_Click(object sender, EventArgs e, DeliveryDriverSet deliveryDriver)
        {
            deliveryDriverSet = deliveryDriver;
            SelectDeliveryDriverEvent?.Invoke(this, EventArgs.Empty);
        }
        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            grdDeliverymanForm.Visibility = Visibility.Hidden;
            grdDeliverymanModule.Visibility = Visibility.Visible;
        }
        private void TextBox_DeliveryDriverSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            FilterDeliveryDriver(textSearch);
        }
        private void FilterDeliveryDriver(string textSearch)
        {
            List<DeliveryDriverSet> filteredDeliveryDriver = deliveryDriverList
            .Where(p =>
                p.Names.ToLower().Contains(textSearch.ToLower()) ||
                p.LastName.ToLower().Contains(textSearch.ToLower()) ||
                p.SecondLastName.ToLower().Contains(textSearch.ToLower())
            )
            .ToList();

            AddVisualDeliveryDriverToWindow(filteredDeliveryDriver);
        }

    }
}
