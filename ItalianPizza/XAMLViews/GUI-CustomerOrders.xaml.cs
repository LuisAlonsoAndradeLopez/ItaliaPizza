using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Windows;
using System.Windows.Controls;
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
        private CustomerOrdersDAO customerOrdersDAO;
        private ProductDAO productDAO;

        public GUI_CustomerOrders()
        {
            InitializeComponent();
            customerOrdersDAO = new CustomerOrdersDAO();
            productDAO = new ProductDAO();
            InitializeComboBoxes();
            ShowAllOrdersToday();
        }

        public void ShowAllOrdersToday()
        {
            DateTime dateToday = DateTime.Today;
            string formattedDate = dateToday.ToString("dd/MM/yyyy");
            List<OrdenCliente> orders;

            try
            {
                orders = customerOrdersDAO.GetCustomerOrdersByDate(formattedDate);
                ShowOrders(orders);
            }
            catch (EntityException ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                
            }

        }

        public void ShowOrders(List<OrdenCliente> orders)
        {
            wpCustomerOrders.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
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
            foreach(var status in statusCustomersOrders)
            {
                cboStatusCustomerOrders.Items.Add(status);
            }
        }

        public void ViewDetailsOrderCustomer(object sender, MouseButtonEventArgs e, OrdenCliente ordenCliente)
        {
            List<Producto> products;

            try
            {
                products = customerOrdersDAO.GetOrderProducts(ordenCliente.Id);
                ShowOrderProducts(products);
            }
            catch (EntityException ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                
            }

        }

        public void ShowOrderProducts(List<Producto> orderProducts)
        {
            wpCustomerOrderProducts.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var product in orderProducts)
            {
                Grid grdContainer = new Grid();

                Label lblNameProduct = new Label
                {
                    Content = product.Nombre,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(20, 0, 0, 0),
                };
                grdContainer.Children.Add(lblNameProduct);

                Label lblCostProduct = new Label
                {
                    Content = "$ " + product.Costo.ToString() + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(400, 0, 0, 0),
                };
                grdContainer.Children.Add(lblCostProduct);

                Label lblAmountProduct = new Label
                {
                    Content = '6',
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(300, 0, 0, 0),
                };
                grdContainer.Children.Add(lblAmountProduct);
                stackPanelContainer.Children.Add(grdContainer);

            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomerOrderProducts.Children.Add(scrollViewer);
        }

        private void ComboBox_OrderStatusSelection(object sender, SelectionChangedEventArgs e)
        {
            string status = cboStatusCustomerOrders.SelectedItem.ToString().Trim();
            List<OrdenCliente> orders;
            
            try
            {
                orders = customerOrdersDAO.GetCustomerOrdersByStatus(status);
                ShowOrders(orders);
            }
            catch (EntityException ex)
            {

            }
            catch (InvalidOperationException ex)
            {

            }
        }

        private void DatePicker_OrderDateSelection(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = dpOrderDateFilter.SelectedDate.Value;
            string formattedDate = selectedDate.ToString("dd/MM/yyyy");
            List<OrdenCliente> orders = customerOrdersDAO.GetCustomerOrdersByDate(formattedDate);
            ShowOrders(orders);
        }

        private void ShowActiveProducts()
        {
            List<Producto> products;

            try
            {
                products = productDAO.GetAllActiveProducts();
                AddVisualProductsToWindow(products);
            }
            catch (EntityException ex)
            {
                
            }
            catch (InvalidOperationException ex)
            {
                
            }

        }

        private void AddVisualProductsToWindow(List<Producto> products)
        {
            wpProducts.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var product in products)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(55, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 145,
                    Width = 808,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(190, 23, 4)),
                    Margin = new Thickness(-45, 0, 0, 0),
                };
                grdContainer.Children.Add(rectBackground);

                Image image = new Image
                {
                    Height = 120,
                    Width = 120,
                    Source = new BitmapImage(new Uri(product.Foto, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-665, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblNameCustomerOrder = new Label
                {
                    Content = product.Nombre,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(120, 15, 0, 0),
                };
                grdContainer.Children.Add(lblNameCustomerOrder);

                Label lblOrderCostCustomer = new Label
                {
                    Content = "$ " + product.Costo + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 19,
                    Margin = new Thickness(120, 42, 0, 0),
                };
                grdContainer.Children.Add(lblOrderCostCustomer);

                Button btnAddProductOrder = new Button
                {
                    Content = "Agregar al pedido",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    Background = new SolidColorBrush(Color.FromRgb(255, 188, 17)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(255, 188, 17)),
                    FontSize = 14,
                    Height = 35,
                    Width = 150,
                    Margin = new Thickness(540, 82, 0, 0),
                };

                grdContainer.Children.Add(btnAddProductOrder);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpProducts.Children.Add(scrollViewer);
        }

        private void GoToCreateOrderVirtualWindow(object sender, RoutedEventArgs e)
        {
            InitializeVirtualWindowCreateOrder();
        }

        private void InitializeVirtualWindowCreateOrder()
        {
            CloseVirtualWindowConsultCustomerOrders();
            grdVirtualWindowCreateOrder.Visibility = Visibility.Visible;
            grdVirtualWindowOrderDataForm.Visibility = Visibility.Visible;
            ShowActiveProducts();
        }

        private void CloseVirtualWindowConsultCustomerOrders()
        {
            VirtualWindowConsultCustomerOrders.Visibility = Visibility.Collapsed;
            grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Collapsed;
            wpCustomerOrderProducts.Children.Clear();
            lblTotalOrderCost.Content = "$ 0.00";
        }
    }
}
