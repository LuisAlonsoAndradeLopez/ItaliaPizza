using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Window = System.Windows.Window;
using System.Collections.Generic;


namespace ItalianPizza.XAMLViews.CustomerOrder
{
    /// <summary>
    /// Lógica de interacción para GUI_UpdateOrderStatusForm.xaml
    /// </summary>
    public partial class GUI_UpdateOrderStatusForm : UserControl
    {
        private CustomerOrdersDAO customerOrdersDAO;
        private UserDAO userDAO;
        private ProductDAO productDAO;
        private CustomerOrderSet customerOrder;
        public GUI_UpdateOrderStatusForm(CustomerOrderSet customerOrderSet)
        {
            InitializeComponent();
            userDAO = new UserDAO();
            productDAO = new ProductDAO();
            customerOrdersDAO = new CustomerOrdersDAO();
            InitializeFieldFill(customerOrderSet);
            customerOrder = customerOrderSet;
        }

        private bool CheckStatusChange(CustomerOrderSet customerOrder, int orderStatusID)
        {
            bool isValid = true;

            if((customerOrder.OrderStatusId - orderStatusID) > 1)
            {
                isValid = false;
            }

            return isValid;
        }

        private void InitializeFieldFill(CustomerOrderSet customerOrder)
        {
            lblOrderCustomerName.Content = customerOrder.OrderTypeSet.Type + " #" + customerOrder.Id;
            if (customerOrder.OrderTypeSet.Type == "Pedido Domicilio")
            {
                try
                {
                    CustomerSet customer = userDAO.GetCustomerByCustomerOrder(customerOrder.Id);
                    DeliveryDriverSet deliveryman = userDAO.GetDeliveryDriverByCustomerOrder(customerOrder.Id);
                    lblFullNameCustomer.Content = customer.Names + " " + customer.LastName + " " + customer.SecondLastName;
                    lblNameCompleteDeliveryman.Content = deliveryman.Names + " " + deliveryman.LastName + " " + deliveryman.SecondLastName;
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
                lblNameCompleteDeliveryman.Content = "Sin repartidor Asignado";
                lblFullNameCustomer.Content = "Sin cliente Asignado";
            }
        }

        private void ChangeOrderStatusToSent(object sender, MouseButtonEventArgs e)
        {
            if (CheckStatusChange(customerOrder, 4))
            {
                try
                {
                    customerOrdersDAO.ModifyOrderStatus(customerOrder.Id, 7);
                    new AlertPopup("Actualización del estado del pedido",
                        "Se actualizó correctamente el estado del pedido",
                        Auxiliary.AlertPopupTypes.Success);
                    CloseForm();
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
                new AlertPopup("Cambio no Valido",
                        "Lo siento pero ese cambio no se puede realizar",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToCanceled(object sender, MouseButtonEventArgs e)
        {
            if (CheckStatusChange(customerOrder, 6))
            {
                try
                {
                    CancelOrder();
                    customerOrdersDAO.ModifyOrderStatus(customerOrder.Id, 6);
                    new AlertPopup("Cancelacion de pedio completada",
                       "Se a cancelado correctamente el pedido ",
                       Auxiliary.AlertPopupTypes.Success);
                    CloseForm();
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
                new AlertPopup("Cambio no Valido",
                        "Lo siento pero ese cambio no se puede realizar",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void CancelOrder()
        {
            if (customerOrder.OrderStatusId != 6)
            {
                try
                {
                    List<ProductSaleSet> orderProducts = productDAO.GetOrderProducts(customerOrder);
                    customerOrdersDAO.CancelCustomerOrder(customerOrder);
                    if (customerOrder.OrderStatusId == 1)
                    {
                        foreach (var productSale in orderProducts)
                        {
                            if ((bool)productSale.Recipee)
                            {
                                productDAO.RestoreSuppliesOnSale(GetRecipeIngredientsByProduct(productSale));
                            }
                        }
                    }

                    foreach (var productSale in orderProducts)
                    {
                        if (!(bool)productSale.Recipee)
                        {
                            for (int i = 0; i < productSale.Quantity; i++)
                            {
                                productDAO.RestoreProductOnSale(productSale);
                            }
                        }
                    }
                }
                catch (EntityException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la " +
                        "conexion a la base de datos, intentelo mas tarde por favor, gracias!",
                        Auxiliary.AlertPopupTypes.Error);
                }
                catch (InvalidOperationException)
                {
                    new AlertPopup("Error con la base de datos", "Lo siento, pero a ocurrido un error con la" +
                        " base de datos, verifique que los datos que usted ingresa no esten corrompidos!",
                        Auxiliary.AlertPopupTypes.Error);
                }
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

        private void ChangeOrderStatusToReady(object sender, MouseButtonEventArgs e)
        {
            if (CheckStatusChange(customerOrder, 3))
            {
                try
                {
                    customerOrdersDAO.ModifyOrderStatus(customerOrder.Id, 3);
                    new AlertPopup("Actualización del estado del pedido",
                        "Se actualizó correctamente el estado del pedido",
                        Auxiliary.AlertPopupTypes.Success);
                    CloseForm();
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
                new AlertPopup("Cambio no Valido",
                        "Lo siento pero ese cambio no se puede realizar",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToInKitchen(object sender, MouseButtonEventArgs e)
        {
            if (CheckStatusChange(customerOrder, 2))
            {
                try
                {
                    customerOrdersDAO.ModifyOrderStatus(customerOrder.Id, 2);
                    new AlertPopup("Actualización del estado del pedido",
                        "Se actualizó correctamente el estado del pedido",
                        Auxiliary.AlertPopupTypes.Success);
                    CloseForm();
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
                new AlertPopup("Cambio no Valido",
                        "Lo siento pero ese cambio no se puede realizar",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void ChangeOrderStatusToOnHold(object sender, MouseButtonEventArgs e)
        {
            if (CheckStatusChange(customerOrder, 1))
            {
                try
                {
                    customerOrdersDAO.ModifyOrderStatus(customerOrder.Id, 1);
                    new AlertPopup("Actualización del estado del pedido",
                        "Se actualizó correctamente el estado del pedido",
                        Auxiliary.AlertPopupTypes.Success);
                    CloseForm();
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
                new AlertPopup("Cambio no Valido",
                        "Lo siento pero ese cambio no se puede realizar",
                        Auxiliary.AlertPopupTypes.Error);
            }
        }

        private void CloseForm()
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            Frame framePrincipal = mainWindow.FindName("frameContainer") as Frame;
            framePrincipal.Navigate(new GUI_ConsultCustomerOrder());
        }

        private void ReturnWindow(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as Panel;
            if (parent != null)
            {
                parent.Children.Remove(this);
            }
        }

        private void btnOrderDelivered_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
