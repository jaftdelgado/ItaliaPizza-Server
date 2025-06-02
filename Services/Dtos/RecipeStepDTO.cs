using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class RecipeStepDTO
    {
        [DataMember]
        public int RecipeStepID { get; set; }

        [DataMember]
        public int RecipeID { get; set; }

        [DataMember]
        public int StepNumber { get; set; }

        [DataMember]
        public string Instruction { get; set; }
    }
}
