﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItalianPizza.DatabaseModel.DatabaseMapping
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ItalianPizzaServerBDEntities : DbContext
    {
        public ItalianPizzaServerBDEntities()
            : base("name=ItalianPizzaServerBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ProductSale> ProductSaleSet { get; set; }
        public virtual DbSet<CustomerOrderDetail> CustomerOrderDetailSet { get; set; }
        public virtual DbSet<ProductType> ProductTypeSet { get; set; }
        public virtual DbSet<OrderType> OrderTypeSet { get; set; }
        public virtual DbSet<ProductStatus> ProductStatusSet { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrderSet { get; set; }
        public virtual DbSet<OrderStatus> OrderStatusSet { get; set; }
        public virtual DbSet<DailyClosing> DailyClosingSet { get; set; }
        public virtual DbSet<InventoryValidation> InventoryValidationSet { get; set; }
        public virtual DbSet<SupplierOrderDetails> SupplierOrderDetailsSet { get; set; }
        public virtual DbSet<Supply> SupplySet { get; set; }
        public virtual DbSet<SupplyUnit> SupplyUnitSet { get; set; }
        public virtual DbSet<FinancialTransaction> FinancialTransactionSet { get; set; }
        public virtual DbSet<Recipe> RecipeSet { get; set; }
        public virtual DbSet<RecipeDetails> RecipeDetailsSet { get; set; }
        public virtual DbSet<UserAccount> UserAccountSet { get; set; }
        public virtual DbSet<Employee> EmployeeSet { get; set; }
        public virtual DbSet<Customer> CustomerSet { get; set; }
        public virtual DbSet<DeliveryDriver> DeliveryDriverSet { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositionSet { get; set; }
        public virtual DbSet<UserStatus> UserStatusSet { get; set; }
        public virtual DbSet<Supplier> SupplierSet { get; set; }
        public virtual DbSet<Address> AddressSet { get; set; }
        public virtual DbSet<SupplierOrder> SupplierOrderSet { get; set; }
        public virtual DbSet<SupplyType> SupplyTypeSet { get; set; }
        public virtual DbSet<CustomerOrderDeliveryDriver> CustomerOrderDeliveryDriverSet { get; set; }
        public virtual DbSet<CustomerOrderCustomer> CustomerOrderCustomerSet { get; set; }
    }
}
