using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_CustomerOrders.xaml
    /// </summary>
    public partial class GUI_CustomerOrders : Window
    {
        public CustomerOrdersDAO customerOrdersDAO;
        public GUI_CustomerOrders()
        {
            InitializeComponent();
            customerOrdersDAO = new CustomerOrdersDAO();
            InitializeComboBoxes();
            ShowAllOrdersToday();
        }

        public void ShowAllOrdersToday()
        {
            DateTime today = DateTime.Today;
            string formattedDate = today.ToString("dd/MM/yyyy");
            List<OrdenCliente> orders = customerOrdersDAO.GetCustomerOrdersByDate(formattedDate);
            ShowOrders(orders);
        }

        public void ShowOrders(List<OrdenCliente> orders)
        {
            wpCustomerOrders.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var Order in orders)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 55,
                    Width = 857,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(23, 23, 33)),
                };
                grdContainer.Children.Add(rectBackground);

                Image image = new Image
                {
                    Height = 45,
                    Width = 45,
                    Source = new BitmapImage(new Uri("..\\Resources\\Pictures\\ICON-Domicilio.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-750, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblNameCustomerOrder = new Label
                {
                    Content = Order.Nombre,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(110, 10, 0, 0),
                };
                grdContainer.Children.Add(lblNameCustomerOrder);

                Label lblCustomerOrderDate = new Label
                {
                    Content = Order.Fecha.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(326, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderDate);


                Label lblCustomerOrderTime = new Label
                {
                    Content = Order.Hora.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(520, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderTime);

                Label lblCustomerOrderStatus = new Label
                {
                    Content = Order.Estado,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(700, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderStatus);

                grdContainer.PreviewMouseLeftButtonDown += (sender, e) => ViewDetailsOrderCustomer(sender, e, Order);

                stackPanelContainer.Children.Add(grdContainer);

            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomerOrders.Children.Add(scrollViewer);

        }

        public void InitializeComboBoxes()
        {
            string[] statusCustomersOrders = { "Entregado", "Listo", "Cancelado", "En Proceso", "En Cocina"};
            foreach(var status in  statusCustomersOrders)
            {
                cboStatusCustomerOrders.Items.Add(status);
            }
        }

        public void ViewDetailsOrderCustomer(object sender, MouseButtonEventArgs e, OrdenCliente ordenCliente)
        {
            lblOrderSelectionSignal.Visibility = Visibility.Collapsed;
            grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string status = cboStatusCustomerOrders.SelectedItem.ToString().Trim();
            List<OrdenCliente> orders = customerOrdersDAO.GetCustomerOrdersByStatus(status);
            ShowOrders(orders);
        }

        private void DatePicker1_ValueChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = datePicker1.SelectedDate.Value;
            string formattedDate = selectedDate.ToString("dd/MM/yyyy");
            List<OrdenCliente> orders = customerOrdersDAO.GetCustomerOrdersByDate(formattedDate);
            ShowOrders(orders);
        }
    }
}
