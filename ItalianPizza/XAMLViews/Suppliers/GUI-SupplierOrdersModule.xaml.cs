using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;
using Label = System.Windows.Controls.Label;
using Path = System.IO.Path;
using TextBox = System.Windows.Controls.TextBox;

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
        private List<SupplierSet> suppliersList;
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
                    if((bool)supplierOrder.IsPaid)
                    {
                        chkPayNow.IsChecked = true;
                    }
                    else
                    {
                        chkPayLater.IsChecked = true;
                    }
                    chkPayLater.IsEnabled = false;
                    chkPayNow.IsEnabled = false;
                    grdGroupModifyButtons.Visibility = Visibility.Visible;
                    grdRegisterButtons.Visibility = Visibility.Hidden;
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
            suppliersList = supplierDAO.GetAllSuppliers();
            lboSuppliers.ItemsSource = suppliersList;
            lboSuppliers.DisplayMemberPath = "CompanyName";
        }

        private void TextBox_SupplierSearch(object sender, EventArgs e)
        {
            string textSearch = txtSupplierSearch.Text;
            FilterSuppliers(textSearch);
        }

        private void FilterSuppliers(string textSearch)
        {
            List<SupplierSet> filteredProducts = suppliersList
                .Where(p => p.CompanyName.ToLower().Contains(textSearch.ToLower())).ToList();
            lboSuppliers.ItemsSource = filteredProducts;
            lboSuppliers.DisplayMemberPath = "CompanyName";
        }

        private void TextBox_ProductSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            FilterSupplies(textSearch);
        }

        private void FilterSupplies(string textSearch)
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

                Image btnDeleteSupply = new Image
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 20,
                    Width = 20,
                    Margin = new Thickness(15, 7, 0, 0),
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Delete.png", UriKind.RelativeOrAbsolute)),
                };

                btnDeleteSupply.MouseLeftButtonUp += (sender, e) => RemoveSupplyToOrder(product);
                grdContainer.Children.Add(btnDeleteSupply);

                Label lblName = new Label
                {
                    Content = product.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(40, 0, 0, 0),
                };
                grdContainer.Children.Add(lblName);

                Label lblUnitMeasurement = new Label
                {
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(200, 0, 0, 0),
                    Width = 105,
                };

                TextBlock txtUnitMeasurement = new TextBlock
                {
                    Text = product.SupplyUnitSet.Unit,
                    TextAlignment = TextAlignment.Center
                };
                lblUnitMeasurement.Content = txtUnitMeasurement;
                grdContainer.Children.Add(lblUnitMeasurement);

                Label lblAmount = new Label
                {
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(-10, 0, 0, 0),
                    Width = 105,
                };

                TextBlock txtAmount = new TextBlock
                {
                    Text = product.Quantity.ToString(),
                    TextAlignment = TextAlignment.Center
                };
                lblAmount.Content = txtAmount;
                grdContainer.Children.Add(lblAmount);
                
                Label lblCost = new Label
                {
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(415, 0, 0, 0),
                    Width = 270,
                };

                TextBlock txtCost = new TextBlock
                {
                    Text = "$ " + product.PricePerUnit.ToString() + ".00",
                    TextAlignment = TextAlignment.Center
                };
                lblCost.Content = txtCost;
                grdContainer.Children.Add(lblCost);

                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpSupplierOrderSupplies.Children.Add(scrollViewer);
        }

        private double CalculateTotalCost()
        {
            double totalOrderCost = 0;

            foreach (var supply in listSupplySupplierOrder)
            {
                totalOrderCost += ((double) supply.PricePerUnit);
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
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    Height = 48,
                    Width = 787,
                    RadiusX = 20,
                    RadiusY = 20,
                    Fill = new SolidColorBrush(Color.FromRgb(137, 162, 91)),
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

                Label lblTitleSupplyName = new Label
                {
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.Bold,
                    Width = 206,
                    FontSize = 20,
                    Margin = new Thickness(-550, 0, 0, 0),
                };

                TextBlock txtTitleSupplyName = new TextBlock
                {
                    Text = supply.Name,
                    TextAlignment = TextAlignment.Center
                };

                lblTitleSupplyName.Content = txtTitleSupplyName;

                grdContainer.Children.Add(lblTitleSupplyName);

                Label lblTitleSupplyUnit = new Label
                {
                    HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.Bold,
                    Width = 127,
                    FontSize = 20,
                    Margin = new Thickness(-205, 0, 0, 0),
                };

                TextBlock txtTitleSupplyUnit = new TextBlock
                {
                    Text = supply.SupplyUnitSet.Unit,
                    TextAlignment = TextAlignment.Center
                };

                lblTitleSupplyUnit.Content = txtTitleSupplyUnit;
                grdContainer.Children.Add(lblTitleSupplyUnit);

                TextBox txtSupplyQuantity = new TextBox
                {
                    Foreground = new SolidColorBrush(Color.FromRgb(24, 25, 26)),
                    FontWeight = FontWeights.SemiBold,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 20,
                    Height = 35,
                    Width = 161,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(115, 0, 0, 0),
                    MaxLength = 9,
                };
                txtSupplyQuantity.PreviewTextInput += TextBox_PreviewTextInput;
                grdContainer.Children.Add(txtSupplyQuantity);

                TextBox txtSupplyCost = new TextBox
                {
                    Foreground = new SolidColorBrush(Color.FromRgb(24, 25, 26)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Height = 35,
                    Width = 161,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(480, 0, 0, 0),
                    MaxLength = 9,
                };
                txtSupplyCost.PreviewTextInput += TextBox_PreviewTextInput;
                grdContainer.Children.Add(txtSupplyCost);

                Image btnAddSupply = new Image
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 34,
                    Width = 145,
                    Margin = new Thickness(730, 7, 0, 0),
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Agregar.png", UriKind.RelativeOrAbsolute)),
                };

                btnAddSupply.MouseLeftButtonUp += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(txtSupplyCost.Text) && !string.IsNullOrEmpty(txtSupplyQuantity.Text))
                    {
                        AddSupplyToOrder(supply, int.Parse(txtSupplyQuantity.Text), int.Parse(txtSupplyCost.Text));
                        txtSupplyQuantity.Text = "";
                        txtSupplyCost.Text = "";
                    }
                    else
                    {
                        new AlertPopup("Campos Inválidos",
                            "Los campos de cantidad y costo de insumo no deben estar vacíos. Verifíquelos, por favor.",
                            AlertPopupTypes.Warning);
                    }
                };

                grdContainer.Children.Add(btnAddSupply);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpSupplies.Children.Add(scrollViewer);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void AddSupplyToOrder(SupplySet supplySet, int quantitySupply, int costSupply)
        {
            SupplySet supplyExisting = listSupplySupplierOrder.FirstOrDefault(p => p.Id == supplySet.Id);
            if (supplyExisting == null)
            {
                supplySet.Quantity = quantitySupply;
                supplySet.PricePerUnit = costSupply;
                listSupplySupplierOrder.Add(supplySet);
            }
            else
            {
                supplyExisting.Quantity += quantitySupply;
                supplyExisting.PricePerUnit += costSupply;
            }

            ShowOrderSupplies(listSupplySupplierOrder);
            lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
        }

        private void RemoveSupplyToOrder(SupplySet supplySet)
        {
            SupplySet supplyExisting = listSupplySupplierOrder.FirstOrDefault(p => p.Name == supplySet.Name);
            listSupplySupplierOrder.Remove(supplyExisting);
            ShowOrderSupplies(listSupplySupplierOrder);
            lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
        }

        private SupplierOrderSet ObtainSupplierOrderInformation()
        {
            SupplierOrderSet supplierOrder = new SupplierOrderSet();
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            supplierOrder.Supplier_Id = supplierSet.Id;
            supplierOrder.OrderDate = DateTime.Now;
            supplierOrder.EmployeeId = UserToken.GetEmployeeID();
            supplierOrder.OrderStatusId = 8;
            supplierOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            if(chkPayNow.IsChecked == true)
            {
                supplierOrder.IsPaid = true;
            }
            else
            {
                supplierOrder.IsPaid= false;
            }

            supplierOrder.TotalAmount = CalculateTotalCost();

            return supplierOrder;
        }

        private void RegisterSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            if (AreValidFields())
            {
                SupplierOrderSet supplierOrder = ObtainSupplierOrderInformation();
                List<string> errorMessages = CheckSuppliesWithSupplier();
                if (errorMessages.Count == 0)
                {
                    OrderSupplierDAO orderSupplierDAO = new OrderSupplierDAO();
                    orderSupplierDAO.AddSupplierOrder(supplierOrder, listSupplySupplierOrder);
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
            else
            {
                new AlertPopup("Datos faltantes",
                        "Por favor verifique el pedido ya tenga selecionado un proveedor y" +
                        "que el tipo de pago ya halla sido selecionado",
                        Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void ListBoxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            if(supplierSet != null)
            {
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
            else
            {
                AddVisualSuppliesToWindow(supplySetList);
            }
            
        }

        private List<string> CheckSuppliesWithSupplier()
        {
            List<string> errorMessages = new List<string>();
            SupplierSet supplierSet = lboSuppliers.SelectedItem as SupplierSet;
            List<int> SupplierSuppliesID = supplyDAO.GetAllSuppliesBySupplier(supplierSet.Id);
            foreach(SupplySet supply in listSupplySupplierOrder)
            {
                int aux = SupplierSuppliesID.FirstOrDefault(a => a ==  supply.Id);
                if(aux == 0)
                {
                    errorMessages.Add(supply.Name);
                }
            }

            return errorMessages;
        }

        private void ModifySupplierOrder_Click(object sender, MouseButtonEventArgs e)
        {
            if (supplierOrderSet.OrderStatusId != 6 && supplierOrderSet.OrderStatusId != 7)
            {
                if (lboSuppliers.SelectedItem != null)
                {
                    List<string> errorMessages = CheckSuppliesWithSupplier();
                    if (errorMessages.Count == 0)
                    {
                        OrderSupplierDAO orderSupplierDAO = new OrderSupplierDAO();
                        orderSupplierDAO.ModifySupplierOrder(supplierOrderSet, listSupplySupplierOrder);
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
                else
                {
                    new AlertPopup("Datos faltantes",
                            "Por favor verifique el pedido ya tenga selecionado un proveedor",
                            Auxiliary.AlertPopupTypes.Warning);
                }
            }
            else
            {
                new AlertPopup("Modificacion no valida",
                            "Los pedidos cancelados o ya entregados, no se pueden modificar",
                            Auxiliary.AlertPopupTypes.Warning);
            }
            
        }

        private bool AreValidFields()
        {
            bool result = true;

            if(lboSuppliers.SelectedItem == null || chkPayNow.IsChecked == false && chkPayLater.IsChecked == false)
            {
                result = false;
            }

            return result;
        }

        private void PaySupplierOrder_Click(object sender, MouseButtonEventArgs e)
        {
            if (supplierOrderSet.OrderStatusId == 7)
            {
                try
                {
                    WithDrawFinancialTransactionSet withDrawFinancialTransaction = new WithDrawFinancialTransactionSet
                    {
                        Description = "Registro del pago de proveedor numero #" + supplierOrderSet.Id + " que se registro el: " + supplierOrderSet.OrderDate,
                        RealizationDate = DateTime.Now,
                        EmployeeId = UserToken.GetEmployeeID(),
                        MonetaryValue = supplierOrderSet.TotalAmount,
                        FinancialTransactionWithDrawContextId = 1
                    };

                    new WithDrawFinancialTransactionDAO().AddWithDrawFinancialTransaction(withDrawFinancialTransaction);
                    new OrderSupplierDAO().ModifyOrderStatus(supplierOrderSet.Id, 5);

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
            else
            {
                new AlertPopup("Error al pagar pedido", "Lo siento, pero los pedidos solamente se pueden pagar si tienen de estado: Entregado.", Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ShowForm_Click(object sender, MouseButtonEventArgs e)
        {
            grdChangeStatusForm.Visibility = Visibility.Visible;
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            grdChangeStatusForm.Visibility = Visibility.Hidden;
        }

        private void ChangeOrderStatusToReady(object sender, MouseButtonEventArgs e)
        {
            if(supplierOrderSet.OrderStatusId != 6 && supplierOrderSet.OrderStatusId != 7)
            {
                new OrderSupplierDAO().ModifyOrderStatus(supplierOrderSet.Id, 7);
                new SupplyDAO().UpdateSupplyInInventory(listSupplySupplierOrder);
                new AlertPopup("Actualización del estado del pedido",
                    "Se actualizó correctamente el estado del pedido",
                    Auxiliary.AlertPopupTypes.Success);
                grdChangeStatusForm.Visibility = Visibility.Hidden;
            }
            else
            {
                new AlertPopup("Cambio no Valido",
                        "Los pedidos cancelados o ya entregados, no se pueden cambiar estado",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToDeliveryOrderToday(object sender, MouseButtonEventArgs e)
        {
            if (supplierOrderSet.OrderStatusId != 6 && supplierOrderSet.OrderStatusId != 7)
            {
                new OrderSupplierDAO().ModifyOrderStatus(supplierOrderSet.Id, 9);
                new AlertPopup("Actualización del estado del pedido",
                    "Se actualizó correctamente el estado del pedido",
                    Auxiliary.AlertPopupTypes.Success);
                grdChangeStatusForm.Visibility = Visibility.Hidden;
            }
            else
            {
                new AlertPopup("Cambio no Valido",
                        "Los pedidos cancelados o ya entregados, no se pueden cambiar estado",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToStandby(object sender, MouseButtonEventArgs e)
        {
            if (supplierOrderSet.OrderStatusId != 6 && supplierOrderSet.OrderStatusId != 7)
            {
                new OrderSupplierDAO().ModifyOrderStatus(supplierOrderSet.Id, 8);
                new AlertPopup("Actualización del estado del pedido",
                    "Se actualizó correctamente el estado del pedido",
                    Auxiliary.AlertPopupTypes.Success);
                grdChangeStatusForm.Visibility = Visibility.Hidden;
            }
            else
            {
                new AlertPopup("Cambio no Valido",
                        "Los pedidos cancelados o ya entregados, no se pueden cambiar estado",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToInShipping(object sender, MouseButtonEventArgs e)
        {
            if (supplierOrderSet.OrderStatusId != 6 && supplierOrderSet.OrderStatusId != 7)
            {
                new OrderSupplierDAO().ModifyOrderStatus(supplierOrderSet.Id, 6);
                new AlertPopup("Actualización del estado del pedido",
                    "Se actualizó correctamente el estado del pedido",
                    Auxiliary.AlertPopupTypes.Success);
                grdChangeStatusForm.Visibility = Visibility.Hidden;
            }
            else
            {
                new AlertPopup("Cambio no Valido",
                        "Los pedidos cancelados o ya entregados, no se pueden cambiar estado",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void BackPage_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new GUI_SuppliersModule());
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == chkPayLater)
            {
                chkPayNow.IsChecked = false;
            }
            else if (sender == chkPayNow)
            {
                chkPayLater.IsChecked = false;
            }
        }
    }
}
