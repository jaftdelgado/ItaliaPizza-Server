//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RecipeSupply
    {
        public int RecipeSupplyID { get; set; }
        public int RecipeID { get; set; }
        public int SupplyID { get; set; }
        public decimal UseQuantity { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
