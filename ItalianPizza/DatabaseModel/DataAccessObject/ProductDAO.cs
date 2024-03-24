using ItalianPizza.DatabaseModel.DatabaseMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductDAO
    {
        public List<Producto> GetAllActiveProducts()
        {
            List<Producto> activeProducts = new List<Producto>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                activeProducts = context.Producto.ToList();
            }

            return activeProducts;
        }
    }
}
