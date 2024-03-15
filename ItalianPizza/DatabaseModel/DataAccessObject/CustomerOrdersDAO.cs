using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class CustomerOrdersDAO
    {
        public CustomerOrdersDAO() { }

        public List<OrdenCliente> GetAllCustomerOrders()
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                orders = context.OrdenClienteSet.ToList();
            }

            return orders;
        }

        public List<OrdenCliente> GetCustomerOrdersByDate(String date)
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                orders = context.OrdenClienteSet.Where(o => o.Fecha == date).ToList();
            }

            return orders;
        }

        public List<OrdenCliente> GetCustomerOrdersByStatus(String status)
        {
            List<OrdenCliente> orders = new List<OrdenCliente>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                orders = context.OrdenClienteSet.Where(o => o.Estado == status).ToList();
            }

            return orders;
        }


    }
}
