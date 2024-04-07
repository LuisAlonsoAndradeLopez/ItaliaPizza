
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/06/2024 17:51:52
-- Generated from EDMX file: D:\Proyectos C#\ItalianPizza\ItaliaPizza\ItalianPizza\DatabaseModel\DatabaseMapping\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ItalianPizzaServerBD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerSet] DROP CONSTRAINT [FK_CustomerAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerCustomerOrderCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderCustomerSet] DROP CONSTRAINT [FK_CustomerCustomerOrderCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrderCustomerOrderCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderCustomerSet] DROP CONSTRAINT [FK_CustomerOrderCustomerOrderCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrderCustomerOrderDeliveryDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderDeliveryDriverSet] DROP CONSTRAINT [FK_CustomerOrderCustomerOrderDeliveryDriver];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrderCustomerOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderDetailSet] DROP CONSTRAINT [FK_CustomerOrderCustomerOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_DailyClosingFinancialTransaction_DailyClosing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyClosingFinancialTransaction] DROP CONSTRAINT [FK_DailyClosingFinancialTransaction_DailyClosing];
GO
IF OBJECT_ID(N'[dbo].[FK_DailyClosingFinancialTransaction_FinancialTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyClosingFinancialTransaction] DROP CONSTRAINT [FK_DailyClosingFinancialTransaction_FinancialTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryDriverCustomerOrderDeliveryDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderDeliveryDriverSet] DROP CONSTRAINT [FK_DeliveryDriverCustomerOrderDeliveryDriver];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_EmployeeAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerSet] DROP CONSTRAINT [FK_EmployeeCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeCustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderSet] DROP CONSTRAINT [FK_EmployeeCustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDailyClosing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyClosingSet] DROP CONSTRAINT [FK_EmployeeDailyClosing];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeDeliveryDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryDriverSet] DROP CONSTRAINT [FK_EmployeeDeliveryDriver];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeFinancialTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinancialTransactionSet] DROP CONSTRAINT [FK_EmployeeFinancialTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeInventoryValidation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InventoryValidationSet] DROP CONSTRAINT [FK_EmployeeInventoryValidation];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePositionEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_EmployeePositionEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeProductSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSaleSet] DROP CONSTRAINT [FK_EmployeeProductSale];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeRecipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeSet] DROP CONSTRAINT [FK_EmployeeRecipe];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierSet] DROP CONSTRAINT [FK_EmployeeSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSupplierOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierOrderSet] DROP CONSTRAINT [FK_EmployeeSupplierOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeSupply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplySet] DROP CONSTRAINT [FK_EmployeeSupply];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeeUserAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_EmployeeUserAccount];
GO
IF OBJECT_ID(N'[dbo].[FK_FinancialTransactionCustomerOrder_CustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinancialTransactionCustomerOrder] DROP CONSTRAINT [FK_FinancialTransactionCustomerOrder_CustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_FinancialTransactionCustomerOrder_FinancialTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinancialTransactionCustomerOrder] DROP CONSTRAINT [FK_FinancialTransactionCustomerOrder_FinancialTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_FinancialTransactionSupplierOrder_FinancialTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinancialTransactionSupplierOrder] DROP CONSTRAINT [FK_FinancialTransactionSupplierOrder_FinancialTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_FinancialTransactionSupplierOrder_SupplierOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FinancialTransactionSupplierOrder] DROP CONSTRAINT [FK_FinancialTransactionSupplierOrder_SupplierOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderStatusCustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderSet] DROP CONSTRAINT [FK_OrderStatusCustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderStatusSupplierOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierOrderSet] DROP CONSTRAINT [FK_OrderStatusSupplierOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderTypeCustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderSet] DROP CONSTRAINT [FK_OrderTypeCustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductSaleCustomerOrderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerOrderDetailSet] DROP CONSTRAINT [FK_ProductSaleCustomerOrderDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductSaleRecipe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeSet] DROP CONSTRAINT [FK_ProductSaleRecipe];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStatusProductSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSaleSet] DROP CONSTRAINT [FK_ProductStatusProductSale];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductStatusSupply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplySet] DROP CONSTRAINT [FK_ProductStatusSupply];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTypeProductSale]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSaleSet] DROP CONSTRAINT [FK_ProductTypeProductSale];
GO
IF OBJECT_ID(N'[dbo].[FK_RecipeRecipeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeDetailsSet] DROP CONSTRAINT [FK_RecipeRecipeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierOrderSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierOrderSet] DROP CONSTRAINT [FK_SupplierOrderSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierOrderSupplierOrderDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierOrderDetailsSet] DROP CONSTRAINT [FK_SupplierOrderSupplierOrderDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierSupply_Supplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierSupply] DROP CONSTRAINT [FK_SupplierSupply_Supplier];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierSupply_Supply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierSupply] DROP CONSTRAINT [FK_SupplierSupply_Supply];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplyRecipeDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecipeDetailsSet] DROP CONSTRAINT [FK_SupplyRecipeDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplySupplierOrderDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierOrderDetailsSet] DROP CONSTRAINT [FK_SupplySupplierOrderDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplyTypeSupply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplySet] DROP CONSTRAINT [FK_SupplyTypeSupply];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplyUnitSupply]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplySet] DROP CONSTRAINT [FK_SupplyUnitSupply];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStatusCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerSet] DROP CONSTRAINT [FK_UserStatusCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStatusDeliveryDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeliveryDriverSet] DROP CONSTRAINT [FK_UserStatusDeliveryDriver];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStatusEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeSet] DROP CONSTRAINT [FK_UserStatusEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStatusSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierSet] DROP CONSTRAINT [FK_UserStatusSupplier];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AddressSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrderCustomerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrderCustomerSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrderDeliveryDriverSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrderDeliveryDriverSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrderDetailSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrderDetailSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerOrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerOrderSet];
GO
IF OBJECT_ID(N'[dbo].[CustomerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerSet];
GO
IF OBJECT_ID(N'[dbo].[DailyClosingFinancialTransaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyClosingFinancialTransaction];
GO
IF OBJECT_ID(N'[dbo].[DailyClosingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyClosingSet];
GO
IF OBJECT_ID(N'[dbo].[DeliveryDriverSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeliveryDriverSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeePositionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePositionSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[FinancialTransactionCustomerOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialTransactionCustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FinancialTransactionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialTransactionSet];
GO
IF OBJECT_ID(N'[dbo].[FinancialTransactionSupplierOrder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FinancialTransactionSupplierOrder];
GO
IF OBJECT_ID(N'[dbo].[InventoryValidationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InventoryValidationSet];
GO
IF OBJECT_ID(N'[dbo].[OrderStatusSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderStatusSet];
GO
IF OBJECT_ID(N'[dbo].[OrderTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderTypeSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSaleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSaleSet];
GO
IF OBJECT_ID(N'[dbo].[ProductStatusSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductStatusSet];
GO
IF OBJECT_ID(N'[dbo].[ProductTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypeSet];
GO
IF OBJECT_ID(N'[dbo].[RecipeDetailsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecipeDetailsSet];
GO
IF OBJECT_ID(N'[dbo].[RecipeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecipeSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierOrderDetailsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierOrderDetailsSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierOrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierOrderSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierSet];
GO
IF OBJECT_ID(N'[dbo].[SupplierSupply]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierSupply];
GO
IF OBJECT_ID(N'[dbo].[SupplySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplySet];
GO
IF OBJECT_ID(N'[dbo].[SupplyTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplyTypeSet];
GO
IF OBJECT_ID(N'[dbo].[SupplyUnitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplyUnitSet];
GO
IF OBJECT_ID(N'[dbo].[UserAccountSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAccountSet];
GO
IF OBJECT_ID(N'[dbo].[UserStatusSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserStatusSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StreetName] nvarchar(100)  NOT NULL,
    [StreetNumber] int  NOT NULL,
    [City] nvarchar(70)  NOT NULL,
    [Colony] nvarchar(70)  NOT NULL,
    [State] nvarchar(70)  NOT NULL,
    [ZipCode] int  NOT NULL,
    [Township] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CustomerOrderCustomerSet'
CREATE TABLE [dbo].[CustomerOrderCustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustomerOrderId] int  NOT NULL,
    [CustomerId] int  NOT NULL
);
GO

-- Creating table 'CustomerOrderDeliveryDriverSet'
CREATE TABLE [dbo].[CustomerOrderDeliveryDriverSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CustomerOrderId] int  NOT NULL,
    [DeliveryDriverId] int  NOT NULL
);
GO

-- Creating table 'CustomerOrderDetailSet'
CREATE TABLE [dbo].[CustomerOrderDetailSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductQuantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [CustomerOrderId] int  NOT NULL,
    [ProductSaleId] int  NOT NULL
);
GO

-- Creating table 'CustomerOrderSet'
CREATE TABLE [dbo].[CustomerOrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [RegistrationTime] time  NOT NULL,
    [OrderStatusId] int  NOT NULL,
    [OrderTypeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Names] nvarchar(70)  NOT NULL,
    [LastName] nvarchar(70)  NOT NULL,
    [SecondLastName] nvarchar(70)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(12)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [UserStatusId] int  NOT NULL,
    [Address_Id] int  NOT NULL
);
GO

-- Creating table 'DailyClosingSet'
CREATE TABLE [dbo].[DailyClosingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClosingDate] datetime  NOT NULL,
    [Description] nvarchar(300)  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'DeliveryDriverSet'
CREATE TABLE [dbo].[DeliveryDriverSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Names] nvarchar(70)  NOT NULL,
    [LastName] nvarchar(70)  NOT NULL,
    [SecondLastName] nvarchar(70)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(12)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [UserStatusId] int  NOT NULL
);
GO

-- Creating table 'EmployeePositionSet'
CREATE TABLE [dbo].[EmployeePositionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Names] nvarchar(70)  NOT NULL,
    [LastName] nvarchar(70)  NOT NULL,
    [SecondLastName] nvarchar(70)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(12)  NOT NULL,
    [ProfilePhoto] varbinary(max)  NULL,
    [UserStatusId] int  NOT NULL,
    [EmployeePositionId] int  NOT NULL,
    [Address_Id] int  NOT NULL,
    [UserAccount_Id] int  NOT NULL
);
GO

-- Creating table 'FinancialTransactionSet'
CREATE TABLE [dbo].[FinancialTransactionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(30)  NOT NULL,
    [Description] nvarchar(225)  NOT NULL,
    [FinancialTransactionDate] datetime  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'InventoryValidationSet'
CREATE TABLE [dbo].[InventoryValidationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InventoryValidationDate] datetime  NOT NULL,
    [Description] nvarchar(225)  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'OrderStatusSet'
CREATE TABLE [dbo].[OrderStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'OrderTypeSet'
CREATE TABLE [dbo].[OrderTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductSaleSet'
CREATE TABLE [dbo].[ProductSaleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [Picture] varbinary(max)  NULL,
    [ProductStatusId] int  NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [IdentificationCode] nvarchar(20)  NOT NULL,
    [Description] nvarchar(300)  NOT NULL
);
GO

-- Creating table 'ProductStatusSet'
CREATE TABLE [dbo].[ProductStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductTypeSet'
CREATE TABLE [dbo].[ProductTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'RecipeDetailsSet'
CREATE TABLE [dbo].[RecipeDetailsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [SupplyId] int  NOT NULL,
    [RecipeId] int  NOT NULL
);
GO

-- Creating table 'RecipeSet'
CREATE TABLE [dbo].[RecipeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Instructions] nvarchar(200)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [ProductSale_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierOrderDetailsSet'
CREATE TABLE [dbo].[SupplierOrderDetailsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SupplyQuantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [SupplierOrderId] int  NOT NULL,
    [SupplyId] int  NOT NULL
);
GO

-- Creating table 'SupplierOrderSet'
CREATE TABLE [dbo].[SupplierOrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [TotalAmount] float  NOT NULL,
    [RegistrationTime] time  NOT NULL,
    [OrderStatusId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [Supplier_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierSet'
CREATE TABLE [dbo].[SupplierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Names] nvarchar(70)  NOT NULL,
    [LastName] nvarchar(70)  NOT NULL,
    [SecondLastName] nvarchar(70)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [Phone] nvarchar(12)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [UserStatusId] int  NOT NULL
);
GO

-- Creating table 'SupplySet'
CREATE TABLE [dbo].[SupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [Picture] varbinary(max)  NULL,
    [SupplyUnitId] int  NOT NULL,
    [ProductStatusId] int  NOT NULL,
    [SupplyTypeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [IdentificationCode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'SupplyTypeSet'
CREATE TABLE [dbo].[SupplyTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'SupplyUnitSet'
CREATE TABLE [dbo].[SupplyUnitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Unit] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'UserAccountSet'
CREATE TABLE [dbo].[UserAccountSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(70)  NOT NULL,
    [Password] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'UserStatusSet'
CREATE TABLE [dbo].[UserStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DailyClosingFinancialTransaction'
CREATE TABLE [dbo].[DailyClosingFinancialTransaction] (
    [DailyClosingSet_Id] int  NOT NULL,
    [FinancialTransactionSet_Id] int  NOT NULL
);
GO

-- Creating table 'FinancialTransactionCustomerOrder'
CREATE TABLE [dbo].[FinancialTransactionCustomerOrder] (
    [CustomerOrderSet_Id] int  NOT NULL,
    [FinancialTransactionSet_Id] int  NOT NULL
);
GO

-- Creating table 'FinancialTransactionSupplierOrder'
CREATE TABLE [dbo].[FinancialTransactionSupplierOrder] (
    [FinancialTransactionSet_Id] int  NOT NULL,
    [SupplierOrderSet_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierSupply'
CREATE TABLE [dbo].[SupplierSupply] (
    [SupplierSet_Id] int  NOT NULL,
    [SupplySet_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderCustomerSet'
ALTER TABLE [dbo].[CustomerOrderCustomerSet]
ADD CONSTRAINT [PK_CustomerOrderCustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderDeliveryDriverSet'
ALTER TABLE [dbo].[CustomerOrderDeliveryDriverSet]
ADD CONSTRAINT [PK_CustomerOrderDeliveryDriverSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderDetailSet'
ALTER TABLE [dbo].[CustomerOrderDetailSet]
ADD CONSTRAINT [PK_CustomerOrderDetailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderSet'
ALTER TABLE [dbo].[CustomerOrderSet]
ADD CONSTRAINT [PK_CustomerOrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DailyClosingSet'
ALTER TABLE [dbo].[DailyClosingSet]
ADD CONSTRAINT [PK_DailyClosingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeliveryDriverSet'
ALTER TABLE [dbo].[DeliveryDriverSet]
ADD CONSTRAINT [PK_DeliveryDriverSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeePositionSet'
ALTER TABLE [dbo].[EmployeePositionSet]
ADD CONSTRAINT [PK_EmployeePositionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FinancialTransactionSet'
ALTER TABLE [dbo].[FinancialTransactionSet]
ADD CONSTRAINT [PK_FinancialTransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryValidationSet'
ALTER TABLE [dbo].[InventoryValidationSet]
ADD CONSTRAINT [PK_InventoryValidationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderStatusSet'
ALTER TABLE [dbo].[OrderStatusSet]
ADD CONSTRAINT [PK_OrderStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderTypeSet'
ALTER TABLE [dbo].[OrderTypeSet]
ADD CONSTRAINT [PK_OrderTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSaleSet'
ALTER TABLE [dbo].[ProductSaleSet]
ADD CONSTRAINT [PK_ProductSaleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductStatusSet'
ALTER TABLE [dbo].[ProductStatusSet]
ADD CONSTRAINT [PK_ProductStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypeSet'
ALTER TABLE [dbo].[ProductTypeSet]
ADD CONSTRAINT [PK_ProductTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeDetailsSet'
ALTER TABLE [dbo].[RecipeDetailsSet]
ADD CONSTRAINT [PK_RecipeDetailsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [PK_RecipeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderDetailsSet'
ALTER TABLE [dbo].[SupplierOrderDetailsSet]
ADD CONSTRAINT [PK_SupplierOrderDetailsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [PK_SupplierOrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [PK_SupplierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [PK_SupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplyTypeSet'
ALTER TABLE [dbo].[SupplyTypeSet]
ADD CONSTRAINT [PK_SupplyTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplyUnitSet'
ALTER TABLE [dbo].[SupplyUnitSet]
ADD CONSTRAINT [PK_SupplyUnitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAccountSet'
ALTER TABLE [dbo].[UserAccountSet]
ADD CONSTRAINT [PK_UserAccountSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserStatusSet'
ALTER TABLE [dbo].[UserStatusSet]
ADD CONSTRAINT [PK_UserStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [DailyClosingSet_Id], [FinancialTransactionSet_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [PK_DailyClosingFinancialTransaction]
    PRIMARY KEY CLUSTERED ([DailyClosingSet_Id], [FinancialTransactionSet_Id] ASC);
GO

-- Creating primary key on [CustomerOrderSet_Id], [FinancialTransactionSet_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [PK_FinancialTransactionCustomerOrder]
    PRIMARY KEY CLUSTERED ([CustomerOrderSet_Id], [FinancialTransactionSet_Id] ASC);
GO

-- Creating primary key on [FinancialTransactionSet_Id], [SupplierOrderSet_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [PK_FinancialTransactionSupplierOrder]
    PRIMARY KEY CLUSTERED ([FinancialTransactionSet_Id], [SupplierOrderSet_Id] ASC);
GO

-- Creating primary key on [SupplierSet_Id], [SupplySet_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [PK_SupplierSupply]
    PRIMARY KEY CLUSTERED ([SupplierSet_Id], [SupplySet_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Address_Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_CustomerAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[AddressSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerAddress'
CREATE INDEX [IX_FK_CustomerAddress]
ON [dbo].[CustomerSet]
    ([Address_Id]);
GO

-- Creating foreign key on [Address_Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_EmployeeAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[AddressSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeAddress'
CREATE INDEX [IX_FK_EmployeeAddress]
ON [dbo].[EmployeeSet]
    ([Address_Id]);
GO

-- Creating foreign key on [CustomerId] in table 'CustomerOrderCustomerSet'
ALTER TABLE [dbo].[CustomerOrderCustomerSet]
ADD CONSTRAINT [FK_CustomerCustomerOrderCustomer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerCustomerOrderCustomer'
CREATE INDEX [IX_FK_CustomerCustomerOrderCustomer]
ON [dbo].[CustomerOrderCustomerSet]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerOrderId] in table 'CustomerOrderCustomerSet'
ALTER TABLE [dbo].[CustomerOrderCustomerSet]
ADD CONSTRAINT [FK_CustomerOrderCustomerOrderCustomer]
    FOREIGN KEY ([CustomerOrderId])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrderCustomerOrderCustomer'
CREATE INDEX [IX_FK_CustomerOrderCustomerOrderCustomer]
ON [dbo].[CustomerOrderCustomerSet]
    ([CustomerOrderId]);
GO

-- Creating foreign key on [CustomerOrderId] in table 'CustomerOrderDeliveryDriverSet'
ALTER TABLE [dbo].[CustomerOrderDeliveryDriverSet]
ADD CONSTRAINT [FK_CustomerOrderCustomerOrderDeliveryDriver]
    FOREIGN KEY ([CustomerOrderId])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrderCustomerOrderDeliveryDriver'
CREATE INDEX [IX_FK_CustomerOrderCustomerOrderDeliveryDriver]
ON [dbo].[CustomerOrderDeliveryDriverSet]
    ([CustomerOrderId]);
GO

-- Creating foreign key on [DeliveryDriverId] in table 'CustomerOrderDeliveryDriverSet'
ALTER TABLE [dbo].[CustomerOrderDeliveryDriverSet]
ADD CONSTRAINT [FK_DeliveryDriverCustomerOrderDeliveryDriver]
    FOREIGN KEY ([DeliveryDriverId])
    REFERENCES [dbo].[DeliveryDriverSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryDriverCustomerOrderDeliveryDriver'
CREATE INDEX [IX_FK_DeliveryDriverCustomerOrderDeliveryDriver]
ON [dbo].[CustomerOrderDeliveryDriverSet]
    ([DeliveryDriverId]);
GO

-- Creating foreign key on [CustomerOrderId] in table 'CustomerOrderDetailSet'
ALTER TABLE [dbo].[CustomerOrderDetailSet]
ADD CONSTRAINT [FK_CustomerOrderCustomerOrderDetail]
    FOREIGN KEY ([CustomerOrderId])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrderCustomerOrderDetail'
CREATE INDEX [IX_FK_CustomerOrderCustomerOrderDetail]
ON [dbo].[CustomerOrderDetailSet]
    ([CustomerOrderId]);
GO

-- Creating foreign key on [ProductSaleId] in table 'CustomerOrderDetailSet'
ALTER TABLE [dbo].[CustomerOrderDetailSet]
ADD CONSTRAINT [FK_ProductSaleCustomerOrderDetail]
    FOREIGN KEY ([ProductSaleId])
    REFERENCES [dbo].[ProductSaleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSaleCustomerOrderDetail'
CREATE INDEX [IX_FK_ProductSaleCustomerOrderDetail]
ON [dbo].[CustomerOrderDetailSet]
    ([ProductSaleId]);
GO

-- Creating foreign key on [EmployeeId] in table 'CustomerOrderSet'
ALTER TABLE [dbo].[CustomerOrderSet]
ADD CONSTRAINT [FK_EmployeeCustomerOrder]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCustomerOrder'
CREATE INDEX [IX_FK_EmployeeCustomerOrder]
ON [dbo].[CustomerOrderSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [OrderStatusId] in table 'CustomerOrderSet'
ALTER TABLE [dbo].[CustomerOrderSet]
ADD CONSTRAINT [FK_OrderStatusCustomerOrder]
    FOREIGN KEY ([OrderStatusId])
    REFERENCES [dbo].[OrderStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderStatusCustomerOrder'
CREATE INDEX [IX_FK_OrderStatusCustomerOrder]
ON [dbo].[CustomerOrderSet]
    ([OrderStatusId]);
GO

-- Creating foreign key on [OrderTypeId] in table 'CustomerOrderSet'
ALTER TABLE [dbo].[CustomerOrderSet]
ADD CONSTRAINT [FK_OrderTypeCustomerOrder]
    FOREIGN KEY ([OrderTypeId])
    REFERENCES [dbo].[OrderTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderTypeCustomerOrder'
CREATE INDEX [IX_FK_OrderTypeCustomerOrder]
ON [dbo].[CustomerOrderSet]
    ([OrderTypeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_EmployeeCustomer]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCustomer'
CREATE INDEX [IX_FK_EmployeeCustomer]
ON [dbo].[CustomerSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [UserStatusId] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_UserStatusCustomer]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusCustomer'
CREATE INDEX [IX_FK_UserStatusCustomer]
ON [dbo].[CustomerSet]
    ([UserStatusId]);
GO

-- Creating foreign key on [EmployeeId] in table 'DailyClosingSet'
ALTER TABLE [dbo].[DailyClosingSet]
ADD CONSTRAINT [FK_EmployeeDailyClosing]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDailyClosing'
CREATE INDEX [IX_FK_EmployeeDailyClosing]
ON [dbo].[DailyClosingSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'DeliveryDriverSet'
ALTER TABLE [dbo].[DeliveryDriverSet]
ADD CONSTRAINT [FK_EmployeeDeliveryDriver]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeDeliveryDriver'
CREATE INDEX [IX_FK_EmployeeDeliveryDriver]
ON [dbo].[DeliveryDriverSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [UserStatusId] in table 'DeliveryDriverSet'
ALTER TABLE [dbo].[DeliveryDriverSet]
ADD CONSTRAINT [FK_UserStatusDeliveryDriver]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusDeliveryDriver'
CREATE INDEX [IX_FK_UserStatusDeliveryDriver]
ON [dbo].[DeliveryDriverSet]
    ([UserStatusId]);
GO

-- Creating foreign key on [EmployeePositionId] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_EmployeePositionEmployee]
    FOREIGN KEY ([EmployeePositionId])
    REFERENCES [dbo].[EmployeePositionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePositionEmployee'
CREATE INDEX [IX_FK_EmployeePositionEmployee]
ON [dbo].[EmployeeSet]
    ([EmployeePositionId]);
GO

-- Creating foreign key on [EmployeeId] in table 'FinancialTransactionSet'
ALTER TABLE [dbo].[FinancialTransactionSet]
ADD CONSTRAINT [FK_EmployeeFinancialTransaction]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeFinancialTransaction'
CREATE INDEX [IX_FK_EmployeeFinancialTransaction]
ON [dbo].[FinancialTransactionSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'InventoryValidationSet'
ALTER TABLE [dbo].[InventoryValidationSet]
ADD CONSTRAINT [FK_EmployeeInventoryValidation]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeInventoryValidation'
CREATE INDEX [IX_FK_EmployeeInventoryValidation]
ON [dbo].[InventoryValidationSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'ProductSaleSet'
ALTER TABLE [dbo].[ProductSaleSet]
ADD CONSTRAINT [FK_EmployeeProductSale]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeProductSale'
CREATE INDEX [IX_FK_EmployeeProductSale]
ON [dbo].[ProductSaleSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_EmployeeRecipe]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeRecipe'
CREATE INDEX [IX_FK_EmployeeRecipe]
ON [dbo].[RecipeSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [FK_EmployeeSupplier]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSupplier'
CREATE INDEX [IX_FK_EmployeeSupplier]
ON [dbo].[SupplierSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [FK_EmployeeSupplierOrder]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSupplierOrder'
CREATE INDEX [IX_FK_EmployeeSupplierOrder]
ON [dbo].[SupplierOrderSet]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [FK_EmployeeSupply]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSupply'
CREATE INDEX [IX_FK_EmployeeSupply]
ON [dbo].[SupplySet]
    ([EmployeeId]);
GO

-- Creating foreign key on [UserAccount_Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_EmployeeUserAccount]
    FOREIGN KEY ([UserAccount_Id])
    REFERENCES [dbo].[UserAccountSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeUserAccount'
CREATE INDEX [IX_FK_EmployeeUserAccount]
ON [dbo].[EmployeeSet]
    ([UserAccount_Id]);
GO

-- Creating foreign key on [UserStatusId] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_UserStatusEmployee]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusEmployee'
CREATE INDEX [IX_FK_UserStatusEmployee]
ON [dbo].[EmployeeSet]
    ([UserStatusId]);
GO

-- Creating foreign key on [OrderStatusId] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [FK_OrderStatusSupplierOrder]
    FOREIGN KEY ([OrderStatusId])
    REFERENCES [dbo].[OrderStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderStatusSupplierOrder'
CREATE INDEX [IX_FK_OrderStatusSupplierOrder]
ON [dbo].[SupplierOrderSet]
    ([OrderStatusId]);
GO

-- Creating foreign key on [ProductSale_Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_ProductSaleRecipe]
    FOREIGN KEY ([ProductSale_Id])
    REFERENCES [dbo].[ProductSaleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductSaleRecipe'
CREATE INDEX [IX_FK_ProductSaleRecipe]
ON [dbo].[RecipeSet]
    ([ProductSale_Id]);
GO

-- Creating foreign key on [ProductStatusId] in table 'ProductSaleSet'
ALTER TABLE [dbo].[ProductSaleSet]
ADD CONSTRAINT [FK_ProductStatusProductSale]
    FOREIGN KEY ([ProductStatusId])
    REFERENCES [dbo].[ProductStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStatusProductSale'
CREATE INDEX [IX_FK_ProductStatusProductSale]
ON [dbo].[ProductSaleSet]
    ([ProductStatusId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'ProductSaleSet'
ALTER TABLE [dbo].[ProductSaleSet]
ADD CONSTRAINT [FK_ProductTypeProductSale]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTypeProductSale'
CREATE INDEX [IX_FK_ProductTypeProductSale]
ON [dbo].[ProductSaleSet]
    ([ProductTypeId]);
GO

-- Creating foreign key on [ProductStatusId] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [FK_ProductStatusSupply]
    FOREIGN KEY ([ProductStatusId])
    REFERENCES [dbo].[ProductStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductStatusSupply'
CREATE INDEX [IX_FK_ProductStatusSupply]
ON [dbo].[SupplySet]
    ([ProductStatusId]);
GO

-- Creating foreign key on [RecipeId] in table 'RecipeDetailsSet'
ALTER TABLE [dbo].[RecipeDetailsSet]
ADD CONSTRAINT [FK_RecipeRecipeDetails]
    FOREIGN KEY ([RecipeId])
    REFERENCES [dbo].[RecipeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipeRecipeDetails'
CREATE INDEX [IX_FK_RecipeRecipeDetails]
ON [dbo].[RecipeDetailsSet]
    ([RecipeId]);
GO

-- Creating foreign key on [SupplyId] in table 'RecipeDetailsSet'
ALTER TABLE [dbo].[RecipeDetailsSet]
ADD CONSTRAINT [FK_SupplyRecipeDetails]
    FOREIGN KEY ([SupplyId])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplyRecipeDetails'
CREATE INDEX [IX_FK_SupplyRecipeDetails]
ON [dbo].[RecipeDetailsSet]
    ([SupplyId]);
GO

-- Creating foreign key on [SupplierOrderId] in table 'SupplierOrderDetailsSet'
ALTER TABLE [dbo].[SupplierOrderDetailsSet]
ADD CONSTRAINT [FK_SupplierOrderSupplierOrderDetails]
    FOREIGN KEY ([SupplierOrderId])
    REFERENCES [dbo].[SupplierOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierOrderSupplierOrderDetails'
CREATE INDEX [IX_FK_SupplierOrderSupplierOrderDetails]
ON [dbo].[SupplierOrderDetailsSet]
    ([SupplierOrderId]);
GO

-- Creating foreign key on [SupplyId] in table 'SupplierOrderDetailsSet'
ALTER TABLE [dbo].[SupplierOrderDetailsSet]
ADD CONSTRAINT [FK_SupplySupplierOrderDetails]
    FOREIGN KEY ([SupplyId])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplySupplierOrderDetails'
CREATE INDEX [IX_FK_SupplySupplierOrderDetails]
ON [dbo].[SupplierOrderDetailsSet]
    ([SupplyId]);
GO

-- Creating foreign key on [Supplier_Id] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [FK_SupplierOrderSupplier]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierOrderSupplier'
CREATE INDEX [IX_FK_SupplierOrderSupplier]
ON [dbo].[SupplierOrderSet]
    ([Supplier_Id]);
GO

-- Creating foreign key on [UserStatusId] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [FK_UserStatusSupplier]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatusSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusSupplier'
CREATE INDEX [IX_FK_UserStatusSupplier]
ON [dbo].[SupplierSet]
    ([UserStatusId]);
GO

-- Creating foreign key on [SupplyTypeId] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [FK_SupplyTypeSupply]
    FOREIGN KEY ([SupplyTypeId])
    REFERENCES [dbo].[SupplyTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplyTypeSupply'
CREATE INDEX [IX_FK_SupplyTypeSupply]
ON [dbo].[SupplySet]
    ([SupplyTypeId]);
GO

-- Creating foreign key on [SupplyUnitId] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [FK_SupplyUnitSupply]
    FOREIGN KEY ([SupplyUnitId])
    REFERENCES [dbo].[SupplyUnitSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplyUnitSupply'
CREATE INDEX [IX_FK_SupplyUnitSupply]
ON [dbo].[SupplySet]
    ([SupplyUnitId]);
GO

-- Creating foreign key on [DailyClosingSet_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [FK_DailyClosingFinancialTransaction_DailyClosingSet]
    FOREIGN KEY ([DailyClosingSet_Id])
    REFERENCES [dbo].[DailyClosingSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FinancialTransactionSet_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [FK_DailyClosingFinancialTransaction_FinancialTransactionSet]
    FOREIGN KEY ([FinancialTransactionSet_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DailyClosingFinancialTransaction_FinancialTransactionSet'
CREATE INDEX [IX_FK_DailyClosingFinancialTransaction_FinancialTransactionSet]
ON [dbo].[DailyClosingFinancialTransaction]
    ([FinancialTransactionSet_Id]);
GO

-- Creating foreign key on [CustomerOrderSet_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [FK_FinancialTransactionCustomerOrder_CustomerOrderSet]
    FOREIGN KEY ([CustomerOrderSet_Id])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FinancialTransactionSet_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [FK_FinancialTransactionCustomerOrder_FinancialTransactionSet]
    FOREIGN KEY ([FinancialTransactionSet_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinancialTransactionCustomerOrder_FinancialTransactionSet'
CREATE INDEX [IX_FK_FinancialTransactionCustomerOrder_FinancialTransactionSet]
ON [dbo].[FinancialTransactionCustomerOrder]
    ([FinancialTransactionSet_Id]);
GO

-- Creating foreign key on [FinancialTransactionSet_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [FK_FinancialTransactionSupplierOrder_FinancialTransactionSet]
    FOREIGN KEY ([FinancialTransactionSet_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SupplierOrderSet_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [FK_FinancialTransactionSupplierOrder_SupplierOrderSet]
    FOREIGN KEY ([SupplierOrderSet_Id])
    REFERENCES [dbo].[SupplierOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinancialTransactionSupplierOrder_SupplierOrderSet'
CREATE INDEX [IX_FK_FinancialTransactionSupplierOrder_SupplierOrderSet]
ON [dbo].[FinancialTransactionSupplierOrder]
    ([SupplierOrderSet_Id]);
GO

-- Creating foreign key on [SupplierSet_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [FK_SupplierSupply_SupplierSet]
    FOREIGN KEY ([SupplierSet_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SupplySet_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [FK_SupplierSupply_SupplySet]
    FOREIGN KEY ([SupplySet_Id])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupply_SupplySet'
CREATE INDEX [IX_FK_SupplierSupply_SupplySet]
ON [dbo].[SupplierSupply]
    ([SupplySet_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------