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

        public List<ProductSaleSet> GetAllActiveProducts()
        {
            List<ProductSaleSet> activeProducts = new List<ProductSaleSet>();

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    activeProducts = context.ProductSaleSet
                        .Include(product => product.ProductStatusSet)
                        .Include(product => product.ProductTypeSet)
                        .Include(product => product.RecipeSet.RecipeDetailsSet)
                        .ToList();   
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

        public int DecreaseSuppliesOnSale(List<RecipeDetailsSet> recipeIngredients)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in recipeIngredients)
                        {
                            SupplySet supply = context.SupplySet.FirstOrDefault(supplyAux => supplyAux.Id == product.SupplyId);
                            supply.Quantity -= product.Quantity;
                            if (supply.Quantity >= 0)
                            {
                                result = context.SaveChanges();
                            }
                            else
                            {
                                result = -1;
                                break;
                            }
                        }

                        if (result != -1)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (EntityException ex)
                    {
                        transaction.Rollback();
                        throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }

        public int RestoreSuppliesOnSale(List<RecipeDetailsSet> recipeIngredients)
        {
            int result = 0;
            using (var context = new ItalianPizzaServerBDEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in recipeIngredients)
                        {
                            SupplySet supply = context.SupplySet.FirstOrDefault(supplyAux => supplyAux.Id == product.SupplyId);
                            supply.Quantity += product.Quantity;
                            result = context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (EntityException ex)
                    {
                        transaction.Rollback();
                        throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
                    }
                }
            }

            return result;
        }

        public List<ProductSaleSet> GetOrderProducts(CustomerOrderSet customerOrder)
        {
            List<ProductSaleSet> customerOrderProducts;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    var customerOrderDetails = context.CustomerOrderDetailSet
                                            .Where(d => d.CustomerOrderId == customerOrder.Id)
                                            .Include(d => d.ProductSaleSet)
                                            .ToList();

                    customerOrderProducts = customerOrderDetails.Select(detalle =>
                        new ProductSaleSet
                        {
                            Id = detalle.ProductSaleSet.Id,
                            Name = detalle.ProductSaleSet.Name,
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
    }
}
