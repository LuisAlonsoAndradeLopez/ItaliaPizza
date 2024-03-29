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
    
    public partial class Insumo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insumo()
        {
            this.PedidoProveedor = new HashSet<PedidoProveedor>();
            this.PedidoProveedorDetalle = new HashSet<PedidoProveedorDetalle>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Costo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int Cantidad { get; set; }
        public string Foto { get; set; }
        public string Estado { get; set; }
        public int EmpleadoId { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoProveedor> PedidoProveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoProveedorDetalle> PedidoProveedorDetalle { get; set; }
    }
}
