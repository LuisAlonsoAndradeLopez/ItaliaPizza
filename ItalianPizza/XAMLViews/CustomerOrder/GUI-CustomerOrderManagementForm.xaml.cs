using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Globalization;
using System.IO;
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

namespace ItalianPizza.XAMLViews
{
    /// <summary>
    /// Lógica de interacción para GUI_CustomerOrderManagementForm.xaml
    /// </summary>
    public partial class GUI_CustomerOrderManagementForm : Page
    {
        private CustomerOrdersDAO customerOrdersDAO;
        private List<ProductSaleSet> listProductsCustomerOrder;
        private List<ProductSaleSet> listProductsCustomerOrderCopy;
        private ProductDAO productDAO;
        private UserDAO userDAO;
        public GUI_CustomerOrderManagementForm(List<ProductSaleSet> customerOrderProducts)
        {
            InitializeComponent();
            listProductsCustomerOrder = customerOrderProducts;
            InitializeDAOConnections();
            ShowActiveProducts();
            InitializeListBoxes();
            if (customerOrderProducts.Count > 0)
            {
                listProductsCustomerOrderCopy = customerOrderProducts;
                ShowOrderProducts(listProductsCustomerOrder);
                btnModifyCustomerOrder.Visibility = Visibility.Visible;
                btnRegisterCustomerOrder.Visibility = Visibility.Hidden;
            }
            else
            {
                listProductsCustomerOrderCopy = new List<ProductSaleSet>();
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
            lboCustomers.ItemsSource = userDAO.GetAllCustomers();
            lboDeliverymen.ItemsSource = userDAO.GetAllDeliveryDriver();
            lboOrderStatusCustomer.ItemsSource = customerOrdersDAO.GetOrderStatuses();
            lboOrderTypeCustomer.ItemsSource = customerOrdersDAO.GetOrderTypes();
            ProductStatusDAO productStatusDAO = new ProductStatusDAO();
            ProductTypeDAO productTypeDAO = new ProductTypeDAO();
            lboProductStatus.ItemsSource = productStatusDAO.GetAllProductStatuses();
            lboProductType.ItemsSource = productTypeDAO.GetAllProductTypes();
            UpdateListBoxItems(lboCustomers);
            UpdateListBoxItems(lboDeliverymen);
            lboOrderStatusCustomer.DisplayMemberPath = "Status";
            lboOrderTypeCustomer.DisplayMemberPath = "Type";
            lboProductStatus.DisplayMemberPath = "Status";
            lboProductType.DisplayMemberPath = "Type";
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
            if (listProductsCustomerOrder.Count != 0)
            {
                CustomerOrderSet customerOrder = PrepareCustomerOrder();
                CustomerSet customer = (CustomerSet)lboCustomers.SelectedItem;
                DeliveryDriverSet deliveryman = (DeliveryDriverSet)lboDeliverymen.SelectedItem;
                customerOrdersDAO.RegisterCustomerOrder(customerOrder, listProductsCustomerOrder, customer, deliveryman);
                //Alert.MostrarMensaje("Se ha registrado correctamente el pedido en la base de datos");
                listProductsCustomerOrder.Clear();
                CleanFields();
            }
            else
            {

            }
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
            OrderStatusSet orderStatus = (OrderStatusSet)lboOrderStatusCustomer.SelectedItem;
            OrderTypeSet orderType = (OrderTypeSet)lboOrderTypeCustomer.SelectedItem;
            customerOrder.OrderTypeSet = orderType;
            customerOrder.OrderStatusSet = orderStatus;
            customerOrder.OrderStatusId = orderStatus.Id;
            customerOrder.OrderTypeId = orderType.Id;
            customerOrder.OrderDate = DateTime.Now;
            customerOrder.RegistrationTime = TimeSpan.ParseExact(DateTime.Now.ToString("HH\\:mm\\:ss"),
                "hh\\:mm\\:ss", CultureInfo.InvariantCulture);
            customerOrder.TotalAmount = CalculateTotalCost();
            customerOrder.EmployeeId = 1;

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
                AddVisualProductsToWindow(productsSale);
            }
            catch (EntityException)
            {
                //Alert.ShowDatabaseConnectionErrorMessage();
            }
            catch (InvalidOperationException)
            {
                //Alert.ShowInvalidDataErrorMessage();
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
            productSale.Quantity = 1;
            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(productSale);

            if (productDAO.DecreaseSuppliesOnSale(ingredients) != -1)
            {
                ProductSaleSet productExisting = listProductsCustomerOrder.FirstOrDefault(p => p.Id == productSale.Id);

                if (productExisting == null)
                {
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
                new AlertPopup("Sin Rercusos", "Lo siento, pero no nos alcansa, bye", Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void RemoveProductSaleToOrder(ProductSaleSet productSale)
        {
            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(productSale);

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

        private List<RecipeDetailsSet> GetRecipeIngredientsByProduct(ProductSaleSet productSale)
        {
            List<RecipeDetailsSet> recipeDetails = new List<RecipeDetailsSet>();

            foreach (var recipeDetail in productSale.RecipeSet.RecipeDetailsSet)
            {
                RecipeDetailsSet details = recipeDetail as RecipeDetailsSet;
                details.Quantity *= productSale.Quantity;
                recipeDetails.Add(details);
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

        private void VerifyUnconfirmedProducts()
        {
            if (listProductsCustomerOrder.Count > 0 && listProductsCustomerOrderCopy.Count == 0)
            {
                foreach (var product in listProductsCustomerOrder)
                {
                    List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(product);
                    productDAO.RestoreSuppliesOnSale(ingredients);
                }
            }
            else if (listProductsCustomerOrder.Count != 0 && listProductsCustomerOrderCopy.Count != 0)
            {
                CompareProductListsAndRestoreSupplies();
            }
        }

        private void CompareProductListsAndRestoreSupplies()
        {
            // Comparar productos nuevos y productos con cantidades diferentes
            foreach (ProductSaleSet product in listProductsCustomerOrder)
            {
                ProductSaleSet correspondingProductInCopy = listProductsCustomerOrderCopy.FirstOrDefault(p => p.Id == product.Id);
                if (correspondingProductInCopy == null)
                {
                    // Producto nuevo en listProductsCustomerOrder
                    List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(product);
                    productDAO.RestoreSuppliesOnSale(ingredients);
                }
                else
                {
                    int differenceInAmount = product.Quantity - correspondingProductInCopy.Quantity;
                    if (differenceInAmount != 0)
                    {
                        // Ajustar cantidades en base a la diferencia
                        ProductSaleSet adjustedProduct = new ProductSaleSet
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Quantity = differenceInAmount
                        };

                        if (differenceInAmount > 0)
                        {
                            // Ajustar suministros si la cantidad aumentó
                            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(adjustedProduct);
                            productDAO.RestoreSuppliesOnSale(ingredients);
                        }
                        else
                        {
                            // Reducir suministros si la cantidad disminuyó
                            adjustedProduct.Quantity *= -1; // Hacer la cantidad negativa para DecreaseSuppliesOnSale
                            List<RecipeDetailsSet> ingredients = GetRecipeIngredientsByProduct(adjustedProduct);
                            productDAO.DecreaseSuppliesOnSale(ingredients);
                        }
                    }
                }
            }
        }
    }
}
