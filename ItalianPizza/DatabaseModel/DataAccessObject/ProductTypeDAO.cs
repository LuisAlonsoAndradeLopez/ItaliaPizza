using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Collections.Generic;
using System.Linq;

namespace ItalianPizza.DatabaseModel.DataAccessObject
{
    public class ProductTypeDAO
    {
        public List<ProductTypeSet> GetAllProductTypes()
        {
            List<ProductTypeSet> productTypes = new List<ProductTypeSet>();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productTypes = context.ProductTypeSet.ToList();
            }

            return productTypes;
        }

        public ProductTypeSet GetProductTypeById(int productTypeId)
        {
            ProductTypeSet productType = new ProductTypeSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productType = context.ProductTypeSet.Where(pt => pt.Id == productTypeId).FirstOrDefault();
            }

            return productType;
        }

        public ProductTypeSet GetProductTypeByName(string productTypeName)
        {
            ProductTypeSet productType = new ProductTypeSet();

            using (var context = new ItalianPizzaServerBDEntities())
            {
                productType = context.ProductTypeSet.Where(pt => pt.Type == productTypeName).FirstOrDefault();
            }

            return productType;
        }
    }
}
