using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [DataContract]
    public class RecipeDTO
    {
        [DataMember]
        public int RecipeID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int PreparationTime { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public ProductDTO Product { get; set; }
        [DataMember]

        public string ProductName { get; set; }

    }
}
