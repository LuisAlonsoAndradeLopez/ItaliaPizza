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
    
<<<<<<<< HEAD:ItalianPizza/DatabaseModel/DatabaseMapping/InventoryValidation.cs
    public partial class InventoryValidation
    {
        public int Id { get; set; }
        public System.DateTime InventoryValidationDate { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual Employee Employee { get; set; }
========
    public partial class RecipeDetailsSet
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int SupplyId { get; set; }
        public int RecipeId { get; set; }
    
        public virtual RecipeSet RecipeSet { get; set; }
        public virtual SupplySet SupplySet { get; set; }
>>>>>>>> d1b5b603a1726b12cc340c733b91b809aceecd0d:ItalianPizza/DatabaseModel/DatabaseMapping/RecipeDetailsSet.cs
    }
}
