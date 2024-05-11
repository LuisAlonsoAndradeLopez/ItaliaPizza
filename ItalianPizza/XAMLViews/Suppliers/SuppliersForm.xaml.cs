using ItalianPizza.Auxiliary;
using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using ItalianPizza.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace ItalianPizza.XAMLViews.Suppliers
{
    /// <summary>
    /// Lógica de interacción para SuppliersForm.xaml
    /// </summary>
    public partial class SuppliersForm : UserControl
    {
        private SupplierSet supplierSet;
        private SupplyDAO supplyDAO;
        private ProductDAO productDAO;
        private List<SupplySet> supplierProducts;

        public SuppliersForm(SupplierSet supplier)
        {
            InitializeComponent();
            supplyDAO = new SupplyDAO();
            productDAO = new ProductDAO();
            InitializeListBox();
            if (supplier != null )
            {
                supplierSet = supplier;
                IniatializeFields();
                supplierProducts = productDAO.GetSupplierProducts(supplier.Id);
                ShowSupplierSupplyScreen(supplierProducts);
            }
            else
            {
                supplierProducts = new List<SupplySet>();
            }
        }

        public void InitializeListBox()
        {
            lboSupplys.ItemsSource = supplyDAO.GetAllSupplyWithoutPhoto();
            UserDAO userDAO = new UserDAO();
            lboStatus.ItemsSource = userDAO.GetAllUserStatus();
            lboStatus.DisplayMemberPath = "Status";
            lboSupplys.DisplayMemberPath = "Name";
        }

        private void IniatializeFields()
        {
            txtNames.Text = supplierSet.Names;
            txtLastName.Text = supplierSet.LastName;
            txtSecondLastName.Text = supplierSet.SecondLastName;
            txtEmail.Text = supplierSet.Email;
            txtCellNumber.Text = supplierSet.Phone;
            txtCompanyName.Text = supplierSet.CompanyName;
            btnModifySupplier.Visibility = Visibility.Visible;
            btnRegisterSupplier.Visibility = Visibility.Collapsed;
        }

        private void CloseSupplierForm(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Frame framePrincipal = mainWindow.FindName("frameContainer") as Frame;
            framePrincipal.Navigate(new GUI_SuppliersModule());
        }

        private void ShowSupplierSupplyScreen(List<SupplySet> supplierSupplysList)
        {
            wpSupplierProducts.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            };

            StackPanel stackPanelContainer = new StackPanel();

            foreach (SupplySet supply in supplierSupplysList)
            {
                Grid grdContainer = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                Rectangle rectBackground = new Rectangle
                {
                    Height = 45,
                    Width = 350,
                    RadiusX = 25,
                    RadiusY = 25,
                    Margin = new Thickness(35, 0, 0, 0),
                    Fill = new LinearGradientBrush
                    {
                        StartPoint = new System.Windows.Point(0.5, 0),
                        EndPoint = new System.Windows.Point(0.5, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Color.FromRgb(46, 95, 122), 0.107),
                            new GradientStop(Color.FromRgb(39, 16, 23), 0.867)
                        }
                    },
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
                    Height = 24,
                    Width = 24,
                    Source = new BitmapImage(new Uri("\\Resources\\Pictures\\ICON-Eliminar.png", UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.Fill,
                    Margin = new Thickness(-260, 0, 0, 0),
                };

                image.MouseLeftButtonUp += (sender, e) => RemoveSupplyToSupplier(supply);
                grdContainer.Children.Add(image);

                Label lblSupplierName = new Label
                {
                    Content = supply.Name,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 252, 252)),
                    FontWeight = FontWeights.Bold,
                    FontSize = 17,
                    Margin = new Thickness(75, 5, 0, 0),
                };
                grdContainer.Children.Add(lblSupplierName);

                stackPanelContainer.Children.Add(grdContainer);
            }
            scrollViewer.Content = stackPanelContainer;
            wpSupplierProducts.Children.Add(scrollViewer);
        }

        private void RemoveSupplyToSupplier(SupplySet supply)
        {
            supplierProducts.Remove(supply);
            ShowSupplierSupplyScreen(supplierProducts);
        }

        private void RegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorMessages = IsValidFields();
            if (errorMessages.Count < 1)
            {
                SupplierSet supplier = GetSupplierData();
                SupplierDAO supplierDAO = new SupplierDAO();
                try
                {
                    supplierDAO.AddSupplier(supplier, supplierProducts);
                    new AlertPopup("Registro Correcto", "Se ha registrado correctamente el proveedor",
                        Auxiliary.AlertPopupTypes.Success);
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
            else
            {
                string WrongFields = "'" + string.Join("', '", errorMessages) + "'";
                new AlertPopup("Campos invalidos", "Los campos " + WrongFields 
                    + " no deben ser nulos, ni debe tener caracteres especiales", 
                    Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private List<string> IsValidFields()
        {
            List<string> errorMessages = new List<string>();
            ResetColorFields();
            if (!RegexChecker.CheckName(txtNames.Text))
            {
                txtNames.BorderBrush = Brushes.Red;
                errorMessages.Add("'Nombres'");
            }

            if (!RegexChecker.CheckName(txtLastName.Text))
            {
                txtLastName.BorderBrush = Brushes.Red;
                errorMessages.Add("'Apellido Paterno'");
            }

            if (!RegexChecker.CheckName(txtSecondLastName.Text))
            {
                txtSecondLastName.BorderBrush = Brushes.Red;
                errorMessages.Add("'Apellido Materno'");
            }

            if (txtCompanyName.Text.Trim().Length == 0)
            {
                txtCompanyName.BorderBrush = Brushes.Red;
                errorMessages.Add("'Nombre Compania'");
            }

            if (!RegexChecker.CheckPhoneNumber(txtCellNumber.Text))
            {
                txtCellNumber.BorderBrush = Brushes.Red;
                errorMessages.Add("'Numero telefonico'");
            }

            if (!RegexChecker.CheckEmail(txtEmail.Text))
            {
                txtEmail.BorderBrush = Brushes.Red;
                errorMessages.Add("'Email'");
            }

            if(lboStatus.SelectedItem == null)
            {
                lboStatus.BorderBrush = Brushes.Red;
                errorMessages.Add("'Estado usuario'");
            }

            return errorMessages;
        }

        private SupplierSet GetSupplierData()
        {
            SupplierSet supplierSet = new SupplierSet
            {
                Names = txtNames.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                SecondLastName = txtSecondLastName.Text.Trim(),
                CompanyName = txtCompanyName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                UserStatusId = 1,
                Phone = txtCellNumber.Text.Trim(),
                EmployeeId = UserToken.GetEmployeeID()
            };

            return supplierSet;
        }

        private void ResetColorFields()
        {
            txtNames.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtSecondLastName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtLastName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            lboStatus.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtCellNumber.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtCompanyName.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
            txtEmail.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6C6969"));
        }

        private void ModifySupplier_Click(object sender, RoutedEventArgs e)
        {
            List<string> menssagesError = IsValidFields();
            if (menssagesError.Count < 1)
            {
                SupplierSet supplier = GetSupplierData();
                supplier.Id = supplierSet.Id;
                SupplierDAO supplierDAO = new SupplierDAO();
                try
                {
                    supplierDAO.ModifySupplier(supplier, supplierProducts);
                    new AlertPopup("Modificación Correcta", "Se ha registrado correctamente la modificacion del proveedor",
                        Auxiliary.AlertPopupTypes.Success);
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
            else
            {
                string WrongFields = "'" + string.Join("', '", menssagesError) + "'";
                new AlertPopup("Campos invalidos", "Los campos " + WrongFields
                    + " no deben ser nulos, ni debe tener caracteres especiales",
                    Auxiliary.AlertPopupTypes.Warning);
            }
        }

        private void AddSupplyToSupplier(object sender, MouseButtonEventArgs e)
        {
            SupplySet supply = lboSupplys.SelectedItem as SupplySet;
            bool proveedorExistente = supplierProducts.Any(p => p.Id == supply.Id);

            if (!proveedorExistente)
            {
                supplierProducts.Add(supply);
                ShowSupplierSupplyScreen(supplierProducts);
            }
            else
            {
                new AlertPopup("Insumo ya relacionado", "El insumo que usted seleciono, " +
                    "ya se encuentra actualmente relacionado con el proveedor", 
                    AlertPopupTypes.Warning);
            }
        }
    }
}
