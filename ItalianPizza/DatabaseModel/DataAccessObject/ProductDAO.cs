using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductDAO
    {
        public int AddProduct(Producto product)
        {
            int result = 0;

            try
            {
                using (var context = new ItalianPizzaServerBDEntities())
                {
                    context.ProductoSet.Add(product);
                    context.SaveChanges();
                }

                result = 1;
            }
            catch (EntityException ex)
            {
                throw new EntityException("Operación no válida al acceder a la base de datos.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Operación no válida al acceder a la base de datos.", ex);
            }

            return result;
        }

        public List<Producto> GetAllActiveProducts()
        {
            List<Producto> activeProducts = new List<Producto>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                activeProducts = context.ProductoSet.ToList();
            }

            return activeProducts;
        }

        public List<Producto> GetSpecifiedProductsByNameOrCode(string textForFindingArticle, string findByType)
        {
            List<Producto> specifiedProducts = new List<Producto>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                if (findByType == "Nombre")
                {
                    specifiedProducts = context.ProductoSet.Where(p => p.Nombre.StartsWith(textForFindingArticle)).ToList();
                }

                if (findByType == "Código")
                {
                    //specifiedProducts = context.ProductoSet.Where(p => p.Código.StartsWith(textForFindingArticle)).ToList();
                }
            }

            return specifiedProducts;
        }
    }
}
