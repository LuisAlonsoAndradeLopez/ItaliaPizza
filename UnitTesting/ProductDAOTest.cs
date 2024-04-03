using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductDAOTests
    {
        private static ProductDAO productDAO;
        private static ProductSaleSet successProductSaleSet1;
        private static ProductSaleSet successProductSaleSet2;
        private static ProductSaleSet successProductSaleSet3;
        private static ProductSaleSet failedProductSaleSet;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            productDAO = new ProductDAO();
            successProductSaleSet1 = new ProductSaleSet
            {
                Id = 10001,
                Name = "Success Product 1",
                Quantity = 1,
                PricePerUnit = 1,
                Picture = new byte[1],
                ProductStatusId = 1,
                ProductTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "99999999",
                Description = "None"
            };

            successProductSaleSet2 = new ProductSaleSet
            {
                Id = 10002,
                Name = "Success Product 2",
                Quantity = 2,
                PricePerUnit = 2,
                Picture = new byte[2],
                ProductStatusId = 1,
                ProductTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "9999999",
                Description = "Nada"
            };

            successProductSaleSet3 = new ProductSaleSet
            {
                Id = 10003,
                Name = "Success Product 2",
                Quantity = 3,
                PricePerUnit = 3,
                Picture = new byte[3],
                ProductStatusId = 1,
                ProductTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "99999",
                Description = "Nadona"
            };

            failedProductSaleSet = new ProductSaleSet
            {
                Id = 10004,
                Name = "Failed Product",
                Quantity = 4,
                PricePerUnit = 4,
                Picture = new byte[4],
                ProductStatusId = 1,
                ProductTypeId = 1,
                EmployeeId = 1,
                IdentificationCode = "9999",
                Description = "No"
            };

            productDAO.AddProduct(successProductSaleSet2);
            productDAO.AddProduct(successProductSaleSet3);
            productDAO.AddProduct(failedProductSaleSet);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            productDAO.DeleteProduct(successProductSaleSet1);
            productDAO.DeleteProduct(successProductSaleSet2);
            productDAO.DeleteProduct(successProductSaleSet3);
            productDAO.DeleteProduct(failedProductSaleSet);
        }

        [TestMethod]
        public void AddProductTest()
        {            
            int result = productDAO.AddProduct(successProductSaleSet1);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddProductTestFail()
        {            
            Assert.ThrowsException<EntityException>(() => productDAO.AddProduct(failedProductSaleSet));
        }

        [TestMethod]
        public void DisableProductTest()
        {            
            string productName = "Success Product 1"; 
            int result = productDAO.DisableProduct(productName);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DisableProductTestFail()
        {            
            string invalidProductName = "Failed Product"; 

            Assert.ThrowsException<EntityException>(() => productDAO.DisableProduct(invalidProductName));
        }

        [TestMethod]
        public void GetAllActiveProductsTest()
        {            
            List<ProductSaleSet> activeProducts = productDAO.GetAllActiveProducts();

            Assert.IsNotNull(activeProducts);
            Assert.IsTrue(activeProducts.Count > 0);
        }

        [TestMethod]
        public void GetAllActiveProductsTestFail()
        {         
            Assert.ThrowsException<EntityException>(() => productDAO.GetAllActiveProducts());
        }

        [TestMethod]
        public void GetImageByProductNameTest()
        {
            //string productName = "Success Product 1"; 
            //BitmapImage bitmapImage = productDAO.GetImageByProductName(productName);
            //
            //Assert.IsNotNull(bitmapImage);
        }

        [TestMethod]
        public void GetImageByProductNameTestFail()
        {
            //string invalidProductName = "Failed Product"; 
            //
            //Assert.ThrowsException<ArgumentNullException>(() => productDAO.GetImageByProductName(invalidProductName));
        }

        [TestMethod]
        public void GetProductByNameTest()
        {            
            string productName = "Success Product 1"; 
            ProductSaleSet product = productDAO.GetProductByName(productName);

            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void GetProductByNameTestFail()
        {            
            string invalidProductName = "Failed Product"; 

            Assert.ThrowsException<EntityException>(() => productDAO.GetProductByName(invalidProductName));
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTest()
        {            
            string textForFindingArticle = "Success Product 1";
            string findByType = "Nombre"; 
            var specifiedProducts = productDAO.GetSpecifiedProductsByNameOrCode(textForFindingArticle, findByType);

            Assert.IsNotNull(specifiedProducts);
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTestFail()
        {            
            string invalidTextForFindingArticle = "Failed Product"; 
            string findByType = "Tipo Inválido"; 

            Assert.ThrowsException<ArgumentNullException>(() => productDAO.GetSpecifiedProductsByNameOrCode(invalidTextForFindingArticle, findByType));
        }

        [TestMethod]
        public void ModifyProductTest()
        {            
            int result = productDAO.ModifyProduct(successProductSaleSet2, successProductSaleSet3);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ModifyProductTestFail()
        {            
            Assert.ThrowsException<EntityException>(() => productDAO.ModifyProduct(successProductSaleSet3, failedProductSaleSet));
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {            
            string productCode = "99999999";
            bool result = productDAO.TheCodeIsAlreadyRegistred(productCode);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {            
            string invalidProductCode = "9999";

            Assert.ThrowsException<ArgumentNullException>(() => productDAO.TheCodeIsAlreadyRegistred(invalidProductCode));
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {            
            string productName = "Success Product 1";
            bool result = productDAO.TheNameIsAlreadyRegistred(productName);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {            
            string invalidProductName = "Failed Product";

            Assert.ThrowsException<ArgumentNullException>(() => productDAO.TheNameIsAlreadyRegistred(invalidProductName));
        }

        [TestMethod]
        public void GetOrderProductsTest()
        {
            CustomerOrderSet customerOrder = new CustomerOrderSet();
            List<ProductSaleSet> orderProducts = productDAO.GetOrderProducts(customerOrder);

            Assert.IsNotNull(orderProducts);
        }

        [TestMethod]
        public void GetOrderProductsTestFail()
        {
            CustomerOrderSet invalidCustomerOrder = new CustomerOrderSet();

            Assert.ThrowsException<EntityException>(() => productDAO.GetOrderProducts(invalidCustomerOrder));
        }

        [TestMethod]
        public void GetAllProductTypesTest()
        {
            List<ProductTypeSet> productTypes = productDAO.GetAllProductTypes();

            Assert.IsNotNull(productTypes);
            Assert.IsTrue(productTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllProductTypesTestFail()
        {
            Assert.ThrowsException<EntityException>(() => productDAO.GetAllProductTypes());
        }

        [TestMethod]
        public void GetAllProductStatusesTest()
        {            
            List<ProductStatusSet> productStatuses = productDAO.GetAllProductStatuses();

            Assert.IsNotNull(productStatuses);
            Assert.IsTrue(productStatuses.Count > 0);
        }

        [TestMethod]
        public void GetAllProductStatusesTestFail()
        {         
            Assert.ThrowsException<EntityException>(() => productDAO.GetAllProductStatuses());
        }
    }
}