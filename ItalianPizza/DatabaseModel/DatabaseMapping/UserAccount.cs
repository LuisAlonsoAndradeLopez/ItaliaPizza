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
    
<<<<<<<< HEAD:ItalianPizza/DatabaseModel/DatabaseMapping/UserAccount.cs
    public partial class UserAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    
        public virtual Employee Employee { get; set; }
========
    public partial class InventoryValidationSet
    {
        public int Id { get; set; }
        public System.DateTime InventoryValidationDate { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual EmployeeSet EmployeeSet { get; set; }
>>>>>>>> d1b5b603a1726b12cc340c733b91b809aceecd0d:ItalianPizza/DatabaseModel/DatabaseMapping/InventoryValidationSet.cs
    }
}
