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
    
    public partial class SupplySet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplySet()
        {
            this.RecipeDetailsSet = new HashSet<RecipeDetailsSet>();
            this.SupplierOrderDetailsSet = new HashSet<SupplierOrderDetailsSet>();
            this.SupplierSet = new HashSet<SupplierSet>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public byte[] Picture { get; set; }
        public int SupplyUnitId { get; set; }
        public int ProductStatusId { get; set; }
        public int SupplyTypeId { get; set; }
        public int EmployeeId { get; set; }
        public string IdentificationCode { get; set; }
        public string Observations { get; set; }
    
        public virtual EmployeeSet EmployeeSet { get; set; }
        public virtual ProductStatusSet ProductStatusSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecipeDetailsSet> RecipeDetailsSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierOrderDetailsSet> SupplierOrderDetailsSet { get; set; }
        public virtual SupplyTypeSet SupplyTypeSet { get; set; }
        public virtual SupplyUnitSet SupplyUnitSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierSet> SupplierSet { get; set; }
    }
}
