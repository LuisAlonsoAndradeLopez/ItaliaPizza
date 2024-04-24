using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Label = System.Windows.Controls.Label;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_ConsultCustomerOrder.xaml
    /// </summary>
    public partial class GUI_ConsultCustomerOrder : Page
    {
        private CustomerOrdersDAO customerOrdersDAO;
        private ProductDAO productDAO;
        private UserDAO userDAO;
        private CustomerOrderSet customerOrderSet;

        public GUI_ConsultCustomerOrder()
        {
            InitializeComponent();
            InitializeDAOConnections();
            ShowAllOrdersToday();
            FillListBoxStatus();
        }

        private void FillListBoxStatus()
        {
            try
            {
                lboStatusCustomerOrders.ItemsSource = customerOrdersDAO.GetOrderStatuses();
                lboStatusCustomerOrders.DisplayMemberPath = "Status";
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la conexion a la base de datos, " +
                    "intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la base de datos, " +
                    "verifique que los datos que usted ingresa no esten corrompidos!",
                    Auxiliary.AlertPopupTypes.Error);
            }
        }

        public void InitializeDAOConnections()
        {
            customerOrdersDAO = new CustomerOrdersDAO();
            productDAO = new ProductDAO();
            userDAO = new UserDAO();
        }

        public void ShowAllOrdersToday()
        {
            DateTime dateToday = DateTime.Now;
            UpdateDatePickerField(dateToday);
            List<CustomerOrderSet> orders;

            try
            {
                orders = customerOrdersDAO.GetCustomerOrdersByDate(dateToday);
                ShowOrders(orders);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }

        }

        public void UpdateDatePickerField(DateTime date)
        {
            string formattedDate = date.ToString("dd/MM/yyyy");
            txtDatePicker.Text = formattedDate;
        }

        private void DatePicker_OrderDateSelection(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = dpOrderDateFilter.SelectedDate.Value;
            UpdateDatePickerField(selectedDate);
            List<CustomerOrderSet> orders;

            try
            {
                orders = customerOrdersDAO.GetCustomerOrdersByDate(selectedDate);
                ShowOrders(orders);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
            grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Collapsed;
            grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Collapsed;
            grdVirtualWindowSelectOrderAlert.Visibility = Visibility.Visible;
        }

        public void ShowOrders(List<CustomerOrderSet> customerOrders)
        {
            wpCustomerOrders.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var customerOrder in customerOrders)
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
                    Fill = new SolidColorBrush(Color.FromRgb(7, 7, 17)),
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

                string addressIconOrderTypeCustomer = "";

                if (customerOrder.OrderTypeId == 2)
                {
                    addressIconOrderTypeCustomer = Properties.Resources.ICON_CustomerHomeDeliveryOrder;
                }
                else
                {
                    addressIconOrderTypeCustomer = Properties.Resources.ICON_LocalCustomerOrder;
                }

                Image image = new Image
                {
                    Height = 45,
                    Width = 45,
                    Source = new BitmapImage(new Uri(addressIconOrderTypeCustomer, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-750, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblNameCustomerOrder = new Label
                {
                    Content = customerOrder.OrderTypeSet.Type + " #" + customerOrder.Id,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(110, 10, 0, 0),
                };
                grdContainer.Children.Add(lblNameCustomerOrder);

                Label lblCustomerOrderDate = new Label
                {
                    Content = customerOrder.OrderDate.ToString("dd/MM/yyyy"),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(326, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderDate);

                Label lblCustomerOrderTime = new Label
                {
                    Content = customerOrder.RegistrationTime.ToString(),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(520, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderTime);

                Label lblCustomerOrderStatus = new Label
                {
                    Content = customerOrder.OrderStatusSet.Status,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(700, 10, 0, 0),
                };
                grdContainer.Children.Add(lblCustomerOrderStatus);

                grdContainer.PreviewMouseLeftButtonDown += (sender, e) =>
                {
                    ChangeGridColorCustomerOrder(rectBackground, stackPanelContainer);
                    ViewDetailsOrderCustomer(customerOrder);
                    if(customerOrder.OrderStatusId == 1)
                    {
                        btnModifyCustomerOrder.IsEnabled = true;
                        btnModifyCustomerOrder.IsEnabled = true;
                    }
                    else
                    {
                        btnModifyCustomerOrder.IsEnabled = false;
                        btnModifyCustomerOrder.IsEnabled = false;
                    }
                };

                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomerOrders.Children.Add(scrollViewer);
        }

        public void ChangeGridColorCustomerOrder(Rectangle rectBackground, StackPanel stackPanelContainer)
        {
            foreach (Grid grid in stackPanelContainer.Children.OfType<Grid>())
            {
                foreach (Rectangle rect in grid.Children.OfType<Rectangle>())
                {
                    rect.Fill = new SolidColorBrush(Color.FromRgb(7, 7, 17));
                }
            }

            rectBackground.Fill = new SolidColorBrush(Color.FromRgb(232, 189, 111));
        }

        public void ViewDetailsOrderCustomer(CustomerOrderSet customerOrder)
        {
            List<ProductSaleSet> productsOrderCustomer;
            customerOrderSet = customerOrder;
            try
            {
                grdVirtualWindowSelectOrderAlert.Visibility = Visibility.Collapsed;
                grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Visible;
                grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Visible;
                lblOrderTypeCustomer.Content = customerOrder.OrderTypeSet.Type;

                if (customerOrder.OrderTypeSet.Type == "Pedido Domicilio")
                {
                    CustomerSet customer = userDAO.GetCustomerByCustomerOrder(customerOrder.Id);
                    DeliveryDriverSet deliveryman = userDAO.GetDeliveryDriverByCustomerOrder(customerOrder.Id);
                    lblFullNameCustomer.Content = customer.Names + " " + customer.LastName + " " + customer.SecondLastName;
                    lblNameCompleteDeliveryman.Content = deliveryman.Names + " " + deliveryman.LastName + " " + deliveryman.SecondLastName;
                }
                else
                {
                    lblNameCompleteDeliveryman.Content = "Sin repartidor Asignado";
                    lblFullNameCustomer.Content = "Sin cliente Asignado";
                }

                productsOrderCustomer = productDAO.GetOrderProducts(customerOrder);
                ShowOrderProducts(productsOrderCustomer);
                lblTotalOrderCost.Content = "$ " + CalculateTotalCost(productsOrderCustomer).ToString() + ".00";
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
        }

        public void ShowOrderProducts(List<ProductSaleSet> productsOrderCustomer)
        {
            wpCustomerOrderProducts.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var product in productsOrderCustomer)
            {
                Grid grdContainer = new Grid();

                Label lblNameProduct = new Label
                {
                    Content = product.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(20, 0, 0, 0),
                };
                grdContainer.Children.Add(lblNameProduct);

                Label lblCostProduct = new Label
                {
                    Content = "$ " + product.PricePerUnit.ToString() + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(400, 0, 0, 0),
                };
                grdContainer.Children.Add(lblCostProduct);

                Label lblAmountProduct = new Label
                {
                    Content = product.Quantity,
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

        private double CalculateTotalCost(List<ProductSaleSet> productsOrderCustomer)
        {
            double totalOrderCost = 0;

            foreach (var product in productsOrderCustomer)
            {
                totalOrderCost += ((double)product.Quantity * product.PricePerUnit);
            }

            return totalOrderCost;
        }

        private void GoToCreateOrderVirtualWindow(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GUI_CustomerOrderManagementForm(null));
        }

        private void GoToModifyOrderVirtualWindow(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GUI_CustomerOrderManagementForm(customerOrderSet));
        }

        private void ListBox_OrderStatusSelection(object sender, SelectionChangedEventArgs e)
        {
            List<CustomerOrderSet> customerOrders;

            try
            {
                customerOrders = customerOrdersDAO.GetCustomerOrdersByStatus((OrderStatusSet)lboStatusCustomerOrders.SelectedItem);
                ShowOrders(customerOrders);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
            grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Collapsed;
            grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Collapsed;
            grdVirtualWindowSelectOrderAlert.Visibility = Visibility.Visible;
        }

        private void BtnPayCustomerOrderOnClick(object sender, RoutedEventArgs e)
        {     

            PayTransactionTypeComboBox.SelectedItem = FinancialTransactionTypes.Entrada.ToString();
            PayMonetaryValueDecimalUpDown.
            PayContextComboBox.SelectedItem =
            PayDescriptionTextBox.

            grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Collapsed;
            grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Collapsed;
            grdVirtualWindowPayDetails.Visibility = Visibility.Visible;
        }

        private void BtnBackOnClick(object sender, RoutedEventArgs e)
        {
            grdVirtualWindowCustomerOrderInformation.Visibility = Visibility.Visible;
            grdVirtualWindowsCustomerOrderDetails.Visibility = Visibility.Visible;
            grdVirtualWindowPayDetails.Visibility = Visibility.Collapsed;
        }

        private void BtnRealizatePayCustomerOrderOnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                FinancialTransactionSet financialTransaction = new FinancialTransactionSet
                {
                    Type = PayTransactionTypeComboBox.SelectedItem.ToString(),
                    Description = PayDescriptionTextBox.Text,
                    FinancialTransactionDate = DateTime.Now,
                    EmployeeId = 2,
                    MonetaryValue = (double)PayMonetaryValueDecimalUpDown.Value,
                    Context = PayContextComboBox.SelectedItem.ToString()
                };

                new FinancialTransactionDAO().AddFinancialTransaction(financialTransaction);

                new AlertPopup("¡Pago exitoso!", "Pago realizado con éxito.", AlertPopupTypes.Success);

            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
        }
    }
}
