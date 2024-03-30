using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductStatusDAO
    {
        public List<ProductStatusSet> GetAllProductStatuses()
        {
            List<ProductStatusSet> productStatuses = new List<ProductStatusSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productStatuses = context.ProductStatusSet.ToList();
            }

            return productStatuses;
        }

        public ProductStatusSet GetProductStatusById(int productStatusId)
        {
            ProductStatusSet productStatus = new ProductStatusSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productStatus = context.ProductStatusSet.Where(ps => ps.Id == productStatusId).FirstOrDefault();
            }

            return productStatus;
        }

        public ProductStatusSet GetProductStatusByName(string productStatusName)
        {
            ProductStatusSet productStatus = new ProductStatusSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productStatus = context.ProductStatusSet.Where(ps => ps.Status == productStatusName).FirstOrDefault();
            }

            return productStatus;
        }
    }
}
