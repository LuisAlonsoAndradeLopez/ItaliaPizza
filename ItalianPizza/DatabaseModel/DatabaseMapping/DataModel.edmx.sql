
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/24/2024 01:54:37
-- Generated from EDMX file: D:\Proyectos C#\ItalianPizza\ItaliaPizza\ItalianPizza\DatabaseModel\DatabaseMapping\DataModel.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductSaleSet'
CREATE TABLE [dbo].[ProductSaleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [Picture] varbinary(max)  NOT NULL,
    [ProductStatusId] int  NOT NULL,
    [ProductTypeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL
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

-- Creating table 'ProductTypeSet'
CREATE TABLE [dbo].[ProductTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'OrderTypeSet'
CREATE TABLE [dbo].[OrderTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductStatusSet'
CREATE TABLE [dbo].[ProductStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
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

-- Creating table 'OrderStatusSet'
CREATE TABLE [dbo].[OrderStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
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

-- Creating table 'InventoryValidationSet'
CREATE TABLE [dbo].[InventoryValidationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InventoryValidationDate] datetime  NOT NULL,
    [Description] nvarchar(225)  NOT NULL,
    [EmployeeId] int  NOT NULL
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

-- Creating table 'SupplySet'
CREATE TABLE [dbo].[SupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Quantity] int  NOT NULL,
    [PricePerUnit] float  NOT NULL,
    [Picture] varbinary(max)  NOT NULL,
    [SupplyUnitId] int  NOT NULL,
    [ProductStatusId] int  NOT NULL,
    [SupplyTypeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'SupplyUnitSet'
CREATE TABLE [dbo].[SupplyUnitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Unit] nvarchar(100)  NOT NULL
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

-- Creating table 'RecipeDetailsSet'
CREATE TABLE [dbo].[RecipeDetailsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [SupplyId] int  NOT NULL,
    [RecipeId] int  NOT NULL
);
GO

-- Creating table 'UserAccountSet'
CREATE TABLE [dbo].[UserAccountSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(70)  NOT NULL,
    [Password] nvarchar(30)  NOT NULL
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
    [ProfilePhoto] varbinary(max)  NOT NULL,
    [UserStatusId] int  NOT NULL,
    [EmployeePositionId] int  NOT NULL,
    [Address_Id] int  NOT NULL,
    [UserAccount_Id] int  NOT NULL
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
    [Address_Id] int  NOT NULL,
    [CustomerOrder_Id] int  NOT NULL
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
    [UserStatusId] int  NOT NULL,
    [CustomerOrder_Id] int  NOT NULL
);
GO

-- Creating table 'EmployeePositionSet'
CREATE TABLE [dbo].[EmployeePositionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserStatusSet'
CREATE TABLE [dbo].[UserStatusSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(50)  NOT NULL
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

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StreetName] nvarchar(100)  NOT NULL,
    [StreetNumber] int  NOT NULL,
    [City] nvarchar(70)  NOT NULL,
    [Colony] nvarchar(70)  NOT NULL,
    [State] nvarchar(70)  NOT NULL,
    [ZipCode] int  NOT NULL
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

-- Creating table 'SupplyTypeSet'
CREATE TABLE [dbo].[SupplyTypeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'SupplierSupply'
CREATE TABLE [dbo].[SupplierSupply] (
    [Supplier_Id] int  NOT NULL,
    [Supply_Id] int  NOT NULL
);
GO

-- Creating table 'FinancialTransactionCustomerOrder'
CREATE TABLE [dbo].[FinancialTransactionCustomerOrder] (
    [FinancialTransaction_Id] int  NOT NULL,
    [CustomerOrder_Id] int  NOT NULL
);
GO

-- Creating table 'FinancialTransactionSupplierOrder'
CREATE TABLE [dbo].[FinancialTransactionSupplierOrder] (
    [FinancialTransaction_Id] int  NOT NULL,
    [SupplierOrder_Id] int  NOT NULL
);
GO

-- Creating table 'DailyClosingFinancialTransaction'
CREATE TABLE [dbo].[DailyClosingFinancialTransaction] (
    [DailyClosing_Id] int  NOT NULL,
    [FinancialTransaction_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductSaleSet'
ALTER TABLE [dbo].[ProductSaleSet]
ADD CONSTRAINT [PK_ProductSaleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderDetailSet'
ALTER TABLE [dbo].[CustomerOrderDetailSet]
ADD CONSTRAINT [PK_CustomerOrderDetailSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypeSet'
ALTER TABLE [dbo].[ProductTypeSet]
ADD CONSTRAINT [PK_ProductTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderTypeSet'
ALTER TABLE [dbo].[OrderTypeSet]
ADD CONSTRAINT [PK_OrderTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductStatusSet'
ALTER TABLE [dbo].[ProductStatusSet]
ADD CONSTRAINT [PK_ProductStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerOrderSet'
ALTER TABLE [dbo].[CustomerOrderSet]
ADD CONSTRAINT [PK_CustomerOrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderStatusSet'
ALTER TABLE [dbo].[OrderStatusSet]
ADD CONSTRAINT [PK_OrderStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DailyClosingSet'
ALTER TABLE [dbo].[DailyClosingSet]
ADD CONSTRAINT [PK_DailyClosingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InventoryValidationSet'
ALTER TABLE [dbo].[InventoryValidationSet]
ADD CONSTRAINT [PK_InventoryValidationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderDetailsSet'
ALTER TABLE [dbo].[SupplierOrderDetailsSet]
ADD CONSTRAINT [PK_SupplierOrderDetailsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [PK_SupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplyUnitSet'
ALTER TABLE [dbo].[SupplyUnitSet]
ADD CONSTRAINT [PK_SupplyUnitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FinancialTransactionSet'
ALTER TABLE [dbo].[FinancialTransactionSet]
ADD CONSTRAINT [PK_FinancialTransactionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [PK_RecipeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeDetailsSet'
ALTER TABLE [dbo].[RecipeDetailsSet]
ADD CONSTRAINT [PK_RecipeDetailsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserAccountSet'
ALTER TABLE [dbo].[UserAccountSet]
ADD CONSTRAINT [PK_UserAccountSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
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

-- Creating primary key on [Id] in table 'UserStatusSet'
ALTER TABLE [dbo].[UserStatusSet]
ADD CONSTRAINT [PK_UserStatusSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [PK_SupplierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [PK_SupplierOrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplyTypeSet'
ALTER TABLE [dbo].[SupplyTypeSet]
ADD CONSTRAINT [PK_SupplyTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Supplier_Id], [Supply_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [PK_SupplierSupply]
    PRIMARY KEY CLUSTERED ([Supplier_Id], [Supply_Id] ASC);
GO

-- Creating primary key on [FinancialTransaction_Id], [CustomerOrder_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [PK_FinancialTransactionCustomerOrder]
    PRIMARY KEY CLUSTERED ([FinancialTransaction_Id], [CustomerOrder_Id] ASC);
GO

-- Creating primary key on [FinancialTransaction_Id], [SupplierOrder_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [PK_FinancialTransactionSupplierOrder]
    PRIMARY KEY CLUSTERED ([FinancialTransaction_Id], [SupplierOrder_Id] ASC);
GO

-- Creating primary key on [DailyClosing_Id], [FinancialTransaction_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [PK_DailyClosingFinancialTransaction]
    PRIMARY KEY CLUSTERED ([DailyClosing_Id], [FinancialTransaction_Id] ASC);
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

-- Creating foreign key on [CustomerOrder_Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_CustomerOrderCustomer]
    FOREIGN KEY ([CustomerOrder_Id])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrderCustomer'
CREATE INDEX [IX_FK_CustomerOrderCustomer]
ON [dbo].[CustomerSet]
    ([CustomerOrder_Id]);
GO

-- Creating foreign key on [CustomerOrder_Id] in table 'DeliveryDriverSet'
ALTER TABLE [dbo].[DeliveryDriverSet]
ADD CONSTRAINT [FK_CustomerOrderDeliveryDriver]
    FOREIGN KEY ([CustomerOrder_Id])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrderDeliveryDriver'
CREATE INDEX [IX_FK_CustomerOrderDeliveryDriver]
ON [dbo].[DeliveryDriverSet]
    ([CustomerOrder_Id]);
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

-- Creating foreign key on [Supplier_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [FK_SupplierSupply_Supplier]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Supply_Id] in table 'SupplierSupply'
ALTER TABLE [dbo].[SupplierSupply]
ADD CONSTRAINT [FK_SupplierSupply_Supply]
    FOREIGN KEY ([Supply_Id])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupply_Supply'
CREATE INDEX [IX_FK_SupplierSupply_Supply]
ON [dbo].[SupplierSupply]
    ([Supply_Id]);
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

-- Creating foreign key on [FinancialTransaction_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [FK_FinancialTransactionCustomerOrder_FinancialTransaction]
    FOREIGN KEY ([FinancialTransaction_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CustomerOrder_Id] in table 'FinancialTransactionCustomerOrder'
ALTER TABLE [dbo].[FinancialTransactionCustomerOrder]
ADD CONSTRAINT [FK_FinancialTransactionCustomerOrder_CustomerOrder]
    FOREIGN KEY ([CustomerOrder_Id])
    REFERENCES [dbo].[CustomerOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinancialTransactionCustomerOrder_CustomerOrder'
CREATE INDEX [IX_FK_FinancialTransactionCustomerOrder_CustomerOrder]
ON [dbo].[FinancialTransactionCustomerOrder]
    ([CustomerOrder_Id]);
GO

-- Creating foreign key on [FinancialTransaction_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [FK_FinancialTransactionSupplierOrder_FinancialTransaction]
    FOREIGN KEY ([FinancialTransaction_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SupplierOrder_Id] in table 'FinancialTransactionSupplierOrder'
ALTER TABLE [dbo].[FinancialTransactionSupplierOrder]
ADD CONSTRAINT [FK_FinancialTransactionSupplierOrder_SupplierOrder]
    FOREIGN KEY ([SupplierOrder_Id])
    REFERENCES [dbo].[SupplierOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FinancialTransactionSupplierOrder_SupplierOrder'
CREATE INDEX [IX_FK_FinancialTransactionSupplierOrder_SupplierOrder]
ON [dbo].[FinancialTransactionSupplierOrder]
    ([SupplierOrder_Id]);
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

-- Creating foreign key on [DailyClosing_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [FK_DailyClosingFinancialTransaction_DailyClosing]
    FOREIGN KEY ([DailyClosing_Id])
    REFERENCES [dbo].[DailyClosingSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FinancialTransaction_Id] in table 'DailyClosingFinancialTransaction'
ALTER TABLE [dbo].[DailyClosingFinancialTransaction]
ADD CONSTRAINT [FK_DailyClosingFinancialTransaction_FinancialTransaction]
    FOREIGN KEY ([FinancialTransaction_Id])
    REFERENCES [dbo].[FinancialTransactionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DailyClosingFinancialTransaction_FinancialTransaction'
CREATE INDEX [IX_FK_DailyClosingFinancialTransaction_FinancialTransaction]
ON [dbo].[DailyClosingFinancialTransaction]
    ([FinancialTransaction_Id]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------