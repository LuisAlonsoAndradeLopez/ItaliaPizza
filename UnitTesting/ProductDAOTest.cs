using ItalianPizza.DatabaseModel.DatabaseMapping;
using System.Data.Entity.Core;

namespace ItalianPizza.DatabaseModel.DataAccessObject.Tests
{
    [TestClass]
    public class ProductDAOTests
    {
        [TestMethod]
        public void AddProductTest()
        {
            var dao = new ProductDAO();
            var product = new ProductSaleSet(); 
            var result = dao.AddProduct(product);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddProductTestFail()
        {
            var dao = new ProductDAO();
            var invalidProduct = new ProductSaleSet(); 

            Assert.ThrowsException<EntityException>(() => dao.AddProduct(invalidProduct));
        }

        [TestMethod]
        public void DisableProductTest()
        {
            var dao = new ProductDAO();
            var productName = "ValidProductName"; 
            var result = dao.DisableProduct(productName);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DisableProductTestFail()
        {
            var dao = new ProductDAO();
            var invalidProductName = "InvalidProductName"; 

            Assert.ThrowsException<EntityException>(() => dao.DisableProduct(invalidProductName));
        }

        [TestMethod]
        public void GetAllActiveProductsTest()
        {
            var dao = new ProductDAO();
            var activeProducts = dao.GetAllActiveProducts();

            Assert.IsNotNull(activeProducts);
            Assert.IsTrue(activeProducts.Count > 0);
        }

        [TestMethod]
        public void GetAllActiveProductsTestFail()
        {
            var dao = new ProductDAO();

            Assert.ThrowsException<EntityException>(() => dao.GetAllActiveProducts());
        }

        [TestMethod]
        public void GetImageByProductNameTest()
        {
            var dao = new ProductDAO();
            var productName = "ValidProductName"; 
            var bitmapImage = dao.GetImageByProductName(productName);

            Assert.IsNotNull(bitmapImage);
        }

        [TestMethod]
        public void GetImageByProductNameTestFail()
        {
            var dao = new ProductDAO();
            var invalidProductName = "InvalidProductName"; 

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetImageByProductName(invalidProductName));
        }

        [TestMethod]
        public void GetProductByNameTest()
        {
            var dao = new ProductDAO();
            var productName = "ValidProductName"; 
            var product = dao.GetProductByName(productName);

            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void GetProductByNameTestFail()
        {
            var dao = new ProductDAO();
            var invalidProductName = "InvalidProductName"; 

            Assert.ThrowsException<EntityException>(() => dao.GetProductByName(invalidProductName));
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTest()
        {
            var dao = new ProductDAO();
            var textForFindingArticle = "ValidText";
            var findByType = "Nombre"; 
            var specifiedProducts = dao.GetSpecifiedProductsByNameOrCode(textForFindingArticle, findByType);

            Assert.IsNotNull(specifiedProducts);
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTestFail()
        {
            var dao = new ProductDAO();
            var invalidTextForFindingArticle = "InvalidText"; 
            var findByType = "InvalidType"; 

            Assert.ThrowsException<ArgumentNullException>(() => dao.GetSpecifiedProductsByNameOrCode(invalidTextForFindingArticle, findByType));
        }

        [TestMethod]
        public void ModifyProductTest()
        {
            var dao = new ProductDAO();
            var originalProduct = new ProductSaleSet(); 
            var modifiedProduct = new ProductSaleSet(); 
            var result = dao.ModifyProduct(originalProduct, modifiedProduct);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ModifyProductTestFail()
        {
            var dao = new ProductDAO();
            var originalProduct = new ProductSaleSet(); 
            var invalidModifiedProduct = new ProductSaleSet(); 

            Assert.ThrowsException<EntityException>(() => dao.ModifyProduct(originalProduct, invalidModifiedProduct));
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {
            var dao = new ProductDAO();
            var productCode = "ValidProductCode";
            var result = dao.TheCodeIsAlreadyRegistred(productCode);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {
            var dao = new ProductDAO();
            var invalidProductCode = "InvalidProductCode";

            Assert.ThrowsException<ArgumentNullException>(() => dao.TheCodeIsAlreadyRegistred(invalidProductCode));
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {
            var dao = new ProductDAO();
            var productName = "ValidProductName";
            var result = dao.TheNameIsAlreadyRegistred(productName);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {
            var dao = new ProductDAO();
            var invalidProductName = "InvalidProductName";

            Assert.ThrowsException<ArgumentNullException>(() => dao.TheNameIsAlreadyRegistred(invalidProductName));
        }

        [TestMethod]
        public void GetOrderProductsTest()
        {
            var dao = new ProductDAO();
            var customerOrder = new CustomerOrderSet();
            var orderProducts = dao.GetOrderProducts(customerOrder);

            Assert.IsNotNull(orderProducts);
        }

        [TestMethod]
        public void GetOrderProductsTestFail()
        {
            var dao = new ProductDAO();
            var invalidCustomerOrder = new CustomerOrderSet();

            Assert.ThrowsException<EntityException>(() => dao.GetOrderProducts(invalidCustomerOrder));
        }

        [TestMethod]
        public void GetAllProductTypesTest()
        {
            var dao = new ProductDAO();
            var productTypes = dao.GetAllProductTypes();

            Assert.IsNotNull(productTypes);
            Assert.IsTrue(productTypes.Count > 0);
        }

        [TestMethod]
        public void GetAllProductTypesTestFail()
        {
            var dao = new ProductDAO();

            Assert.ThrowsException<EntityException>(() => dao.GetAllProductTypes());
        }

        [TestMethod]
        public void GetAllProductStatusesTest()
        {
            var dao = new ProductDAO();
            var productStatuses = dao.GetAllProductStatuses();

            Assert.IsNotNull(productStatuses);
            Assert.IsTrue(productStatuses.Count > 0);
        }

        [TestMethod]
        public void GetAllProductStatusesTestFail()
        {
            var dao = new ProductDAO();

            Assert.ThrowsException<EntityException>(() => dao.GetAllProductStatuses());
        }
    }
}