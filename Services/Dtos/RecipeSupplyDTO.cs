using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [DataContract]
    public class RecipeSupplyDTO
    {
        [DataMember]
        public int RecipeSupplyID { get; set; }
        [DataMember]
        public int RecipeID { get; set; }
        [DataMember]
        public int SupplyID { get; set; }
        [DataMember]
        public decimal UseQuantity { get; set; }
    }
}
