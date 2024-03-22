
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/21/2024 16:57:11
-- Generated from EDMX file: C:\Users\Luis Alonso\Documents\ItaliaPizza\ItalianPizza\DatabaseModel\DatabaseMapping\DataModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_ProductoOrdenCliente_Producto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoOrdenCliente] DROP CONSTRAINT [FK_ProductoOrdenCliente_Producto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoOrdenCliente_OrdenCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoOrdenCliente] DROP CONSTRAINT [FK_ProductoOrdenCliente_OrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoSet] DROP CONSTRAINT [FK_EmpleadoProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoOrdenCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrdenClienteSet] DROP CONSTRAINT [FK_EmpleadoOrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoPedidoProveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidoProveedorSet] DROP CONSTRAINT [FK_EmpleadoPedidoProveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoInsumo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InsumoSet] DROP CONSTRAINT [FK_EmpleadoInsumo];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoProveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProveedorSet] DROP CONSTRAINT [FK_EmpleadoProveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoRepartidor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepartidorSet] DROP CONSTRAINT [FK_EmpleadoRepartidor];
GO
IF OBJECT_ID(N'[dbo].[FK_ProveedorPedidoProveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidoProveedorSet] DROP CONSTRAINT [FK_ProveedorPedidoProveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_InsumoPedidoProveedor_Insumo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InsumoPedidoProveedor] DROP CONSTRAINT [FK_InsumoPedidoProveedor_Insumo];
GO
IF OBJECT_ID(N'[dbo].[FK_InsumoPedidoProveedor_PedidoProveedor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InsumoPedidoProveedor] DROP CONSTRAINT [FK_InsumoPedidoProveedor_PedidoProveedor];
GO
IF OBJECT_ID(N'[dbo].[FK_OrdenClienteRepartidor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepartidorSet] DROP CONSTRAINT [FK_OrdenClienteRepartidor];
GO
IF OBJECT_ID(N'[dbo].[FK_CorteDiarioOrdenCliente_CorteDiario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CorteDiarioOrdenCliente] DROP CONSTRAINT [FK_CorteDiarioOrdenCliente_CorteDiario];
GO
IF OBJECT_ID(N'[dbo].[FK_CorteDiarioOrdenCliente_OrdenCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CorteDiarioOrdenCliente] DROP CONSTRAINT [FK_CorteDiarioOrdenCliente_OrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoCorteDiario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CorteDiarioSet] DROP CONSTRAINT [FK_EmpleadoCorteDiario];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoValidacionInventario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValidacionInventarioSet] DROP CONSTRAINT [FK_EmpleadoValidacionInventario];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteOrdenCliente_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClienteOrdenCliente] DROP CONSTRAINT [FK_ClienteOrdenCliente_Cliente];
GO
IF OBJECT_ID(N'[dbo].[FK_ClienteOrdenCliente_OrdenCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClienteOrdenCliente] DROP CONSTRAINT [FK_ClienteOrdenCliente_OrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_DirecciónCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClienteSet] DROP CONSTRAINT [FK_DirecciónCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_DirecciónEmpleado]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpleadoSet] DROP CONSTRAINT [FK_DirecciónEmpleado];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoCuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmpleadoSet] DROP CONSTRAINT [FK_EmpleadoCuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_OrdenClienteOrdenClienteDetalle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrdenClienteDetalleSet] DROP CONSTRAINT [FK_OrdenClienteOrdenClienteDetalle];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoOrdenClienteDetalle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrdenClienteDetalleSet] DROP CONSTRAINT [FK_ProductoOrdenClienteDetalle];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoProveedorPedidoProveedorDetalle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidoProveedorDetalleSet] DROP CONSTRAINT [FK_PedidoProveedorPedidoProveedorDetalle];
GO
IF OBJECT_ID(N'[dbo].[FK_InsumoPedidoProveedorDetalle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PedidoProveedorDetalleSet] DROP CONSTRAINT [FK_InsumoPedidoProveedorDetalle];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProductoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoSet];
GO
IF OBJECT_ID(N'[dbo].[OrdenClienteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrdenClienteSet];
GO
IF OBJECT_ID(N'[dbo].[EmpleadoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmpleadoSet];
GO
IF OBJECT_ID(N'[dbo].[ProveedorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProveedorSet];
GO
IF OBJECT_ID(N'[dbo].[ClienteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClienteSet];
GO
IF OBJECT_ID(N'[dbo].[DirecciónSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DirecciónSet];
GO
IF OBJECT_ID(N'[dbo].[RepartidorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepartidorSet];
GO
IF OBJECT_ID(N'[dbo].[InsumoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InsumoSet];
GO
IF OBJECT_ID(N'[dbo].[PedidoProveedorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PedidoProveedorSet];
GO
IF OBJECT_ID(N'[dbo].[CorteDiarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CorteDiarioSet];
GO
IF OBJECT_ID(N'[dbo].[ValidacionInventarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValidacionInventarioSet];
GO
IF OBJECT_ID(N'[dbo].[CuentaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CuentaSet];
GO
IF OBJECT_ID(N'[dbo].[OrdenClienteDetalleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrdenClienteDetalleSet];
GO
IF OBJECT_ID(N'[dbo].[PedidoProveedorDetalleSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PedidoProveedorDetalleSet];
GO
IF OBJECT_ID(N'[dbo].[ProductoOrdenCliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoOrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[InsumoPedidoProveedor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InsumoPedidoProveedor];
GO
IF OBJECT_ID(N'[dbo].[CorteDiarioOrdenCliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CorteDiarioOrdenCliente];
GO
IF OBJECT_ID(N'[dbo].[ClienteOrdenCliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClienteOrdenCliente];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProductoSet'
CREATE TABLE [dbo].[ProductoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Costo] float  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Categoria] nvarchar(max)  NOT NULL,
    [Foto] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL
);
GO

-- Creating table 'OrdenClienteSet'
CREATE TABLE [dbo].[OrdenClienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Fecha] nvarchar(max)  NULL,
    [Hora] time  NOT NULL,
    [CostoTotal] float  NOT NULL,
    [EmpleadoId] int  NOT NULL,
    [Estado] nvarchar(max)  NULL
);
GO

-- Creating table 'EmpleadoSet'
CREATE TABLE [dbo].[EmpleadoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(max)  NOT NULL,
    [ApellidoPaterno] nvarchar(max)  NOT NULL,
    [ApellidoMaterno] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [DirecciónId] int  NOT NULL,
    [Cuenta_Id] int  NOT NULL
);
GO

-- Creating table 'ProveedorSet'
CREATE TABLE [dbo].[ProveedorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [ApellidoPaterno] nvarchar(max)  NOT NULL,
    [ApellidoMaterno] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [Empresa] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL
);
GO

-- Creating table 'ClienteSet'
CREATE TABLE [dbo].[ClienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(max)  NOT NULL,
    [ApellidoPaterno] nvarchar(max)  NOT NULL,
    [ApellidoMaterno] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [DirecciónId] int  NOT NULL
);
GO

-- Creating table 'DirecciónSet'
CREATE TABLE [dbo].[DirecciónSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Calle] nvarchar(max)  NOT NULL,
    [Ciudad] nvarchar(max)  NOT NULL,
    [CodigoPostal] int  NOT NULL,
    [NumeroCalle] int  NOT NULL
);
GO

-- Creating table 'RepartidorSet'
CREATE TABLE [dbo].[RepartidorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(max)  NOT NULL,
    [ApellidoPaterno] nvarchar(max)  NOT NULL,
    [ApellidoMaterno] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL,
    [OrdenCliente_Id] int  NOT NULL
);
GO

-- Creating table 'InsumoSet'
CREATE TABLE [dbo].[InsumoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Costo] float  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Tipo] nvarchar(max)  NOT NULL,
    [Cantidad] int  NOT NULL,
    [Foto] nvarchar(max)  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL
);
GO

-- Creating table 'PedidoProveedorSet'
CREATE TABLE [dbo].[PedidoProveedorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Hora] time  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [CostoTotal] float  NOT NULL,
    [EmpleadoId] int  NOT NULL,
    [ProveedorId] int  NOT NULL
);
GO

-- Creating table 'CorteDiarioSet'
CREATE TABLE [dbo].[CorteDiarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Hora] time  NOT NULL,
    [MontoTotal] float  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL
);
GO

-- Creating table 'ValidacionInventarioSet'
CREATE TABLE [dbo].[ValidacionInventarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Hora] time  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [EmpleadoId] int  NOT NULL
);
GO

-- Creating table 'CuentaSet'
CREATE TABLE [dbo].[CuentaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Usuario] nvarchar(max)  NOT NULL,
    [Contraseña] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrdenClienteDetalleSet'
CREATE TABLE [dbo].[OrdenClienteDetalleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrdenClienteId] int  NOT NULL,
    [ProductoId] int  NOT NULL,
    [CantidadProducto] nvarchar(max)  NOT NULL,
    [CostoProducto] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PedidoProveedorDetalleSet'
CREATE TABLE [dbo].[PedidoProveedorDetalleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PedidoProveedorId] int  NOT NULL,
    [InsumoId] int  NOT NULL,
    [CantidadInsumo] nvarchar(max)  NOT NULL,
    [CostoInsumo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductoOrdenCliente'
CREATE TABLE [dbo].[ProductoOrdenCliente] (
    [Producto_Id] int  NOT NULL,
    [OrdenCliente_Id] int  NOT NULL
);
GO

-- Creating table 'InsumoPedidoProveedor'
CREATE TABLE [dbo].[InsumoPedidoProveedor] (
    [Insumo_Id] int  NOT NULL,
    [PedidoProveedor_Id] int  NOT NULL
);
GO

-- Creating table 'CorteDiarioOrdenCliente'
CREATE TABLE [dbo].[CorteDiarioOrdenCliente] (
    [CorteDiario_Id] int  NOT NULL,
    [OrdenCliente_Id] int  NOT NULL
);
GO

-- Creating table 'ClienteOrdenCliente'
CREATE TABLE [dbo].[ClienteOrdenCliente] (
    [Cliente_Id] int  NOT NULL,
    [OrdenCliente_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProductoSet'
ALTER TABLE [dbo].[ProductoSet]
ADD CONSTRAINT [PK_ProductoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrdenClienteSet'
ALTER TABLE [dbo].[OrdenClienteSet]
ADD CONSTRAINT [PK_OrdenClienteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmpleadoSet'
ALTER TABLE [dbo].[EmpleadoSet]
ADD CONSTRAINT [PK_EmpleadoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProveedorSet'
ALTER TABLE [dbo].[ProveedorSet]
ADD CONSTRAINT [PK_ProveedorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClienteSet'
ALTER TABLE [dbo].[ClienteSet]
ADD CONSTRAINT [PK_ClienteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DirecciónSet'
ALTER TABLE [dbo].[DirecciónSet]
ADD CONSTRAINT [PK_DirecciónSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RepartidorSet'
ALTER TABLE [dbo].[RepartidorSet]
ADD CONSTRAINT [PK_RepartidorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InsumoSet'
ALTER TABLE [dbo].[InsumoSet]
ADD CONSTRAINT [PK_InsumoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PedidoProveedorSet'
ALTER TABLE [dbo].[PedidoProveedorSet]
ADD CONSTRAINT [PK_PedidoProveedorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CorteDiarioSet'
ALTER TABLE [dbo].[CorteDiarioSet]
ADD CONSTRAINT [PK_CorteDiarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValidacionInventarioSet'
ALTER TABLE [dbo].[ValidacionInventarioSet]
ADD CONSTRAINT [PK_ValidacionInventarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CuentaSet'
ALTER TABLE [dbo].[CuentaSet]
ADD CONSTRAINT [PK_CuentaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrdenClienteDetalleSet'
ALTER TABLE [dbo].[OrdenClienteDetalleSet]
ADD CONSTRAINT [PK_OrdenClienteDetalleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PedidoProveedorDetalleSet'
ALTER TABLE [dbo].[PedidoProveedorDetalleSet]
ADD CONSTRAINT [PK_PedidoProveedorDetalleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Producto_Id], [OrdenCliente_Id] in table 'ProductoOrdenCliente'
ALTER TABLE [dbo].[ProductoOrdenCliente]
ADD CONSTRAINT [PK_ProductoOrdenCliente]
    PRIMARY KEY CLUSTERED ([Producto_Id], [OrdenCliente_Id] ASC);
GO

-- Creating primary key on [Insumo_Id], [PedidoProveedor_Id] in table 'InsumoPedidoProveedor'
ALTER TABLE [dbo].[InsumoPedidoProveedor]
ADD CONSTRAINT [PK_InsumoPedidoProveedor]
    PRIMARY KEY CLUSTERED ([Insumo_Id], [PedidoProveedor_Id] ASC);
GO

-- Creating primary key on [CorteDiario_Id], [OrdenCliente_Id] in table 'CorteDiarioOrdenCliente'
ALTER TABLE [dbo].[CorteDiarioOrdenCliente]
ADD CONSTRAINT [PK_CorteDiarioOrdenCliente]
    PRIMARY KEY CLUSTERED ([CorteDiario_Id], [OrdenCliente_Id] ASC);
GO

-- Creating primary key on [Cliente_Id], [OrdenCliente_Id] in table 'ClienteOrdenCliente'
ALTER TABLE [dbo].[ClienteOrdenCliente]
ADD CONSTRAINT [PK_ClienteOrdenCliente]
    PRIMARY KEY CLUSTERED ([Cliente_Id], [OrdenCliente_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Producto_Id] in table 'ProductoOrdenCliente'
ALTER TABLE [dbo].[ProductoOrdenCliente]
ADD CONSTRAINT [FK_ProductoOrdenCliente_Producto]
    FOREIGN KEY ([Producto_Id])
    REFERENCES [dbo].[ProductoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrdenCliente_Id] in table 'ProductoOrdenCliente'
ALTER TABLE [dbo].[ProductoOrdenCliente]
ADD CONSTRAINT [FK_ProductoOrdenCliente_OrdenCliente]
    FOREIGN KEY ([OrdenCliente_Id])
    REFERENCES [dbo].[OrdenClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoOrdenCliente_OrdenCliente'
CREATE INDEX [IX_FK_ProductoOrdenCliente_OrdenCliente]
ON [dbo].[ProductoOrdenCliente]
    ([OrdenCliente_Id]);
GO

-- Creating foreign key on [EmpleadoId] in table 'ProductoSet'
ALTER TABLE [dbo].[ProductoSet]
ADD CONSTRAINT [FK_EmpleadoProducto]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoProducto'
CREATE INDEX [IX_FK_EmpleadoProducto]
ON [dbo].[ProductoSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'OrdenClienteSet'
ALTER TABLE [dbo].[OrdenClienteSet]
ADD CONSTRAINT [FK_EmpleadoOrdenCliente]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoOrdenCliente'
CREATE INDEX [IX_FK_EmpleadoOrdenCliente]
ON [dbo].[OrdenClienteSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'PedidoProveedorSet'
ALTER TABLE [dbo].[PedidoProveedorSet]
ADD CONSTRAINT [FK_EmpleadoPedidoProveedor]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoPedidoProveedor'
CREATE INDEX [IX_FK_EmpleadoPedidoProveedor]
ON [dbo].[PedidoProveedorSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'InsumoSet'
ALTER TABLE [dbo].[InsumoSet]
ADD CONSTRAINT [FK_EmpleadoInsumo]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoInsumo'
CREATE INDEX [IX_FK_EmpleadoInsumo]
ON [dbo].[InsumoSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'ProveedorSet'
ALTER TABLE [dbo].[ProveedorSet]
ADD CONSTRAINT [FK_EmpleadoProveedor]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoProveedor'
CREATE INDEX [IX_FK_EmpleadoProveedor]
ON [dbo].[ProveedorSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'RepartidorSet'
ALTER TABLE [dbo].[RepartidorSet]
ADD CONSTRAINT [FK_EmpleadoRepartidor]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoRepartidor'
CREATE INDEX [IX_FK_EmpleadoRepartidor]
ON [dbo].[RepartidorSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [ProveedorId] in table 'PedidoProveedorSet'
ALTER TABLE [dbo].[PedidoProveedorSet]
ADD CONSTRAINT [FK_ProveedorPedidoProveedor]
    FOREIGN KEY ([ProveedorId])
    REFERENCES [dbo].[ProveedorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProveedorPedidoProveedor'
CREATE INDEX [IX_FK_ProveedorPedidoProveedor]
ON [dbo].[PedidoProveedorSet]
    ([ProveedorId]);
GO

-- Creating foreign key on [Insumo_Id] in table 'InsumoPedidoProveedor'
ALTER TABLE [dbo].[InsumoPedidoProveedor]
ADD CONSTRAINT [FK_InsumoPedidoProveedor_Insumo]
    FOREIGN KEY ([Insumo_Id])
    REFERENCES [dbo].[InsumoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PedidoProveedor_Id] in table 'InsumoPedidoProveedor'
ALTER TABLE [dbo].[InsumoPedidoProveedor]
ADD CONSTRAINT [FK_InsumoPedidoProveedor_PedidoProveedor]
    FOREIGN KEY ([PedidoProveedor_Id])
    REFERENCES [dbo].[PedidoProveedorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InsumoPedidoProveedor_PedidoProveedor'
CREATE INDEX [IX_FK_InsumoPedidoProveedor_PedidoProveedor]
ON [dbo].[InsumoPedidoProveedor]
    ([PedidoProveedor_Id]);
GO

-- Creating foreign key on [OrdenCliente_Id] in table 'RepartidorSet'
ALTER TABLE [dbo].[RepartidorSet]
ADD CONSTRAINT [FK_OrdenClienteRepartidor]
    FOREIGN KEY ([OrdenCliente_Id])
    REFERENCES [dbo].[OrdenClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrdenClienteRepartidor'
CREATE INDEX [IX_FK_OrdenClienteRepartidor]
ON [dbo].[RepartidorSet]
    ([OrdenCliente_Id]);
GO

-- Creating foreign key on [CorteDiario_Id] in table 'CorteDiarioOrdenCliente'
ALTER TABLE [dbo].[CorteDiarioOrdenCliente]
ADD CONSTRAINT [FK_CorteDiarioOrdenCliente_CorteDiario]
    FOREIGN KEY ([CorteDiario_Id])
    REFERENCES [dbo].[CorteDiarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrdenCliente_Id] in table 'CorteDiarioOrdenCliente'
ALTER TABLE [dbo].[CorteDiarioOrdenCliente]
ADD CONSTRAINT [FK_CorteDiarioOrdenCliente_OrdenCliente]
    FOREIGN KEY ([OrdenCliente_Id])
    REFERENCES [dbo].[OrdenClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CorteDiarioOrdenCliente_OrdenCliente'
CREATE INDEX [IX_FK_CorteDiarioOrdenCliente_OrdenCliente]
ON [dbo].[CorteDiarioOrdenCliente]
    ([OrdenCliente_Id]);
GO

-- Creating foreign key on [EmpleadoId] in table 'CorteDiarioSet'
ALTER TABLE [dbo].[CorteDiarioSet]
ADD CONSTRAINT [FK_EmpleadoCorteDiario]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoCorteDiario'
CREATE INDEX [IX_FK_EmpleadoCorteDiario]
ON [dbo].[CorteDiarioSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [EmpleadoId] in table 'ValidacionInventarioSet'
ALTER TABLE [dbo].[ValidacionInventarioSet]
ADD CONSTRAINT [FK_EmpleadoValidacionInventario]
    FOREIGN KEY ([EmpleadoId])
    REFERENCES [dbo].[EmpleadoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoValidacionInventario'
CREATE INDEX [IX_FK_EmpleadoValidacionInventario]
ON [dbo].[ValidacionInventarioSet]
    ([EmpleadoId]);
GO

-- Creating foreign key on [Cliente_Id] in table 'ClienteOrdenCliente'
ALTER TABLE [dbo].[ClienteOrdenCliente]
ADD CONSTRAINT [FK_ClienteOrdenCliente_Cliente]
    FOREIGN KEY ([Cliente_Id])
    REFERENCES [dbo].[ClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [OrdenCliente_Id] in table 'ClienteOrdenCliente'
ALTER TABLE [dbo].[ClienteOrdenCliente]
ADD CONSTRAINT [FK_ClienteOrdenCliente_OrdenCliente]
    FOREIGN KEY ([OrdenCliente_Id])
    REFERENCES [dbo].[OrdenClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteOrdenCliente_OrdenCliente'
CREATE INDEX [IX_FK_ClienteOrdenCliente_OrdenCliente]
ON [dbo].[ClienteOrdenCliente]
    ([OrdenCliente_Id]);
GO

-- Creating foreign key on [DirecciónId] in table 'ClienteSet'
ALTER TABLE [dbo].[ClienteSet]
ADD CONSTRAINT [FK_DirecciónCliente]
    FOREIGN KEY ([DirecciónId])
    REFERENCES [dbo].[DirecciónSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DirecciónCliente'
CREATE INDEX [IX_FK_DirecciónCliente]
ON [dbo].[ClienteSet]
    ([DirecciónId]);
GO

-- Creating foreign key on [DirecciónId] in table 'EmpleadoSet'
ALTER TABLE [dbo].[EmpleadoSet]
ADD CONSTRAINT [FK_DirecciónEmpleado]
    FOREIGN KEY ([DirecciónId])
    REFERENCES [dbo].[DirecciónSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DirecciónEmpleado'
CREATE INDEX [IX_FK_DirecciónEmpleado]
ON [dbo].[EmpleadoSet]
    ([DirecciónId]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'EmpleadoSet'
ALTER TABLE [dbo].[EmpleadoSet]
ADD CONSTRAINT [FK_EmpleadoCuenta]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[CuentaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoCuenta'
CREATE INDEX [IX_FK_EmpleadoCuenta]
ON [dbo].[EmpleadoSet]
    ([Cuenta_Id]);
GO

-- Creating foreign key on [OrdenClienteId] in table 'OrdenClienteDetalleSet'
ALTER TABLE [dbo].[OrdenClienteDetalleSet]
ADD CONSTRAINT [FK_OrdenClienteOrdenClienteDetalle]
    FOREIGN KEY ([OrdenClienteId])
    REFERENCES [dbo].[OrdenClienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrdenClienteOrdenClienteDetalle'
CREATE INDEX [IX_FK_OrdenClienteOrdenClienteDetalle]
ON [dbo].[OrdenClienteDetalleSet]
    ([OrdenClienteId]);
GO

-- Creating foreign key on [ProductoId] in table 'OrdenClienteDetalleSet'
ALTER TABLE [dbo].[OrdenClienteDetalleSet]
ADD CONSTRAINT [FK_ProductoOrdenClienteDetalle]
    FOREIGN KEY ([ProductoId])
    REFERENCES [dbo].[ProductoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoOrdenClienteDetalle'
CREATE INDEX [IX_FK_ProductoOrdenClienteDetalle]
ON [dbo].[OrdenClienteDetalleSet]
    ([ProductoId]);
GO

-- Creating foreign key on [PedidoProveedorId] in table 'PedidoProveedorDetalleSet'
ALTER TABLE [dbo].[PedidoProveedorDetalleSet]
ADD CONSTRAINT [FK_PedidoProveedorPedidoProveedorDetalle]
    FOREIGN KEY ([PedidoProveedorId])
    REFERENCES [dbo].[PedidoProveedorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoProveedorPedidoProveedorDetalle'
CREATE INDEX [IX_FK_PedidoProveedorPedidoProveedorDetalle]
ON [dbo].[PedidoProveedorDetalleSet]
    ([PedidoProveedorId]);
GO

-- Creating foreign key on [InsumoId] in table 'PedidoProveedorDetalleSet'
ALTER TABLE [dbo].[PedidoProveedorDetalleSet]
ADD CONSTRAINT [FK_InsumoPedidoProveedorDetalle]
    FOREIGN KEY ([InsumoId])
    REFERENCES [dbo].[InsumoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InsumoPedidoProveedorDetalle'
CREATE INDEX [IX_FK_InsumoPedidoProveedorDetalle]
ON [dbo].[PedidoProveedorDetalleSet]
    ([InsumoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------