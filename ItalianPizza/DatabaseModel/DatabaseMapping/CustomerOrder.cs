//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CustomerOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerOrder()
        {
            this.CustomerOrderDetail = new HashSet<CustomerOrderDetail>();
            this.FinancialTransaction = new HashSet<FinancialTransaction>();
            this.CustomerOrderCustomer = new HashSet<CustomerOrderCustomer>();
            this.CustomerOrderDeliveryDriver = new HashSet<CustomerOrderDeliveryDriver>();
        }
    
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public System.TimeSpan RegistrationTime { get; set; }
        public int OrderStatusId { get; set; }
        public int OrderTypeId { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual OrderType OrderType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetail { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FinancialTransaction> FinancialTransaction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerOrderCustomer> CustomerOrderCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerOrderDeliveryDriver> CustomerOrderDeliveryDriver { get; set; }
    }
}
