using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ItalianPizza.DatabaseModel.DataAccessObject;
using System.Windows.Shapes;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Windows.Input;
using System.Windows.Forms;
using Panel = System.Windows.Controls.Panel;
using Label = System.Windows.Controls.Label;
using UserControl = System.Windows.Controls.UserControl;
using System.Linq;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : UserControl
    {
        private readonly UserDAO userDAO;
        private List<CustomerSet> customerList;
        private CustomerSet customerSet;
        public event EventHandler SelectCustomerEvent;
        public CustomerForm()
        {
            InitializeComponent();
            userDAO = new UserDAO();
            ShowCustomers();
        }
        private void ShowCustomers()
        {
            customerList = userDAO.GetAllCustomers();
            AddVisualCustomersToWindow(customerList);
        }
        public void AddVisualCustomersToWindow(List<CustomerSet> customerList)
        {
            wpCustomers.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            AddNewCustomerRegistrationbutton(stackPanelContainer);

            foreach (var customer in customerList)
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

                Image imgIconCustomer = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Customer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-470, 0, 0, 0),
                };
                grdContainer.Children.Add(imgIconCustomer);

                Label lblFullnameCustomer = new Label
                {
                    Content = customer.Names + " " + customer.LastName + " " + customer.SecondLastName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(80, 10, 0, 0),
                };
                grdContainer.Children.Add(lblFullnameCustomer);

                Image imgSelectCustomer = new Image
                {
                    Height = 35,
                    Width = 35,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-SelectCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(300, 0, 0, 0),
                };
                imgSelectCustomer.PreviewMouseLeftButtonDown += (sender, e) => SelectCustomer_Click(sender, e, customer);
                grdContainer.Children.Add(imgSelectCustomer);

                Image imgEditCustomer = new Image
                {
                    Height = 30,
                    Width = 30,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-UpdateCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(400, 0, 0, 0),
                };
                imgEditCustomer.PreviewMouseLeftButtonDown += (sender, e) => ShowCustomerForm(customer);
                grdContainer.Children.Add(imgEditCustomer);

                Image imgDeleteCustomer = new Image
                {
                    Height = 30,
                    Width = 30,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-DeleteCustomer.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(500, 0, 0, 0),
                };
                imgDeleteCustomer.PreviewMouseLeftButtonDown += (sender, e) => DeleteCustomer(customer);
                grdContainer.Children.Add(imgDeleteCustomer);


                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomers.Children.Add(scrollViewer);
        }

        private void AddNewCustomerRegistrationbutton(StackPanel stackPanel)
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
                Content = "Agregar nuevo cliente",
                Foreground = new SolidColorBrush(Color.FromRgb(253, 254, 254)),
                FontWeight = FontWeights.Bold,
                FontSize = 19,
                Margin = new Thickness(195, 10, 0, 0),
            };
            grdButtonAdd.Children.Add(lblTitleButton);

            Image iconButton = new Image
            {
                Height = 45,
                Width = 45,
                Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-AddCustomer.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.Fill,
                Margin = new Thickness(-225, 0, 0, 0),
            };
            grdButtonAdd.Children.Add(iconButton);
            grdButtonAdd.PreviewMouseLeftButtonDown += (sender, e) => ShowCustomerForm(null);

            stackPanel.Children.Add(grdButtonAdd);
        }
        private void Click_CloseCustomerModule(object sender, MouseButtonEventArgs e)
        {
            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
               parentPanel.Children.Remove(this);
            }
        }
        private void ShowCustomerForm(CustomerSet customer)
        {
            grdCustomerModule.Visibility = Visibility.Hidden;
            grdCustomerForm.Visibility = Visibility.Visible;
            if(customer != null)
            {

            }
        }
        private void DeleteCustomer(CustomerSet customer)
        {

        }
        protected void SelectCustomer_Click(object sender, EventArgs e, CustomerSet customer)
        {
            customerSet = customer;
            SelectCustomerEvent?.Invoke(this, EventArgs.Empty);
        }
        public CustomerSet GetSelectCustomer()
        {
            return customerSet;
        }
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            grdCustomerForm.Visibility = Visibility.Hidden;
            grdCustomerModule.Visibility = Visibility.Visible;
        }
        private void TextBox_CustomerSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            FilterCustomer(textSearch);
        }

        private void FilterCustomer(string textSearch)
        {
            List<CustomerSet> filteredCustomers = customerList
            .Where(p =>
                p.Names.ToLower().Contains(textSearch.ToLower()) ||
                p.LastName.ToLower().Contains(textSearch.ToLower()) ||
                p.SecondLastName.ToLower().Contains(textSearch.ToLower())
            )
            .ToList();

            AddVisualCustomersToWindow(filteredCustomers);
        }
    }
}
