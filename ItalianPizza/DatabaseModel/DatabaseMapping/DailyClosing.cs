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
    
<<<<<<<< HEAD:ItalianPizza/DatabaseModel/DatabaseMapping/DailyClosing.cs
    public partial class DailyClosing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DailyClosing()
        {
            this.FinancialTransaction = new HashSet<FinancialTransaction>();
========
    public partial class DailyClosingSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DailyClosingSet()
        {
            this.FinancialTransactionSet = new HashSet<FinancialTransactionSet>();
>>>>>>>> d1b5b603a1726b12cc340c733b91b809aceecd0d:ItalianPizza/DatabaseModel/DatabaseMapping/DailyClosingSet.cs
        }
    
        public int Id { get; set; }
        public System.DateTime ClosingDate { get; set; }
        public string Description { get; set; }
        public double TotalAmount { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual EmployeeSet EmployeeSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
<<<<<<<< HEAD:ItalianPizza/DatabaseModel/DatabaseMapping/DailyClosing.cs
        public virtual ICollection<FinancialTransaction> FinancialTransaction { get; set; }
        public virtual Employee Employee { get; set; }
========
        public virtual ICollection<FinancialTransactionSet> FinancialTransactionSet { get; set; }
>>>>>>>> d1b5b603a1726b12cc340c733b91b809aceecd0d:ItalianPizza/DatabaseModel/DatabaseMapping/DailyClosingSet.cs
    }
}
