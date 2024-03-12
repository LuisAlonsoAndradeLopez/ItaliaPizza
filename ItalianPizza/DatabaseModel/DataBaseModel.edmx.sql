
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/10/2024 17:47:18
-- Generated from EDMX file: D:\Proyectos C#\ItalianPizza\ItaliaPizza\ItalianPizza\DatabaseModel\DataBaseModel.edmx
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

-- Creating table 'SupplierSet'
CREATE TABLE [dbo].[SupplierSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] int  NOT NULL,
    [Company] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Picture] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Cost] smallint  NOT NULL,
    [Amount] smallint  NOT NULL,
    [EmployeeID] int  NOT NULL
);
GO

-- Creating table 'OrderCustomerSet'
CREATE TABLE [dbo].[OrderCustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [TotalCost] float  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [Customer_Id] int  NULL
);
GO

-- Creating table 'RecipeSet'
CREATE TABLE [dbo].[RecipeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [ProductID] smallint  NOT NULL,
    [EmployeID] int  NOT NULL,
    [Product_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierOrderSet'
CREATE TABLE [dbo].[SupplierOrderSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [TotalCost] float  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [SupplierID] int  NOT NULL
);
GO

-- Creating table 'SupplySet'
CREATE TABLE [dbo].[SupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Cost] int  NOT NULL,
    [Picture] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Amount] smallint  NOT NULL,
    [EmployeeID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderCustomerProductSet'
CREATE TABLE [dbo].[OrderCustomerProductSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderCustomerID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [IndividualProductCost] float  NOT NULL,
    [ProductCount] smallint  NOT NULL
);
GO

-- Creating table 'CustomerSet'
CREATE TABLE [dbo].[CustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [EmployeeID] int  NOT NULL,
    [AddressID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SupplierOrderSupplySet'
CREATE TABLE [dbo].[SupplierOrderSupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IndividualSupplyCost] float  NOT NULL,
    [SupplyCount] smallint  NOT NULL,
    [SupplyID] int  NOT NULL,
    [OrderSupplierID] int  NOT NULL
);
GO

-- Creating table 'RecipeSupplySet'
CREATE TABLE [dbo].[RecipeSupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AmountSupplies] smallint  NOT NULL,
    [RecipeID] smallint  NOT NULL,
    [SupplyID] smallint  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Rol] int  NOT NULL,
    [AddressID] nvarchar(max)  NOT NULL,
    [Address_Id] int  NOT NULL,
    [EmployeeRole_Id] int  NOT NULL
);
GO

-- Creating table 'EmployeeRoleSet'
CREATE TABLE [dbo].[EmployeeRoleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rol] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AddressSet'
CREATE TABLE [dbo].[AddressSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StreetName] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [ZipCode] int  NOT NULL,
    [StreetNumber] smallint  NOT NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierSupplySet'
CREATE TABLE [dbo].[SupplierSupplySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SupplyID] int  NOT NULL,
    [SupplierID] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DealerSet'
CREATE TABLE [dbo].[DealerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DealerOrderCustomerSet'
CREATE TABLE [dbo].[DealerOrderCustomerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrderCustomerID] int  NOT NULL,
    [DealerID] int  NOT NULL
);
GO

-- Creating table 'ProductOrderCustomerProduct'
CREATE TABLE [dbo].[ProductOrderCustomerProduct] (
    [Product_Id] int  NOT NULL,
    [OrderCustomerProduct_Id] int  NOT NULL
);
GO

-- Creating table 'OrderCustomerProductOrderCustomer'
CREATE TABLE [dbo].[OrderCustomerProductOrderCustomer] (
    [OrderCustomerProduct_Id] int  NOT NULL,
    [OrderCustomer_Id] int  NOT NULL
);
GO

-- Creating table 'SupplyRecipeSupply'
CREATE TABLE [dbo].[SupplyRecipeSupply] (
    [Supply_Id] int  NOT NULL,
    [RecipeSupply_Id] int  NOT NULL
);
GO

-- Creating table 'RecipeRecipeSupply'
CREATE TABLE [dbo].[RecipeRecipeSupply] (
    [Recipe_Id] int  NOT NULL,
    [RecipeSupply_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierSupplierProduct'
CREATE TABLE [dbo].[SupplierSupplierProduct] (
    [Supplier_Id] int  NOT NULL,
    [SupplierProduct_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierOrderSupplySupply'
CREATE TABLE [dbo].[SupplierOrderSupplySupply] (
    [SupplierOrderSupply_Id] int  NOT NULL,
    [Supply_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierSupplySupply'
CREATE TABLE [dbo].[SupplierSupplySupply] (
    [SupplierSupply_Id] int  NOT NULL,
    [Supply_Id] int  NOT NULL
);
GO

-- Creating table 'SupplierOrderSupplierOrderSupply'
CREATE TABLE [dbo].[SupplierOrderSupplierOrderSupply] (
    [SupplierOrder_Id] int  NOT NULL,
    [SupplierOrderSupply_Id] int  NOT NULL
);
GO

-- Creating table 'OrderCustomerDealerOrderCustomer'
CREATE TABLE [dbo].[OrderCustomerDealerOrderCustomer] (
    [OrderCustomer_Id] int  NOT NULL,
    [DealerOrderCustomer_Id] int  NOT NULL
);
GO

-- Creating table 'DealerOrderCustomerDealer'
CREATE TABLE [dbo].[DealerOrderCustomerDealer] (
    [DealerOrderCustomer_Id] int  NOT NULL,
    [Dealer_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SupplierSet'
ALTER TABLE [dbo].[SupplierSet]
ADD CONSTRAINT [PK_SupplierSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderCustomerSet'
ALTER TABLE [dbo].[OrderCustomerSet]
ADD CONSTRAINT [PK_OrderCustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [PK_RecipeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [PK_SupplierOrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplySet'
ALTER TABLE [dbo].[SupplySet]
ADD CONSTRAINT [PK_SupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderCustomerProductSet'
ALTER TABLE [dbo].[OrderCustomerProductSet]
ADD CONSTRAINT [PK_OrderCustomerProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [PK_CustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierOrderSupplySet'
ALTER TABLE [dbo].[SupplierOrderSupplySet]
ADD CONSTRAINT [PK_SupplierOrderSupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecipeSupplySet'
ALTER TABLE [dbo].[RecipeSupplySet]
ADD CONSTRAINT [PK_RecipeSupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeRoleSet'
ALTER TABLE [dbo].[EmployeeRoleSet]
ADD CONSTRAINT [PK_EmployeeRoleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [PK_AddressSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierSupplySet'
ALTER TABLE [dbo].[SupplierSupplySet]
ADD CONSTRAINT [PK_SupplierSupplySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DealerSet'
ALTER TABLE [dbo].[DealerSet]
ADD CONSTRAINT [PK_DealerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DealerOrderCustomerSet'
ALTER TABLE [dbo].[DealerOrderCustomerSet]
ADD CONSTRAINT [PK_DealerOrderCustomerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Product_Id], [OrderCustomerProduct_Id] in table 'ProductOrderCustomerProduct'
ALTER TABLE [dbo].[ProductOrderCustomerProduct]
ADD CONSTRAINT [PK_ProductOrderCustomerProduct]
    PRIMARY KEY CLUSTERED ([Product_Id], [OrderCustomerProduct_Id] ASC);
GO

-- Creating primary key on [OrderCustomerProduct_Id], [OrderCustomer_Id] in table 'OrderCustomerProductOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerProductOrderCustomer]
ADD CONSTRAINT [PK_OrderCustomerProductOrderCustomer]
    PRIMARY KEY CLUSTERED ([OrderCustomerProduct_Id], [OrderCustomer_Id] ASC);
GO

-- Creating primary key on [Supply_Id], [RecipeSupply_Id] in table 'SupplyRecipeSupply'
ALTER TABLE [dbo].[SupplyRecipeSupply]
ADD CONSTRAINT [PK_SupplyRecipeSupply]
    PRIMARY KEY CLUSTERED ([Supply_Id], [RecipeSupply_Id] ASC);
GO

-- Creating primary key on [Recipe_Id], [RecipeSupply_Id] in table 'RecipeRecipeSupply'
ALTER TABLE [dbo].[RecipeRecipeSupply]
ADD CONSTRAINT [PK_RecipeRecipeSupply]
    PRIMARY KEY CLUSTERED ([Recipe_Id], [RecipeSupply_Id] ASC);
GO

-- Creating primary key on [Supplier_Id], [SupplierProduct_Id] in table 'SupplierSupplierProduct'
ALTER TABLE [dbo].[SupplierSupplierProduct]
ADD CONSTRAINT [PK_SupplierSupplierProduct]
    PRIMARY KEY CLUSTERED ([Supplier_Id], [SupplierProduct_Id] ASC);
GO

-- Creating primary key on [SupplierOrderSupply_Id], [Supply_Id] in table 'SupplierOrderSupplySupply'
ALTER TABLE [dbo].[SupplierOrderSupplySupply]
ADD CONSTRAINT [PK_SupplierOrderSupplySupply]
    PRIMARY KEY CLUSTERED ([SupplierOrderSupply_Id], [Supply_Id] ASC);
GO

-- Creating primary key on [SupplierSupply_Id], [Supply_Id] in table 'SupplierSupplySupply'
ALTER TABLE [dbo].[SupplierSupplySupply]
ADD CONSTRAINT [PK_SupplierSupplySupply]
    PRIMARY KEY CLUSTERED ([SupplierSupply_Id], [Supply_Id] ASC);
GO

-- Creating primary key on [SupplierOrder_Id], [SupplierOrderSupply_Id] in table 'SupplierOrderSupplierOrderSupply'
ALTER TABLE [dbo].[SupplierOrderSupplierOrderSupply]
ADD CONSTRAINT [PK_SupplierOrderSupplierOrderSupply]
    PRIMARY KEY CLUSTERED ([SupplierOrder_Id], [SupplierOrderSupply_Id] ASC);
GO

-- Creating primary key on [OrderCustomer_Id], [DealerOrderCustomer_Id] in table 'OrderCustomerDealerOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerDealerOrderCustomer]
ADD CONSTRAINT [PK_OrderCustomerDealerOrderCustomer]
    PRIMARY KEY CLUSTERED ([OrderCustomer_Id], [DealerOrderCustomer_Id] ASC);
GO

-- Creating primary key on [DealerOrderCustomer_Id], [Dealer_Id] in table 'DealerOrderCustomerDealer'
ALTER TABLE [dbo].[DealerOrderCustomerDealer]
ADD CONSTRAINT [PK_DealerOrderCustomerDealer]
    PRIMARY KEY CLUSTERED ([DealerOrderCustomer_Id], [Dealer_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Product_Id] in table 'ProductOrderCustomerProduct'
ALTER TABLE [dbo].[ProductOrderCustomerProduct]
ADD CONSTRAINT [FK_ProductOrderCustomerProduct_Product]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrderCustomerProduct_Id] in table 'ProductOrderCustomerProduct'
ALTER TABLE [dbo].[ProductOrderCustomerProduct]
ADD CONSTRAINT [FK_ProductOrderCustomerProduct_OrderCustomerProduct]
    FOREIGN KEY ([OrderCustomerProduct_Id])
    REFERENCES [dbo].[OrderCustomerProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrderCustomerProduct_OrderCustomerProduct'
CREATE INDEX [IX_FK_ProductOrderCustomerProduct_OrderCustomerProduct]
ON [dbo].[ProductOrderCustomerProduct]
    ([OrderCustomerProduct_Id]);
GO

-- Creating foreign key on [OrderCustomerProduct_Id] in table 'OrderCustomerProductOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerProductOrderCustomer]
ADD CONSTRAINT [FK_OrderCustomerProductOrderCustomer_OrderCustomerProduct]
    FOREIGN KEY ([OrderCustomerProduct_Id])
    REFERENCES [dbo].[OrderCustomerProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrderCustomer_Id] in table 'OrderCustomerProductOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerProductOrderCustomer]
ADD CONSTRAINT [FK_OrderCustomerProductOrderCustomer_OrderCustomer]
    FOREIGN KEY ([OrderCustomer_Id])
    REFERENCES [dbo].[OrderCustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCustomerProductOrderCustomer_OrderCustomer'
CREATE INDEX [IX_FK_OrderCustomerProductOrderCustomer_OrderCustomer]
ON [dbo].[OrderCustomerProductOrderCustomer]
    ([OrderCustomer_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'OrderCustomerSet'
ALTER TABLE [dbo].[OrderCustomerSet]
ADD CONSTRAINT [FK_OrderCustomerCustomer]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCustomerCustomer'
CREATE INDEX [IX_FK_OrderCustomerCustomer]
ON [dbo].[OrderCustomerSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_RecipeProduct]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipeProduct'
CREATE INDEX [IX_FK_RecipeProduct]
ON [dbo].[RecipeSet]
    ([Product_Id]);
GO

-- Creating foreign key on [Supply_Id] in table 'SupplyRecipeSupply'
ALTER TABLE [dbo].[SupplyRecipeSupply]
ADD CONSTRAINT [FK_SupplyRecipeSupply_Supply]
    FOREIGN KEY ([Supply_Id])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RecipeSupply_Id] in table 'SupplyRecipeSupply'
ALTER TABLE [dbo].[SupplyRecipeSupply]
ADD CONSTRAINT [FK_SupplyRecipeSupply_RecipeSupply]
    FOREIGN KEY ([RecipeSupply_Id])
    REFERENCES [dbo].[RecipeSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplyRecipeSupply_RecipeSupply'
CREATE INDEX [IX_FK_SupplyRecipeSupply_RecipeSupply]
ON [dbo].[SupplyRecipeSupply]
    ([RecipeSupply_Id]);
GO

-- Creating foreign key on [EmployeeID] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_EmployeeProduct]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeProduct'
CREATE INDEX [IX_FK_EmployeeProduct]
ON [dbo].[ProductSet]
    ([EmployeeID]);
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

-- Creating foreign key on [EmployeeID] in table 'OrderCustomerSet'
ALTER TABLE [dbo].[OrderCustomerSet]
ADD CONSTRAINT [FK_EmployeeOrderCustomer]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeOrderCustomer'
CREATE INDEX [IX_FK_EmployeeOrderCustomer]
ON [dbo].[OrderCustomerSet]
    ([EmployeeID]);
GO

-- Creating foreign key on [Recipe_Id] in table 'RecipeRecipeSupply'
ALTER TABLE [dbo].[RecipeRecipeSupply]
ADD CONSTRAINT [FK_RecipeRecipeSupply_Recipe]
    FOREIGN KEY ([Recipe_Id])
    REFERENCES [dbo].[RecipeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RecipeSupply_Id] in table 'RecipeRecipeSupply'
ALTER TABLE [dbo].[RecipeRecipeSupply]
ADD CONSTRAINT [FK_RecipeRecipeSupply_RecipeSupply]
    FOREIGN KEY ([RecipeSupply_Id])
    REFERENCES [dbo].[RecipeSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipeRecipeSupply_RecipeSupply'
CREATE INDEX [IX_FK_RecipeRecipeSupply_RecipeSupply]
ON [dbo].[RecipeRecipeSupply]
    ([RecipeSupply_Id]);
GO

-- Creating foreign key on [EmployeID] in table 'RecipeSet'
ALTER TABLE [dbo].[RecipeSet]
ADD CONSTRAINT [FK_EmployeeRecipe]
    FOREIGN KEY ([EmployeID])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeRecipe'
CREATE INDEX [IX_FK_EmployeeRecipe]
ON [dbo].[RecipeSet]
    ([EmployeID]);
GO

-- Creating foreign key on [EmployeeID] in table 'CustomerSet'
ALTER TABLE [dbo].[CustomerSet]
ADD CONSTRAINT [FK_EmployeeCustomer]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeCustomer'
CREATE INDEX [IX_FK_EmployeeCustomer]
ON [dbo].[CustomerSet]
    ([EmployeeID]);
GO

-- Creating foreign key on [EmployeeRole_Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_EmployeeEmployeeRole]
    FOREIGN KEY ([EmployeeRole_Id])
    REFERENCES [dbo].[EmployeeRoleSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeEmployeeRole'
CREATE INDEX [IX_FK_EmployeeEmployeeRole]
ON [dbo].[EmployeeSet]
    ([EmployeeRole_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'AddressSet'
ALTER TABLE [dbo].[AddressSet]
ADD CONSTRAINT [FK_AddressCustomer]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[CustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressCustomer'
CREATE INDEX [IX_FK_AddressCustomer]
ON [dbo].[AddressSet]
    ([Customer_Id]);
GO

-- Creating foreign key on [Supplier_Id] in table 'SupplierSupplierProduct'
ALTER TABLE [dbo].[SupplierSupplierProduct]
ADD CONSTRAINT [FK_SupplierSupplierProduct_Supplier]
    FOREIGN KEY ([Supplier_Id])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SupplierProduct_Id] in table 'SupplierSupplierProduct'
ALTER TABLE [dbo].[SupplierSupplierProduct]
ADD CONSTRAINT [FK_SupplierSupplierProduct_SupplierProduct]
    FOREIGN KEY ([SupplierProduct_Id])
    REFERENCES [dbo].[SupplierSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplierProduct_SupplierProduct'
CREATE INDEX [IX_FK_SupplierSupplierProduct_SupplierProduct]
ON [dbo].[SupplierSupplierProduct]
    ([SupplierProduct_Id]);
GO

-- Creating foreign key on [SupplierOrderSupply_Id] in table 'SupplierOrderSupplySupply'
ALTER TABLE [dbo].[SupplierOrderSupplySupply]
ADD CONSTRAINT [FK_SupplierOrderSupplySupply_SupplierOrderSupply]
    FOREIGN KEY ([SupplierOrderSupply_Id])
    REFERENCES [dbo].[SupplierOrderSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Supply_Id] in table 'SupplierOrderSupplySupply'
ALTER TABLE [dbo].[SupplierOrderSupplySupply]
ADD CONSTRAINT [FK_SupplierOrderSupplySupply_Supply]
    FOREIGN KEY ([Supply_Id])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierOrderSupplySupply_Supply'
CREATE INDEX [IX_FK_SupplierOrderSupplySupply_Supply]
ON [dbo].[SupplierOrderSupplySupply]
    ([Supply_Id]);
GO

-- Creating foreign key on [SupplierSupply_Id] in table 'SupplierSupplySupply'
ALTER TABLE [dbo].[SupplierSupplySupply]
ADD CONSTRAINT [FK_SupplierSupplySupply_SupplierSupply]
    FOREIGN KEY ([SupplierSupply_Id])
    REFERENCES [dbo].[SupplierSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Supply_Id] in table 'SupplierSupplySupply'
ALTER TABLE [dbo].[SupplierSupplySupply]
ADD CONSTRAINT [FK_SupplierSupplySupply_Supply]
    FOREIGN KEY ([Supply_Id])
    REFERENCES [dbo].[SupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplySupply_Supply'
CREATE INDEX [IX_FK_SupplierSupplySupply_Supply]
ON [dbo].[SupplierSupplySupply]
    ([Supply_Id]);
GO

-- Creating foreign key on [EmployeeID] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [FK_EmployeeSupplierOrder]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeSupplierOrder'
CREATE INDEX [IX_FK_EmployeeSupplierOrder]
ON [dbo].[SupplierOrderSet]
    ([EmployeeID]);
GO

-- Creating foreign key on [SupplierOrder_Id] in table 'SupplierOrderSupplierOrderSupply'
ALTER TABLE [dbo].[SupplierOrderSupplierOrderSupply]
ADD CONSTRAINT [FK_SupplierOrderSupplierOrderSupply_SupplierOrder]
    FOREIGN KEY ([SupplierOrder_Id])
    REFERENCES [dbo].[SupplierOrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SupplierOrderSupply_Id] in table 'SupplierOrderSupplierOrderSupply'
ALTER TABLE [dbo].[SupplierOrderSupplierOrderSupply]
ADD CONSTRAINT [FK_SupplierOrderSupplierOrderSupply_SupplierOrderSupply]
    FOREIGN KEY ([SupplierOrderSupply_Id])
    REFERENCES [dbo].[SupplierOrderSupplySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierOrderSupplierOrderSupply_SupplierOrderSupply'
CREATE INDEX [IX_FK_SupplierOrderSupplierOrderSupply_SupplierOrderSupply]
ON [dbo].[SupplierOrderSupplierOrderSupply]
    ([SupplierOrderSupply_Id]);
GO

-- Creating foreign key on [SupplierID] in table 'SupplierOrderSet'
ALTER TABLE [dbo].[SupplierOrderSet]
ADD CONSTRAINT [FK_SupplierSupplierOrder]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[SupplierSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplierOrder'
CREATE INDEX [IX_FK_SupplierSupplierOrder]
ON [dbo].[SupplierOrderSet]
    ([SupplierID]);
GO

-- Creating foreign key on [OrderCustomer_Id] in table 'OrderCustomerDealerOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerDealerOrderCustomer]
ADD CONSTRAINT [FK_OrderCustomerDealerOrderCustomer_OrderCustomer]
    FOREIGN KEY ([OrderCustomer_Id])
    REFERENCES [dbo].[OrderCustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [DealerOrderCustomer_Id] in table 'OrderCustomerDealerOrderCustomer'
ALTER TABLE [dbo].[OrderCustomerDealerOrderCustomer]
ADD CONSTRAINT [FK_OrderCustomerDealerOrderCustomer_DealerOrderCustomer]
    FOREIGN KEY ([DealerOrderCustomer_Id])
    REFERENCES [dbo].[DealerOrderCustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCustomerDealerOrderCustomer_DealerOrderCustomer'
CREATE INDEX [IX_FK_OrderCustomerDealerOrderCustomer_DealerOrderCustomer]
ON [dbo].[OrderCustomerDealerOrderCustomer]
    ([DealerOrderCustomer_Id]);
GO

-- Creating foreign key on [DealerOrderCustomer_Id] in table 'DealerOrderCustomerDealer'
ALTER TABLE [dbo].[DealerOrderCustomerDealer]
ADD CONSTRAINT [FK_DealerOrderCustomerDealer_DealerOrderCustomer]
    FOREIGN KEY ([DealerOrderCustomer_Id])
    REFERENCES [dbo].[DealerOrderCustomerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Dealer_Id] in table 'DealerOrderCustomerDealer'
ALTER TABLE [dbo].[DealerOrderCustomerDealer]
ADD CONSTRAINT [FK_DealerOrderCustomerDealer_Dealer]
    FOREIGN KEY ([Dealer_Id])
    REFERENCES [dbo].[DealerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DealerOrderCustomerDealer_Dealer'
CREATE INDEX [IX_FK_DealerOrderCustomerDealer_Dealer]
ON [dbo].[DealerOrderCustomerDealer]
    ([Dealer_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------