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
    
    public partial class SupplierOrderDetails
    {
        public int Id { get; set; }
        public int SupplyQuantity { get; set; }
        public double PricePerUnit { get; set; }
        public int SupplierOrderId { get; set; }
        public int SupplyId { get; set; }
    
        public virtual SupplierOrder SupplierOrder { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
