using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class RecipeDTO
    {
        [DataMember]
        public int RecipeID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int PreparationTime { get; set; }

        [DataMember]
        public List<RecipeStepDTO> Steps { get; set; }

        [DataMember]
        public List<RecipeSupplyDTO> Supplies { get; set; }

    }
}