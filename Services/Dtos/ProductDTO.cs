using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public bool IsPrepared { get; set; }

        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
