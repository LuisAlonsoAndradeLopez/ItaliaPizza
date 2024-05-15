using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
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

namespace ItalianPizza.XAMLViews.Suppliers
{
    /// <summary>
    /// Lógica de interacción para GUI_SuppliersModule.xaml
    /// </summary>
    public partial class GUI_SuppliersModule : Page
    {
        private SupplierDAO supplierDAO;
        private SupplyDAO supplyDAO;
        private OrderSupplierDAO orderSupplierDAO;
        private List<SupplierSet> suppliersList;
        private List<SupplierOrderSet> supplierOrderlist;

        public GUI_SuppliersModule()
        {
            InitializeComponent();
            InitializeDAOConnections();
            ShowSuppliersList();
            ShowOrderSupplierList();
            InitializeListBox();
        }

        public void InitializeListBox()
        {
            lboSearchbySupply.ItemsSource = supplyDAO.GetAllSupplyWithoutPhoto();
            lboSearchbySupply.DisplayMemberPath = "Name";
        }

        public void InitializeDAOConnections()
        {
            supplierDAO = new SupplierDAO();
            supplyDAO = new SupplyDAO();
            orderSupplierDAO = new OrderSupplierDAO();
        }

        public void ShowSuppliersList()
        {
            suppliersList = supplierDAO.GetAllSuppliers();
            if (suppliersList != null)
            {
                ShowOnSuppliersScreen(suppliersList);
            }
        }

        public void ShowOrderSupplierList()
        {
            DateTime dateToday = DateTime.Now;
            UpdateDatePickerField(dateToday);
            supplierOrderlist = orderSupplierDAO.GetSupplierOrdersByDate(dateToday);

            if (supplierOrderlist != null)
            {
                ShowOnSupplierOrdersScreen(supplierOrderlist);
            }
        }

        public void ShowOnSupplierOrdersScreen(List<SupplierOrderSet> supplierOrderList)
        {
            wpOrdersSuppliers.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (SupplierOrderSet supplierOrder in supplierOrderList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(8, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 100,
                    Width = 560,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(25, 25, 25)),
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

                Image image = new Image
                {
                    Height = 50,
                    Width = 50,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-PedidoProveedor.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-450, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblSupplierName = new Label
                {
                    Content = supplierOrder.SupplierSet.CompanyName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 17,
                    Margin = new Thickness(110, 10, 0, 0),
                };
                grdContainer.Children.Add(lblSupplierName);

                Label lblCompanyName = new Label
                {
                    Content = supplierOrder.OrderDate,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 17,
                    Margin = new Thickness(110, 30, 0, 0),
                };
                grdContainer.Children.Add(lblCompanyName);

                Label lblStatus = new Label
                {
                    Content = supplierOrder.OrderStatusSet.Status,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 17,
                    Margin = new Thickness(110, 50, 0, 0),
                };
                grdContainer.Children.Add(lblStatus);

                Button btnViewDetails = new Button
                {
                    Content = "Ver Detalles",
                    Background = new SolidColorBrush(Color.FromRgb(85, 25, 25)),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    Height = 40,
                    Width = 145,
                    FontSize = 15,
                    Margin = new Thickness(385, 0, 0, 0),
                };

                btnViewDetails.Click += (sender, e) => NavigationService.Navigate(new GUI_SupplierOrdersModule(supplierOrder));

                grdContainer.Children.Add(btnViewDetails);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpOrdersSuppliers.Children.Add(scrollViewer);
        }

        public void ShowOnSuppliersScreen(List<SupplierSet> supplierSetList)
        {
            wpSuppliers.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var supplier in supplierSetList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 100,
                    Width = 860,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(25, 25, 25)),
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

                Image image = new Image
                {
                    Height = 70,
                    Width = 70,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Supplier.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-750, 0, 0, 0),
                };
                grdContainer.Children.Add(image);

                Label lblSupplierName = new Label
                {
                    Content = supplier.Names + " " + supplier.LastName + " " + supplier.SecondLastName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(110, 20, 0, 0),
                };
                grdContainer.Children.Add(lblSupplierName);

                Label lblCompanyName = new Label
                {
                    Content = supplier.CompanyName,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 19,
                    Margin = new Thickness(110, 50, 0, 0),
                };
                grdContainer.Children.Add(lblCompanyName);

                Button btnViewDetails = new Button
                {
                    Content = "Ver Detalles",
                    Background = new SolidColorBrush(Color.FromRgb(28, 23, 85)),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    Height = 45,
                    Width = 150,
                    FontSize = 16,
                    Margin = new Thickness(660, 10, 0, 0),
                };

                btnViewDetails.Click += (sender, e) => OpenSupplierForm(supplier);

                grdContainer.Children.Add(btnViewDetails);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpSuppliers.Children.Add(scrollViewer);
        }

        private void OpenSupplierForm(SupplierSet supplier)
        {
            SuppliersForm suppliersForm = new SuppliersForm(supplier)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(255, 13, 0, 0)
            };
            Grid.SetColumn(suppliersForm, 0);
            Background.Children.Add(suppliersForm);

            if (supplier != null)
            {
                List<SupplierOrderSet> supplierOrders;
                supplierOrders = orderSupplierDAO.GetSupplierOrderbySupplier(supplier.Id);
                ShowOnSupplierOrdersScreen(supplierOrders);
            }
        }

        private void LboSearchbySupply_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<SupplierSet> suppliers;
            try
            {
                SupplySet supply = (SupplySet)lboSearchbySupply.SelectedItem;
                suppliers = supplierDAO.GetAllSuppliersBySupply(supply.Name);
                ShowOnSuppliersScreen(suppliers);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos",
                    "Lo siento, pero a ocurrido un error con la conexion a la base de datos," +
                    " intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos",
                    "Lo siento, pero a ocurrido un error con la base de datos, verifique que " +
                    "los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
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
            List<SupplierOrderSet> orders;

            try
            {
                orders = orderSupplierDAO.GetSupplierOrdersByDate(selectedDate);

                if (supplierOrderlist != null)
                {
                    ShowOnSupplierOrdersScreen(orders);
                }
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
                    "Lo siento, pero a ocurrido un error con la base de datos, verifique que los " +
                    "datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void TextBox_SupplierSearch(object sender, EventArgs e)
        {
            string textSearch = txtSearchbyName.Text;
            RecoverProducts(textSearch);
        }

        private void RecoverProducts(string textSearch)
        {
            List<SupplierSet> filteredSupplier = suppliersList
                .Where(p =>
                    p.Names.ToLower().Contains(textSearch.ToLower()) ||
                    p.LastName.ToLower().Contains(textSearch.ToLower()) ||
                    p.SecondLastName.ToLower().Contains(textSearch.ToLower()) ||
                    p.CompanyName.ToLower().Contains(textSearch.ToLower())
                )
                .ToList();
            ShowOnSuppliersScreen(filteredSupplier);
        }

        private void BtnRegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            OpenSupplierForm(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GUI_SupplierOrdersModule(null));
        }


    }
}
