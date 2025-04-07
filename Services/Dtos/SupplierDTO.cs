using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplierDTO
    {
        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public int CategorySupply { get; set; } 
    }
}
