
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Label = System.Windows.Controls.Label;
using ListBox = System.Windows.Controls.ListBox;

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_CustomerOrderManagementForm.xaml
    /// </summary>
    public partial class GUI_CustomerOrderManagementForm : Page
    {
        private CustomerOrdersDAO customerOrdersDAO;
        private List<ProductSaleSet> productSaleSets;
        private List<ProductSaleSet> listProductsCustomerOrder;
        private List<ProductSaleSet> listProductsCustomerOrderCopy;
        private CustomerOrderSet customerOrderSet;
        private ProductDAO productDAO;
        private UserDAO userDAO;
        public GUI_CustomerOrderManagementForm(CustomerOrderSet customerOrder)
        {
            InitializeComponent();
            customerOrderSet = customerOrder;
            InitializeDAOConnections();
            ShowActiveProducts();
            InitializeListBoxes();
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
                    CustomerSet customer = userDAO.GetCustomerByCustomerOrder(customerOrder.Id);
                    DeliveryDriverSet deliveryman = userDAO.GetDeliveryDriverByCustomerOrder(customerOrder.Id);
                    lboCustomers.SelectedItem = customer;
                    lboDeliverymen.SelectedItem = deliveryman;
                    lboOrderStatusCustomer.SelectedItem = customerOrderSet.OrderStatusSet;
                    lboOrderTypeCustomer.SelectedItem = customerOrderSet.OrderTypeSet;
                    listProductsCustomerOrder = productDAO.GetOrderProducts(customerOrder);
                    listProductsCustomerOrderCopy = new List<ProductSaleSet>();
                    foreach (ProductSaleSet product in listProductsCustomerOrder)
                    {
                        ProductSaleSet productSaleSet = new ProductSaleSet
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            PricePerUnit = product.PricePerUnit,
                            ProductStatusId = product.ProductStatusId,
                            EmployeeId = product.EmployeeId,
                            Quantity = product.Quantity
                        };
                        listProductsCustomerOrderCopy.Add(productSaleSet);
                    }
                    ShowOrderProducts(listProductsCustomerOrder);
                    btnModifyCustomerOrder.Visibility = Visibility.Visible;
                    btnRegisterCustomerOrder.Visibility = Visibility.Hidden;
                }
                else
                {
                    listProductsCustomerOrder = new List<ProductSaleSet>();
                    listProductsCustomerOrderCopy = new List<ProductSaleSet>();
                }
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

        private void InitializeDAOConnections()
        {
            customerOrdersDAO = new CustomerOrdersDAO();
            productDAO = new ProductDAO();
            userDAO = new UserDAO();
        }

        private void InitializeListBoxes()
        {
            try
            {
                lboCustomers.ItemsSource = userDAO.GetAllCustomers();
                lboDeliverymen.ItemsSource = userDAO.GetAllDeliveryDriver();
                lboOrderStatusCustomer.ItemsSource = customerOrdersDAO.GetOrderStatuses();
                lboOrderTypeCustomer.ItemsSource = customerOrdersDAO.GetOrderTypes();
                lboProductStatus.ItemsSource = productDAO.GetAllProductStatuses();
                lboProductType.ItemsSource = productDAO.GetAllProductTypes();
                UpdateListBoxItems(lboCustomers);
                UpdateListBoxItems(lboDeliverymen);
                lboOrderStatusCustomer.DisplayMemberPath = "Status";
                lboOrderTypeCustomer.DisplayMemberPath = "Type";
                lboProductStatus.DisplayMemberPath = "Status";
                lboProductType.DisplayMemberPath = "Type";
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

        private void UpdateListBoxItems(ListBox listBox)
        {
            listBox.ItemContainerGenerator.StatusChanged += (sender, e) =>
            {
                if (listBox.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                {
                    foreach (var item in listBox.Items)
                    {
                        var container = listBox.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                        if (container != null)
                        {
                            var names = item.GetType().GetProperty("Names").GetValue(item, null);
                            var lastName = item.GetType().GetProperty("LastName").GetValue(item, null);
                            var secondLastName = item.GetType().GetProperty("SecondLastName").GetValue(item, null);
                            var fullName = $"{names} {lastName} {secondLastName}";
                            container.Content = fullName;
                        }
                    }
                }
            };
        }

        private void ListBox_CustomerOrderTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
            if (orderType.Type == "Pedido Domicilio")
            {
                lboCustomers.Visibility = Visibility.Visible;
                lboDeliverymen.Visibility = Visibility.Visible;
                grdButtonAddDeliveryMan.Visibility = Visibility.Visible;
                grdButtonAddCustomer.Visibility = Visibility.Visible;
                lblDeliverymen.Visibility = Visibility.Visible;
                lblCustomers.Visibility = Visibility.Visible;
            }
            else
            {
                lboCustomers.Visibility = Visibility.Collapsed;
                lboDeliverymen.Visibility = Visibility.Collapsed;
                grdButtonAddDeliveryMan.Visibility = Visibility.Collapsed;
                grdButtonAddCustomer.Visibility = Visibility.Collapsed;
                lblDeliverymen.Visibility = Visibility.Collapsed;
                lblCustomers.Visibility = Visibility.Collapsed;
            }
        }

        public void MouseLeftButtonUp_AddDeliveryMan(object sender, MouseButtonEventArgs e)
        {
            grdVirtualWindowDeliveryCustomerRegistrationForm.Visibility = Visibility.Visible;
        }

        public void MouseLeftButtonUp_AddCustomer(object sender, MouseButtonEventArgs e)
        {
            grdVirtualWindowDeliveryCustomerRegistrationForm.Visibility = Visibility.Visible;
            grdVitualWindowAddressForm.Visibility = Visibility.Visible;
        }

        private void Button_RegisterOrderClient(object sender, RoutedEventArgs e)
        {
            if (ValidateOrderDetails())
            {
                CustomerOrderSet customerOrder = PrepareCustomerOrder();
                CustomerSet customer = (CustomerSet)lboCustomers.SelectedItem;
                DeliveryDriverSet deliveryman = (DeliveryDriverSet)lboDeliverymen.SelectedItem;

                try
                {
                    customerOrdersDAO.RegisterCustomerOrder(customerOrder, listProductsCustomerOrder, customer, deliveryman);
                    new AlertPopup("Registro Exitoso", "Se ha registrado correctamente el pedido a la base de datos", Auxiliary.AlertPopupTypes.Success);
                    listProductsCustomerOrder.Clear();
                    CleanFields();
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

        private bool ValidateOrderDetails()
        {
            if (listProductsCustomerOrder.Count == 0)
            {
                ShowAlert("Informacion incompleta", "El pedido del cliente debe tener al menos un pedido registrado, verifique si el pedido cumple con esto e inténtelo de nuevo!");
                return false;
            }

            if(customerOrderSet == null)
            {
                CustomerOrderSet customerOrder = PrepareCustomerOrder();
                OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
                if (orderType == null && customerOrder.OrderStatusId != 15)
                {
                    ShowAlert("Informacion incompleta", "Verifique que el tipo de pedido haya sido seleccionado, gracias!");
                    return false;
                }

                CustomerSet customer = (CustomerSet)lboCustomers.SelectedItem;
                DeliveryDriverSet deliveryman = (DeliveryDriverSet)lboDeliverymen.SelectedItem;

                if (customerOrder.OrderTypeId == 1 && (customer == null || deliveryman == null) && customerOrder.OrderStatusId != 15)
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

                CustomerSet customer = (CustomerSet)lboCustomers.SelectedItem;
                DeliveryDriverSet deliveryman = (DeliveryDriverSet)lboDeliverymen.SelectedItem;

                if (customerOrderSet.OrderTypeId == 1 && (customer == null || deliveryman == null) && customerOrderSet.OrderStatusId != 15)
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
            listProductsCustomerOrder.Clear();
            ShowOrderProducts(listProductsCustomerOrder);
            lblTotalOrderCost.Content = "$ 0.00";
            ShowActiveProducts();
        }

        private CustomerOrderSet PrepareCustomerOrder()
        {
            CustomerOrderSet customerOrder = new CustomerOrderSet();
            OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
            customerOrder.OrderStatusId = 1;
            customerOrder.OrderTypeId = orderType.Id;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = CalculateTotalCost();
            customerOrder.EmployeeId = 1;

            return customerOrder;
        }

        private void PrepareModifyCustomerOrder()
        {
            OrderStatusSet orderStatus = (OrderStatusSet)lboOrderStatusCustomer.SelectedItem;
            OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
            customerOrderSet.OrderStatusId = orderStatus.Id;
            customerOrderSet.OrderTypeId = orderType.Id;
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

            foreach (var product in listProductsCustomerOrder)
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
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
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

                Image imgPhotoProduct = new Image
                {
                    Height = 120,
                    Width = 120,
                    Source = GetBitmapImage(product.Picture),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-665, 0, 0, 0),
                };
                grdContainer.Children.Add(imgPhotoProduct);

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

            if (productDAO.DecreaseSuppliesOnSale(ingredients) != -1)
            {
                ProductSaleSet productExisting = listProductsCustomerOrder.FirstOrDefault(p => p.Id == productSale.Id);
                if (productExisting == null)
                {
                    productSale.Quantity = 1;
                    listProductsCustomerOrder.Add(productSale);
                }
                else
                {
                    productExisting.Quantity++;
                }

                ShowOrderProducts(listProductsCustomerOrder);
                lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
            }
            else
            {
                new AlertPopup("Faltan ingredientes", "Lo siento, pero no nos alcanza los ingredientes para otro producto mas de este tipo", Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void RemoveProductSaleToOrder(ProductSaleSet productSale)
        {
            RecipeDAO recipeDAO = new RecipeDAO();

            try
            {
                List<RecipeDetailsSet> ingredients = recipeDAO.GetRecipeDetailsByProductSale(productSale);

                ProductSaleSet productExisting = listProductsCustomerOrder.FirstOrDefault(p => p.Name == productSale.Name);
                if (productExisting != null && productExisting.Quantity > 1)
                {
                    productExisting.Quantity--;
                    productDAO.RestoreSuppliesOnSale(ingredients);
                }
                else if (productExisting != null && productExisting.Quantity == 1)
                {
                    productSale.Quantity = 0;
                    listProductsCustomerOrder.Remove(productExisting);
                    productDAO.RestoreSuppliesOnSale(ingredients);
                }
                ShowOrderProducts(listProductsCustomerOrder);
                lblTotalOrderCost.Content = "$ " + CalculateTotalCost() + ".00";
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
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la conexion a la base de datos, intentelo mas tarde por favor, gracias!", Auxiliary.AlertPopupTypes.Error);
            }
            catch (InvalidOperationException)
            {
                new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la base de datos, verifique que los datos que usted ingresa no esten corrompidos!", Auxiliary.AlertPopupTypes.Error);
            }

            return recipeDetails;
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

        private void GoToConsultCustomerOrdersVirtualWindow(object sender, RoutedEventArgs e)
        {
            VerifyUnconfirmedProducts();
            NavigationService.Navigate(new GUI_ConsultCustomerOrder());
        }

        private void Button_AddCustomer(object sender, RoutedEventArgs e)
        {
            grdVirtualWindowDeliveryCustomerRegistrationForm.Visibility = Visibility.Collapsed;
            grdVitualWindowAddressForm.Visibility = Visibility.Collapsed;
        }

        private void Button_ModifyCustomerOrder(object sender, RoutedEventArgs e)
        {
            if (ValidateOrderDetails())
            {
                OrderStatusSet orderStatus = (OrderStatusSet)lboOrderStatusCustomer.SelectedItem;
                if (orderStatus.Id != 15)
                {
                    PrepareModifyCustomerOrder();
                    CustomerSet customer = (CustomerSet)lboCustomers.SelectedItem;
                    DeliveryDriverSet deliveryman = (DeliveryDriverSet)lboDeliverymen.SelectedItem;

                    try
                    {
                        customerOrdersDAO.ModifyCustomerOrder(customerOrderSet, listProductsCustomerOrder, customer, deliveryman);
                        new AlertPopup("Modificación exitosa", "Se han registrado correctamente la modificacion del pedido", Auxiliary.AlertPopupTypes.Success);
                        NavigationService.Navigate(new GUI_ConsultCustomerOrder());
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
                    CancelOrder();
                }
            }
        }

        private void CancelOrder()
        {
            if(customerOrderSet.OrderStatusId == 2)
            {
                customerOrderSet.OrderStatusId = 15;

                try
                {
                    List<ProductSaleSet> orderProducts = productDAO.GetOrderProducts(customerOrderSet);
                    customerOrdersDAO.CancelCustomerOrder(customerOrderSet);
                    foreach (var productSale in orderProducts)
                    {
                        productDAO.RestoreSuppliesOnSale(GetRecipeIngredientsByProduct(productSale));
                    }
                    new AlertPopup("Cancelacion de pedio completada", "Se a cancelado correctamente el pedido", Auxiliary.AlertPopupTypes.Success);
                    NavigationService.Navigate(new GUI_ConsultCustomerOrder());
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
                new AlertPopup("No se puede cancelar este pedio", "Una vez que entra a cocina un pedido, no se puede modificar", Auxiliary.AlertPopupTypes.Warning);
            }

        }

        private void VerifyUnconfirmedProducts()
        {
            try
            {
                foreach (ProductSaleSet product in listProductsCustomerOrder)
                {
                    ProductSaleSet correspondingProductInCopy = listProductsCustomerOrderCopy.FirstOrDefault(p => p.Id == product.Id);
                    if (correspondingProductInCopy == null)
                    {
                        List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(product);
                        productDAO.RestoreSuppliesOnSale(ingredients);
                    }
                    else
                    {
                        int differenceInAmount = product.Quantity - correspondingProductInCopy.Quantity;
                        if (differenceInAmount != 0)
                        {
                            ProductSaleSet adjustedProduct = new ProductSaleSet
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Quantity = differenceInAmount
                            };

                            if (differenceInAmount > 0)
                            {
                                List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(adjustedProduct);
                                productDAO.RestoreSuppliesOnSale(ingredients);
                            }
                            else
                            {
                                adjustedProduct.Quantity *= -1;
                                List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(adjustedProduct);
                                productDAO.DecreaseSuppliesOnSale(ingredients);
                            }
                        }
                    }
                }

                foreach (ProductSaleSet product in listProductsCustomerOrderCopy)
                {
                    if (!listProductsCustomerOrder.Any(p => p.Id == product.Id))
                    {
                        List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(product);
                        productDAO.DecreaseSuppliesOnSale(ingredients);
                    }
                }
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
