
using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using ItalianPizza.XAMLViews.CustomerOrder;
using ItalianPizza.XAMLViews.Suppliers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;
using ListBox = System.Windows.Controls.ListBox;
using Path = System.IO.Path;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_CustomerOrderManagementForm.xaml
    /// </summary>
    public partial class GUI_CustomerOrderManagementForm : Page
    {
        private CustomerOrdersDAO customerOrdersDAO;
        private CustomerSet customerSet;
        private DeliveryDriverSet deliveryDriverSet;
        private List<ProductSaleSet> productSaleSets;
        private List<ProductSaleSet> productsCustomerOrderList;
        private List<ProductSaleSet> listProductsCustomerOrderCopy;
        private CustomerOrderSet customerOrderSet;
        private ProductDAO productDAO;
        private ImageManager imageManager;

        public GUI_CustomerOrderManagementForm(CustomerOrderSet customerOrder)
        {
            InitializeComponent();
            CustomerOrderToken.ActivateTransaction();
            customerOrderSet = customerOrder;
            imageManager = new ImageManager();
            InitializeDAOConnections();
            ShowActiveProducts();
            InitializeListBoxes(customerOrder);
            GetCustomerOrderInformation(customerOrder);
        }

        private void ListBox_ProductTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            ProductTypeSet productType = (ProductTypeSet)lboProductType.SelectedItem;
            List<ProductSaleSet> filteredProducts = productSaleSets
                .Where(p => p.ProductTypeId == productType.Id).ToList();
            AddVisualProductsToWindow(filteredProducts);
        }

        private void ListBox_ProductStatusSelection(object sender, SelectionChangedEventArgs e)
        {
            ProductStatusSet productStatus = (ProductStatusSet)lboProductStatus.SelectedItem;
            List<ProductSaleSet> filteredProducts = productSaleSets
                .Where(p => p.ProductStatusId == productStatus.Id).ToList();
            AddVisualProductsToWindow(filteredProducts);
        }

        private void TextBox_ProductSearch(object sender, EventArgs e)
        {
            string textSearch = txtProductSearch.Text;
            RecoverProducts(textSearch); 
        }

        private void RecoverProducts(string textSearch)
        {
            List<ProductSaleSet> filteredProducts = productSaleSets
                .Where(p => p.Name.ToLower().Contains(textSearch.ToLower())).ToList();
            AddVisualProductsToWindow(filteredProducts);
        }

        private void GetCustomerOrderInformation(CustomerOrderSet customerOrder)
        {
            try
            {
                if (customerOrderSet != null)
                {
                    grdVirtualWindowOrderDataForm.Visibility = Visibility.Hidden;
                    lboOrderTypeCustomer.SelectedItem = customerOrderSet.OrderTypeSet;
                    GetOrderProductsActually(customerOrder);
                }
                else
                {
                    productsCustomerOrderList = new List<ProductSaleSet>();
                    listProductsCustomerOrderCopy = new List<ProductSaleSet>();
                }
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la conexion a la base de datos," +
                    " intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (Exception)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la base de datos, verifique que " +
                    "los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void GetOrderProductsActually(CustomerOrderSet customerOrder)
        {
            productsCustomerOrderList = productDAO.GetOrderProducts(customerOrder);
            listProductsCustomerOrderCopy = new List<ProductSaleSet>();
            foreach (ProductSaleSet product in productsCustomerOrderList)
            {
                ProductSaleSet productSaleSet = new ProductSaleSet
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    PricePerUnit = product.PricePerUnit,
                    ProductStatusId = product.ProductStatusId,
                    EmployeeId = product.EmployeeId,
                    Quantity = product.Quantity,
                    Recipee = product.Recipee,
                };
                listProductsCustomerOrderCopy.Add(productSaleSet);
            }
            ShowOrderProducts(productsCustomerOrderList);
            lblTotalOrderCost.Content = $"$ {CalculateTotalCost()}.00";
            btnModifyCustomerOrder.Visibility = Visibility.Visible;
            btnRegisterCustomerOrder.Visibility = Visibility.Hidden;
        }

        private void InitializeDAOConnections()
        {
            customerOrdersDAO = new CustomerOrdersDAO();
            productDAO = new ProductDAO();
        }

        private void InitializeListBoxes(CustomerOrderSet customerOrder)
        {
            try
            {
                EmployeePositionSet employeePosition = UserToken.GetEmployeePosition();

                if(employeePosition.Position == "Mesero")
                {
                    txtCustomers.Visibility = Visibility.Collapsed;
                    txtDeliverymen.Visibility = Visibility.Collapsed;
                    grdButtonAddDeliveryMan.Visibility = Visibility.Collapsed;
                    grdButtonAddCustomer.Visibility = Visibility.Collapsed;
                    lblDeliverymen.Visibility = Visibility.Collapsed;
                    lblCustomers.Visibility = Visibility.Collapsed;
                    lboOrderTypeCustomer.IsEnabled = false;
                }

                lboOrderTypeCustomer.ItemsSource = customerOrdersDAO.GetOrderTypes();
                lboProductStatus.ItemsSource = productDAO.GetAllProductStatuses();
                lboProductType.ItemsSource = productDAO.GetAllProductTypes();
                lboOrderTypeCustomer.DisplayMemberPath = "Type";
                lboProductStatus.DisplayMemberPath = "Status";
                lboProductType.DisplayMemberPath = "Type";
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la conexion a la base de datos," +
                    " intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (Exception)
            {
                new AlertPopup("Error con la base de datos", 
                    "Lo siento, pero a ocurrido un error con la base de datos, " +
                    "verifique que los datos que usted ingresa no esten corrompidos!", 
                    Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ListBox_CustomerOrderTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
            if (orderType.Type == "Pedido Domicilio")
            {
                txtCustomers.Visibility = Visibility.Visible;
                txtDeliverymen.Visibility = Visibility.Visible;
                grdButtonAddDeliveryMan.Visibility = Visibility.Visible;
                grdButtonAddCustomer.Visibility = Visibility.Visible;
                lblDeliverymen.Visibility = Visibility.Visible;
                lblCustomers.Visibility = Visibility.Visible;
            }
            else
            {
                txtCustomers.Visibility = Visibility.Collapsed;
                txtDeliverymen.Visibility = Visibility.Collapsed;
                grdButtonAddDeliveryMan.Visibility = Visibility.Collapsed;
                grdButtonAddCustomer.Visibility = Visibility.Collapsed;
                lblDeliverymen.Visibility = Visibility.Collapsed;
                lblCustomers.Visibility = Visibility.Collapsed;
            }
        }

        public void MouseLeftButtonUp_AddDeliveryMan(object sender, MouseButtonEventArgs e)
        {
            DeliveryDriverForm deliveryDriverForm = new DeliveryDriverForm()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(1090, 7, 0, 0)
            };
            Grid.SetColumn(deliveryDriverForm, 0);
            Background.Children.Add(deliveryDriverForm);
            deliveryDriverForm.SelectDeliveryDriverEvent += (s, args) => RecoverSelectedDeliveryDriver(sender, e, deliveryDriverForm);
        }

        private void RecoverSelectedDeliveryDriver(object sender, EventArgs e, DeliveryDriverForm deliveryDriverForm)
        {
            deliveryDriverSet = deliveryDriverForm.GetSelectDeliveryDriver();
            txtDeliverymen.Text = " " + deliveryDriverSet.Names + " " + deliveryDriverSet.LastName + " " + deliveryDriverSet.SecondLastName;
            Background.Children.Remove(deliveryDriverForm);
        }

        public void MouseLeftButtonUp_AddCustomer(object sender, MouseButtonEventArgs e)
        {
            CustomerForm customerForm = new CustomerForm()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(1090, 7, 0, 0)
            };
            Grid.SetColumn(customerForm, 0);
            Background.Children.Add(customerForm);
            customerForm.SelectCustomerEvent += (s, args) => RecoverSelectedCustomer(sender, e, customerForm);
        }

        private void RecoverSelectedCustomer(object sender, EventArgs e, CustomerForm customerForm)
        {
            customerSet = customerForm.GetSelectCustomer();
            txtCustomers.Text = " " + customerSet.Names + " " + customerSet.LastName + " " + customerSet.SecondLastName
                + Environment.NewLine + " " +customerSet.AddressSet.StreetName + ", #" + customerSet.AddressSet.StreetNumber 
                + ", " + customerSet.AddressSet.City + ", " + customerSet.AddressSet.Colony;
            Background.Children.Remove(customerForm);
        }

        private void Button_RegisterOrderClient(object sender, RoutedEventArgs e)
        {
            if (ValidateOrderDetails())
            {
                CustomerOrderSet customerOrder = PrepareCustomerOrder();
                try
                {
                    customerOrdersDAO.RegisterCustomerOrder(customerOrder, 
                        productsCustomerOrderList, customerSet, deliveryDriverSet);
                    productsCustomerOrderList.Clear();
                    CleanFields();
                    new AlertPopup("Registro Exitoso", "Se ha registrado " +
                        "correctamente el pedido a la base de datos", Auxiliary.AlertPopupTypes.Success);
                    ;
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos", 
                        "Lo siento, pero a ocurrido un error con la conexion a la base de datos," +
                        " intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
                }
                catch (InvalidOperationException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, " +
                        "pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
            }
        }

        private bool ValidateOrderDetails()
        {
            if (productsCustomerOrderList.Count == 0)
            {
                ShowAlert("Informacion incompleta", "El pedido del cliente debe tener al" +
                    " menos un pedido registrado, verifique si el pedido cumple con esto e inténtelo de nuevo!");
                return false;
            }

            EmployeePositionSet employeePosition = UserToken.GetEmployeePosition();
            if (employeePosition.Position == "Mesero")
            {
                return true;
            }

            if (lboOrderTypeCustomer.SelectedIndex == -1) 
            {
                ShowAlert("Sin tipo de pedido", "Por favor verifique que se haya selecionado el tipo de pedido");
                return false;
            }

            if (customerOrderSet == null)
            {
                CustomerOrderSet customerOrder = PrepareCustomerOrder();
                OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
                if (orderType == null && customerOrder.OrderStatusId != 15)
                {
                    ShowAlert("Informacion incompleta", "Verifique que el tipo de pedido haya sido seleccionado, gracias!");
                    return false;
                }

                if (customerOrder.OrderTypeId == 1 && (customerSet == null || deliveryDriverSet == null) && customerOrder.OrderStatusId != 15)
                {
                    ShowAlert("Informacion incompleta", "Verifique que el cliente y el repartido del pedido haya sido seleccionado, gracias!");
                    return false;
                }
            }
            else
            {
                OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
                if (orderType == null && customerOrderSet.OrderStatusId != 15)
                {
                    ShowAlert("Informacion incompleta", "Verifique que el tipo de pedido haya sido seleccionado, gracias!");
                    return false;
                }

                if (customerOrderSet.OrderTypeId == 1 && (customerSet == null || deliveryDriverSet == null) && customerOrderSet.OrderStatusId != 15)
                {
                    ShowAlert("Informacion incompleta", "Verifique que el cliente y el repartido del pedido haya sido seleccionado, gracias!");
                    return false;
                }
            }

            return true;
        }

        private void ShowAlert(string title, string message)
        {
            new AlertPopup(title, message, Auxiliary.AlertPopupTypes.Warning);
        }

        private void CleanFields()
        {
            productsCustomerOrderList.Clear();
            ShowOrderProducts(productsCustomerOrderList);
            lblTotalOrderCost.Content = "$ 0.00";
            ShowActiveProducts();
        }

        private CustomerOrderSet PrepareCustomerOrder()
        {
            CustomerOrderSet customerOrder = new CustomerOrderSet();
            EmployeePositionSet employeePosition = UserToken.GetEmployeePosition();
            if (employeePosition.Position != "Mesero")
            {
                OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
                customerOrder.OrderTypeId = orderType.Id;
            }
            else
            {
                customerOrder.OrderTypeId = 2;
            } 
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = CalculateTotalCost();
            customerOrder.EmployeeId = UserToken.GetEmployeeID();

            return customerOrder;
        }

        public void ShowOrderProducts(List<ProductSaleSet> orderProducts)
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
                    FontSize = 18,
                    Margin = new Thickness(400, 0, 0, 0),
                };
                grdContainer.Children.Add(lblCostProduct);

                Label lblAmountProduct = new Label
                {
                    Content = product.Quantity,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Margin = new Thickness(300, 0, 0, 0),
                };
                grdContainer.Children.Add(lblAmountProduct);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpCustomerOrderProducts.Children.Add(scrollViewer);
        }

        private double CalculateTotalCost()
        {
            double totalOrderCost = 0;

            foreach (var product in productsCustomerOrderList)
            {
                totalOrderCost += ((double)product.Quantity * product.PricePerUnit);
            }

            return totalOrderCost;
        }

        private void ShowActiveProducts()
        {
            List<ProductSaleSet> productsSale;

            try
            {
                productsSale = productDAO.GetAllActiveProducts();
                productSaleSets = productsSale;
                AddVisualProductsToWindow(productsSale);
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                    "la conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                    Auxiliary.AlertPopupTypes.Error);
            }
            catch (Exception)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un" +
                    " error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                    Auxiliary.AlertPopupTypes.Error);
            }

        }

        private void AddVisualProductsToWindow(List<ProductSaleSet> products)
        {
            wpProducts.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = "";
            string imagePath = "";

            foreach (var product in products)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(55, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 145,
                    Width = 768,
                    RadiusX = 30,
                    RadiusY = 30,
                    Fill = new SolidColorBrush(Color.FromRgb(181, 59, 59)),
                    Margin = new Thickness(-45, 0, 0, 0),
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

                if(imageManager.CheckProductImagePath(product.Id))
                {
                    relativePath = $"..\\TempCache\\Products\\{product.Id}.png";
                    imagePath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));
                    Image imgPhotoProduct = new Image
                    {
                        Height = 120,
                        Width = 120,
                        Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.Fill,
                        Margin = new Thickness(-665, 0, 0, 0),
                    };
                    grdContainer.Children.Add(imgPhotoProduct);
                }

                Label lblNameCustomerOrder = new Label
                {
                    Content = product.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 20,
                    Margin = new Thickness(120, 15, 0, 0),
                };
                grdContainer.Children.Add(lblNameCustomerOrder);

                Label lblOrderCostCustomer = new Label
                {
                    Content = "$ " + product.PricePerUnit + ".00",
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                    FontWeight = FontWeights.SemiBold,
                    FontSize = 19,
                    Margin = new Thickness(120, 42, 0, 0),
                };
                grdContainer.Children.Add(lblOrderCostCustomer);

                Image imgAddProductIcon = new Image
                {
                    Height = 40,
                    Width = 40,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Agregar.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(640, 50, 0, 0),
                };

                imgAddProductIcon.MouseLeftButtonUp += (sender, e) => AddProductSaleToOrder(product);

                grdContainer.Children.Add(imgAddProductIcon);

                Image imgReduceProductIcon = new Image
                {
                    Height = 40,
                    Width = 40,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Disminuir.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(540, 50, 0, 0),
                };

                imgReduceProductIcon.MouseLeftButtonUp += (sender, e) => RemoveProductSaleToOrder(product);

                grdContainer.Children.Add(imgReduceProductIcon);
                stackPanelContainer.Children.Add(grdContainer);
            }

            scrollViewer.Content = stackPanelContainer;
            wpProducts.Children.Add(scrollViewer);
        }

        private void AddProductSaleToOrder(ProductSaleSet productSale)
        {
            RecipeDAO recipeDAO = new RecipeDAO();
            List<RecipeDetailsSet> ingredients = recipeDAO.GetRecipeDetailsByProductSale(productSale);

            bool isRecipee = (bool)productSale.Recipee;
            int decreaseResult = isRecipee ? productDAO.DecreaseSuppliesOnSale(ingredients) : productDAO.DecreaseProductOnSale(productSale);

            if (decreaseResult != -1)
            {
                ProductSaleSet productExisting = productsCustomerOrderList.FirstOrDefault(p => p.Id == productSale.Id);
                if (productExisting == null)
                {
                    productSale.Quantity = 1;
                    productsCustomerOrderList.Add(productSale);
                }
                else
                {
                    productExisting.Quantity++;
                }

                ShowOrderProducts(productsCustomerOrderList);
                lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
            }
            else
            {
                string alertTitle = isRecipee ? "Faltan ingredientes" : "Faltan Productos";
                string alertMessage = isRecipee ? "Lo siento, pero no nos alcanza los ingredientes para otro producto más de este tipo" : "Lo siento, pero no nos alcanza la cantidad de productos de este tipo";
                new AlertPopup(alertTitle, alertMessage, Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void RemoveProductSaleToOrder(ProductSaleSet productSale)
        {
            RecipeDAO recipeDAO = new RecipeDAO();
            List<RecipeDetailsSet> ingredients = recipeDAO.GetRecipeDetailsByProductSale(productSale);
            ProductSaleSet productExisting = productsCustomerOrderList.FirstOrDefault(p => p.Name == productSale.Name);

            if (productExisting != null)
            {
                if (productExisting.Quantity > 1)
                {
                    productExisting.Quantity--;
                }
                else
                {
                    productSale.Quantity = 0;
                    productsCustomerOrderList.Remove(productExisting);
                }

                try
                {
                    if ((bool)productExisting.Recipee)
                    {
                        productDAO.RestoreSuppliesOnSale(ingredients);
                    }
                    else
                    {
                        productDAO.RestoreProductOnSale(productSale);
                    }

                    ShowOrderProducts(productsCustomerOrderList);
                    lblTotalOrderCost.Content = $"$ {CalculateTotalCost()}.00";
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                        "la conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                        Auxiliary.AlertPopupTypes.Error);
                }
                catch (Exception)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un" +
                        " error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
            }
            else
            {
                new AlertPopup("Producto no agregado al pedido", 
                    $"Lo siento, pero no se puede disminuir un producto que no exite en el pedido!", 
                    Auxiliary.AlertPopupTypes.Error);
            }
        }

        private List<RecipeDetailsSet> GetRecipeIngredientsByProduct(ProductSaleSet productSale)
        {
            RecipeDAO recipeDAO = new RecipeDAO();
            List<RecipeDetailsSet> recipeDetails = null;

            try
            {
                recipeDetails = recipeDAO.GetRecipeDetailsByProductSale(productSale);

                foreach (var recipeDetail in recipeDetails)
                {
                    recipeDetail.Quantity *= productSale.Quantity;
                }
            }
            catch (EntityException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error" +
                    " con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error" +
                    " con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }

            return recipeDetails;
        }

        private void GoToConsultCustomerOrdersVirtualWindow(object sender, RoutedEventArgs e)
        {
            VerifyUnconfirmedProducts();
            CustomerOrderToken.DeactivateTransaction();
            NavigationService.Navigate(new GUI_ConsultCustomerOrder());
        }

        private void Button_ModifyCustomerOrder(object sender, RoutedEventArgs e)
        {
            if (productsCustomerOrderList.Count > 0)
            {
                try
                {
                    customerOrdersDAO.ModifyCustomerOrder(customerOrderSet, productsCustomerOrderList);
                    new AlertPopup("Modificación exitosa", "Se han registrado correctamente la modificacion del pedido", Auxiliary.AlertPopupTypes.Success);
                    CustomerOrderToken.DeactivateTransaction();
                    NavigationService.Navigate(new GUI_ConsultCustomerOrder());
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error " +
                        "con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
                }
                catch (Exception)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con " +
                        "la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
                }
            }
        }

        private void VerifyUnconfirmedProducts()
        {
            try
            {
                foreach (ProductSaleSet product in productsCustomerOrderList)
                {
                    ProductSaleSet correspondingProductInCopy = listProductsCustomerOrderCopy.FirstOrDefault(p => p.Id == product.Id);
                    int differenceInAmount = product.Quantity - (correspondingProductInCopy?.Quantity ?? 0);

                    if (differenceInAmount != 0)
                    {
                        ProductSaleSet adjustedProduct = new ProductSaleSet
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Quantity = differenceInAmount
                        };

                        if ((bool)product.Recipee)
                        {
                            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(adjustedProduct);
                            productDAO.RestoreSuppliesOnSale(ingredients);
                        }
                        else
                        {
                            int step = differenceInAmount > 0 ? 1 : -1;
                            for (int i = 0; i != differenceInAmount; i += step)
                            {
                                productDAO.RestoreProductOnSale(product);
                            }
                        }
                    }
                }

                foreach (ProductSaleSet product in listProductsCustomerOrderCopy)
                {
                    if (!productsCustomerOrderList.Any(p => p.Id == product.Id))
                    {
                        if ((bool)product.Recipee)
                        {
                            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(product);
                            productDAO.DecreaseSuppliesOnSale(ingredients);
                        }
                        else
                        {
                            for (int i = 0; i < product.Quantity; i++)
                            {
                                productDAO.RestoreProductOnSale(product);
                            }
                        }
                    }
                }
            }
            catch (EntityException)
            {
                ShowDatabaseErrorAlert("conexion a la base de datos");
            }
            catch (Exception)
            {
                ShowDatabaseErrorAlert("verifique que los datos que usted ingresa no estén corrompidos");
            }
        }

        private void ShowDatabaseErrorAlert(string errorMessage)
        {
            new AlertPopup("Error con la base de datos", $"Lo siento, pero ha ocurrido un error con la {errorMessage}, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
        }

    }
}