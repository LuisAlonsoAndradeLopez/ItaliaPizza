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
    
    public partial class PedidoProveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PedidoProveedor()
        {
            this.Insumo = new HashSet<Insumo>();
            this.PedidoProveedorDetalle = new HashSet<PedidoProveedorDetalle>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }
        public string Estado { get; set; }
        public double CostoTotal { get; set; }
        public int EmpleadoId { get; set; }
        public int ProveedorId { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Insumo> Insumo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoProveedorDetalle> PedidoProveedorDetalle { get; set; }
    }
}