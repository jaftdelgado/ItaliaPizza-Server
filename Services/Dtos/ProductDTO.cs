using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? Category { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public bool? IsPrepared { get; set; }

        [DataMember]
        public byte[] ProductPic { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int? SupplyID { get; set; }
        
        [DataMember]
        public int? RecipeID { get; set; }

        [DataMember]
        public RecipeDTO Recipe { get; set; }

        [DataMember]
        public bool IsDeletable { get; set; }

    }
}
