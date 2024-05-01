using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;

namespace ItalianPizza.XAMLViews.Suppliers
{
    /// <summary>
    /// Lógica de interacción para GUI_SupplierOrdersModule.xaml
    /// </summary>
    public partial class GUI_SupplierOrdersModule : Page
    {
        private List<SupplySet> supplySetList;
        private List<SupplySet> listSupplySupplierOrder;
        private SupplierOrderSet supplierOrderSet;
        private SupplyDAO supplyDAO;
        private SupplierDAO supplierDAO;
        private CustomerOrdersDAO customerOrdersDAO;

        public GUI_SupplierOrdersModule(SupplierOrderSet supplierOrder)
        {
            InitializeComponent();
            InitializeDAOConnections();
            InitializeListBoxes();
            InitializeSupplies();
            InitializeSupplierOrderInformation(supplierOrder);
        }

        private void InitializeSupplierOrderInformation(SupplierOrderSet supplierOrder)
        {
            if (supplierOrder != null)
            {
                supplierOrderSet = supplierOrder;
                try
                {
                    listSupplySupplierOrder = supplyDAO.GetAllSuppliesBySupplierOrder(supplierOrder);
                    ShowOrderSupplies(listSupplySupplierOrder);
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                        "la conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                        Auxiliary.AlertPopupTypes.Error);
                }
                catch (InvalidOperationException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un" +
                        " error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
            }
            else
            {
                listSupplySupplierOrder = new List<SupplySet>();
            }
        }

        private void InitializeDAOConnections()
        {
            supplyDAO = new SupplyDAO();
            supplierDAO = new SupplierDAO();
            customerOrdersDAO = new CustomerOrdersDAO();
        }

        private void InitializeSupplies()
        {
            try
            {
                supplySetList = supplyDAO.GetAllSupply();
                AddVisualSuppliesToWindow(supplySetList);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                    "la conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                    Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un" +
                    " error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                    Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void InitializeListBoxes()
        {
            lboOrderStatus.ItemsSource = customerOrdersDAO.GetOrderStatuses();
            lboOrderStatus.DisplayMemberPath = "Status";
            lboSuppliers.ItemsSource = supplierDAO.GetAllSuppliers();
            lboSuppliers.DisplayMemberPath = "CompanyName";
        }

        private void TextBox_ProductSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            RecoverProducts(textSearch);
        }

        private void RecoverProducts(string textSearch)
        {
            List<SupplySet> filteredProducts = supplySetList
                .Where(p => p.Name.ToLower().Contains(textSearch.ToLower())).ToList();
            AddVisualSuppliesToWindow(filteredProducts);
        }

        public void ShowOrderSupplies(List<SupplySet> orderSupplies)
        {
            wpSupplierOrderSupplies.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (var product in orderSupplies)
            {
                Grid grdContainer = new Grid();

                Label lblName = new Label
                {
                    Content = product.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(20, 0, 0, 0),
                };
                grdContainer.Children.Add(lblName);

                Label lblUnitMeasurement = new Label
                {
                    Content = product.SupplyUnitSet.Unit,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(420, 0, 0, 0),
                };
                grdContainer.Children.Add(lblUnitMeasurement);

                Label lblAmount = new Label
                {
                    Content = product.Quantity,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(330, 0, 0, 0),
                };
                grdContainer.Children.Add(lblAmount);
                stackPanelContainer.Children.Add(grdContainer);

                Label lblCost = new Label
                {
                    Content = "$ " + product.PricePerUnit.ToString() + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(520, 0, 0, 0),
                };
                grdContainer.Children.Add(lblCost);
            }

            scrollViewer.Content = stackPanelContainer;
            wpSupplierOrderSupplies.Children.Add(scrollViewer);
        }

        private double CalculateTotalCost()
        {
            double totalOrderCost = 0;

            foreach (var product in listSupplySupplierOrder)
            {
                totalOrderCost += ((double)product.Quantity * product.PricePerUnit);
            }

            return totalOrderCost;
        }

        private void AddVisualSuppliesToWindow(List<SupplySet> suppliesList)
        {
            wpSupplies.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (SupplySet supply in suppliesList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 145,
                    Width = 729,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(70, 65, 65)),
                    Margin = new Thickness(0, 0, 0, 0),
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

                Image imgPhotoProduct = new Image
                {
                    Height = 120,
                    Width = 120,
                    Source = GetBitmapImage(supply.Picture),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-565, 0, 0, 0),
                };
                grdContainer.Children.Add(imgPhotoProduct);

                Label lblTitleSupplyName = new Label
                {
                    Content = supply.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.Black,
                    FontSize = 24,
                    Margin = new Thickness(165, 16, 0, 0),
                };
                grdContainer.Children.Add(lblTitleSupplyName);

                Label lblTitleSupplyCost = new Label
                {
                    Content = "Costo: ",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 189, 58)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(165, 55, 0, 0),
                };
                grdContainer.Children.Add(lblTitleSupplyCost);

                Label lblSupplyCost = new Label
                {
                    Content = "$ " + supply.PricePerUnit + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(230, 55, 0, 0),
                };
                grdContainer.Children.Add(lblSupplyCost);

                Label lblTitleSupplyUnit = new Label
                {
                    Content = "Unidad de Medida: ",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 189, 58)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(165, 90, 0, 0),
                };
                grdContainer.Children.Add(lblTitleSupplyUnit);

                Label lblSupplyUnit = new Label
                {
                    Content = supply.SupplyUnitSet.Unit,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(345, 90, 0, 0),
                };
                grdContainer.Children.Add(lblSupplyUnit);

