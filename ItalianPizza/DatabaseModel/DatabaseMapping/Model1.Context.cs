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
    
        public virtual DbSet<AddressSet> AddressSet { get; set; }
        public virtual DbSet<CustomerOrderCustomerSet> CustomerOrderCustomerSet { get; set; }
        public virtual DbSet<CustomerOrderDeliveryDriverSet> CustomerOrderDeliveryDriverSet { get; set; }
        public virtual DbSet<CustomerOrderDetailSet> CustomerOrderDetailSet { get; set; }
        public virtual DbSet<CustomerOrderSet> CustomerOrderSet { get; set; }
        public virtual DbSet<CustomerSet> CustomerSet { get; set; }
        public virtual DbSet<DailyClosingSet> DailyClosingSet { get; set; }
        public virtual DbSet<DeliveryDriverSet> DeliveryDriverSet { get; set; }
        public virtual DbSet<EmployeePositionSet> EmployeePositionSet { get; set; }
        public virtual DbSet<EmployeeSet> EmployeeSet { get; set; }
        public virtual DbSet<FinancialTransactionSet> FinancialTransactionSet { get; set; }
        public virtual DbSet<InventoryValidationSet> InventoryValidationSet { get; set; }
        public virtual DbSet<OrderStatusSet> OrderStatusSet { get; set; }
        public virtual DbSet<OrderTypeSet> OrderTypeSet { get; set; }
        public virtual DbSet<ProductSaleSet> ProductSaleSet { get; set; }
        public virtual DbSet<ProductStatusSet> ProductStatusSet { get; set; }
        public virtual DbSet<ProductTypeSet> ProductTypeSet { get; set; }
        public virtual DbSet<RecipeDetailsSet> RecipeDetailsSet { get; set; }
        public virtual DbSet<RecipeSet> RecipeSet { get; set; }
        public virtual DbSet<SupplierOrderDetailsSet> SupplierOrderDetailsSet { get; set; }
        public virtual DbSet<SupplierOrderSet> SupplierOrderSet { get; set; }
        public virtual DbSet<SupplierSet> SupplierSet { get; set; }
        public virtual DbSet<SupplySet> SupplySet { get; set; }
        public virtual DbSet<SupplyTypeSet> SupplyTypeSet { get; set; }
        public virtual DbSet<SupplyUnitSet> SupplyUnitSet { get; set; }
        public virtual DbSet<UserAccountSet> UserAccountSet { get; set; }
        public virtual DbSet<UserStatusSet> UserStatusSet { get; set; }
        public virtual DbSet<EmployeePictureSet> EmployeePictureSet { get; set; }
        public virtual DbSet<ProductPictureSet> ProductPictureSet { get; set; }
        public virtual DbSet<SupplyPictureSet> SupplyPictureSet { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
        public virtual DbSet<FinancialTransactionIncomeContextSet> FinancialTransactionIncomeContextSet { get; set; }
        public virtual DbSet<FinancialTransactionWithDrawContextSet> FinancialTransactionWithDrawContextSet { get; set; }
    }
}
