using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductDAO
    {
        public ProductDAO() { }

        public List<ProductSale> GetAllActiveProducts()
        {
            List<ProductSale> activeProducts = new List<ProductSale>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    activeProducts = context.ProductSaleSet.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return activeProducts;
        }

        public List<ProductSale> GetOrderProducts(CustomerOrder customerOrder)
        {
            List<ProductSale> customerOrderProducts;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetails = context.CustomerOrderDetailSet
                                            .Where(d => d.CustomerOrderId == customerOrder.Id)
                                            .Include(d => d.ProductSale)
                                            .ToList();

                    customerOrderProducts = customerOrderDetails.Select(detalle =>
                        new ProductSale
                        {
                            Id = detalle.ProductSale.Id,
                            Name = detalle.ProductSale.Name,
                            Quantity = detalle.ProductQuantity,
                            PricePerUnit = detalle.PricePerUnit
                        }).ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return customerOrderProducts;

        }

        public List<ProductType> GetAllProductTypes()
        {
            List<ProductType> productTypesList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    productTypesList = context.ProductTypeSet.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return productTypesList;
        }

        public List<ProductStatus> GetAllProductStatuses()
        {
            List<ProductStatus> productStatusesList;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    productStatusesList = context.ProductStatusSet.ToList();
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return productStatusesList;
        }
    }
}