                Image imgAddProductIcon = new Image
                {
                    Height = 40,
                    Width = 40,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Agregar.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(640, 50, 0, 0),
                };

                imgAddProductIcon.MouseLeftButtonUp += (sender, e) => AddSupplyToOrder(supply);

                grdContainer.Children.Add(imgAddProductIcon);

                Image imgReduceProductIcon = new Image
                {
                    Height = 40,
                    Width = 40,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Disminuir.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(540, 50, 0, 0),
                };

                imgReduceProductIcon.MouseLeftButtonUp += (sender, e) => RemoveSupplyToOrder(supply);

                grdContainer.Children.Add(imgReduceProductIcon);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpSupplies.Children.Add(scrollViewer);
        }

        private void AddSupplyToOrder(SupplySet supplySet)
        {
            SupplySet supplyExisting = listSupplySupplierOrder.FirstOrDefault(p => p.Id == supplySet.Id);
            if (supplyExisting == null)
            {
                supplySet.Quantity = 1;
                listSupplySupplierOrder.Add(supplySet);
            }
            else
            {
                supplyExisting.Quantity++;
            }

            ShowOrderSupplies(listSupplySupplierOrder);
            lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
        }

        private void RemoveSupplyToOrder(SupplySet supplySet)
        {
            SupplySet supplyExisting = listSupplySupplierOrder.FirstOrDefault(p => p.Name == supplySet.Name);
            if (supplyExisting != null && supplyExisting.Quantity > 1)
            {
                supplyExisting.Quantity--;
            }
            else if (supplyExisting != null && supplyExisting.Quantity == 1)
            {
                supplySet.Quantity = 0;
                listSupplySupplierOrder.Remove(supplyExisting);
            }
            ShowOrderSupplies(listSupplySupplierOrder);
            lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
        }

        private BitmapImage GetBitmapImage(byte[] data)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private SupplierOrderSet ObtainSupplierOrderInformation()
        {
            SupplierOrderSet supplierOrder = new SupplierOrderSet();
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            supplierOrder.Supplier_Id = supplierSet.Id;
            supplierOrder.OrderDate = DateTime.Now;
            supplierOrder.EmployeeId = 2;
            supplierOrder.OrderStatusId = 4;
            supplierOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            supplierOrder.TotalAmount = CalculateTotalCost();

            return supplierOrder;
        }

        private void RegisterCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            SupplierOrderSet supplierOrder = ObtainSupplierOrderInformation();
            List<string> errorMessages = CheckSuppliesWithSupplier();

            if (AreValidFields())
            {
                if (errorMessages.Count == 0)
                {
                    OrderSupplierDAO orderSupplierDAO = new OrderSupplierDAO();
                    orderSupplierDAO.AddSupplierOrder(supplierOrder, supplySetList);
                    new AlertPopup("Registro Completado",
                        "Se ha registrado el Pedido a proveedor corectamente",
                        Auxiliary.AlertPopupTypes.Success);
                    listSupplySupplierOrder.Clear();
                    ShowOrderSupplies(listSupplySupplierOrder);
                }
                else
                {
                    string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                    new AlertPopup("Insumos no validos", "Los insumos " + WrongFields
                        + " no estan asociados con el proveedor que selecionaste",
                        Auxiliary.AlertPopupTypes.Warning);
                }
            }
        }

        private void ListBoxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            List<int> SupplierSuppliesID = supplyDAO.GetAllSuppliesBySupplier(supplierSet.Id);
            List<SupplySet> supplySetListAux = new List<SupplySet>();
            foreach (int supplyID in SupplierSuppliesID)
            {
                SupplySet supply = supplySetList.FirstOrDefault(s => s.Id == supplyID);
                if (supply != null)
                {
                    supplySetListAux.Add(supply);
                }
            }

            AddVisualSuppliesToWindow(supplySetListAux);
        }

        private List<string> CheckSuppliesWithSupplier()
        {
            List<string> errorMessages = new List<string>();
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            List<int> SupplierSuppliesID = supplyDAO.GetAllSuppliesBySupplier(supplierSet.Id);
            foreach (SupplySet supply in listSupplySupplierOrder)
            {
                int aux = SupplierSuppliesID.FirstOrDefault(a => a == supply.Id);
                if (aux == 0)
                {
                    errorMessages.Add(supply.Name);
                }
            }

            return errorMessages;
        }

        private void ModifyCustomerOrder_Click(object sender, RoutedEventArgs e)
        {
            SupplierOrderSet supplierOrder = ObtainSupplierOrderInformation();
            List<string> errorMessages = CheckSuppliesWithSupplier();

            if (AreValidFields())
            {
                if (errorMessages.Count == 0)
                {
                    OrderSupplierDAO orderSupplierDAO = new OrderSupplierDAO();
                    orderSupplierDAO.ModifySupplierOrder(supplierOrder, listSupplySupplierOrder);
                    new AlertPopup("Modificación Completada",
                        "Se ha modificado correctamente el Pedido a proveedor",
                        Auxiliary.AlertPopupTypes.Success);
                    NavigationService.Navigate(new GUI_SuppliersModule());
                }
                else
                {
                    string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                    new AlertPopup("Insumos no validos", "Los insumos " + WrongFields
                        + " no estan asociados con el proveedor que selecionaste",
                        Auxiliary.AlertPopupTypes.Warning);
                }
            }

        }

        private bool AreValidFields()
        {
            bool result = true;

            if (lboOrderStatus.SelectedItem != null)
            {

            }

            if (lboSuppliers.SelectedItem != null)
            {

            }

            return result;
        }
    }
}
