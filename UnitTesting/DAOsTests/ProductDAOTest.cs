using ItalianPizza.DatabaseModel.DataAccessObject;
using ItalianPizza.DatabaseModel.DatabaseMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Transactions;

namespace DAOsTests
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using (var scope = new TransactionScope())
            {
                scope.Complete();
            }
        }

        [TestMethod]
        public void AddProductTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = productDAO.AddProduct(successProductSaleSet1);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void DisableProductTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string productName = "Success Product 1";
                int result = productDAO.DisableProduct(productName);

                Assert.AreEqual(1, result);
            }
        }

        [TestMethod]
        public void GetAllActiveProductsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ProductSaleSet> activeProducts = productDAO.GetAllActiveProducts();

                Assert.IsNotNull(activeProducts);
                Assert.IsTrue(activeProducts.Count > 0);
            }
        }

        [TestMethod]
        public void GetProductByNameTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string productName = "Success Product 1";
                ProductSaleSet product = productDAO.GetProductByName(productName);

                Assert.IsNotNull(product);
            }
        }

        [TestMethod]
        public void GetProductByNameTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidProductName = "Failed Product";
                ProductSaleSet product = productDAO.GetProductByName(invalidProductName);

                Assert.IsNull(product);
            }
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string textForFindingArticle = "Success Product 1";
                string findByType = "Nombre";
                List<ProductSaleSet> specifiedProducts = productDAO.GetSpecifiedProductsByNameOrCode(textForFindingArticle, findByType);

                Assert.IsNotNull(specifiedProducts);
            }
        }

        [TestMethod]
        public void GetSpecifiedProductsByNameOrCodeTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidTextForFindingArticle = "Failed Product";
                string findByType = "Tipo Inválido";
                List<ProductSaleSet> specifiedProducts = productDAO.GetSpecifiedProductsByNameOrCode(invalidTextForFindingArticle, findByType);

                Assert.IsNull(specifiedProducts);
            }
        }

        [TestMethod]
        public void ModifyProductTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                int result = productDAO.ModifyProduct(successProductSaleSet2, successProductSaleSet3);

                Assert.IsTrue(result > 0);
            }
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string productCode = "99999999";
                bool result = productDAO.TheCodeIsAlreadyRegistred(productCode);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void TheCodeIsAlreadyRegistredTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidProductCode = "9999";
                bool result = productDAO.TheCodeIsAlreadyRegistred(invalidProductCode);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string productName = "Success Product 1";
                bool result = productDAO.TheNameIsAlreadyRegistred(productName);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void TheNameIsAlreadyRegistredTestFail()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                string invalidProductName = "Failed Product";
                bool result = productDAO.TheNameIsAlreadyRegistred(invalidProductName);

                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void GetOrderProductsTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                CustomerOrderSet customerOrder = new CustomerOrderSet();
                List<ProductSaleSet> orderProducts = productDAO.GetOrderProducts(customerOrder);

                Assert.IsNotNull(orderProducts);
            }
        }

        [TestMethod]
        public void GetAllProductTypesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ProductTypeSet> productTypes = productDAO.GetAllProductTypes();

                Assert.IsNotNull(productTypes);
                Assert.IsTrue(productTypes.Count > 0);
            }
        }

        [TestMethod]
        public void GetAllProductStatusesTest()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                List<ProductStatusSet> productStatuses = productDAO.GetAllProductStatuses();

                Assert.IsNotNull(productStatuses);
                Assert.IsTrue(productStatuses.Count > 0);
            }
        }
    }
}