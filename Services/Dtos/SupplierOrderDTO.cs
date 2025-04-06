using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplierOrderDTO
    {
        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public int SupplyID { get; set; }

        [DataMember]
        public DateTime OrderedDate { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public string Status { get; set; } 
    }
}
