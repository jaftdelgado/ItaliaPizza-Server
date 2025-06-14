﻿using System.Runtime.Serialization;

namespace Services.Dtos
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
