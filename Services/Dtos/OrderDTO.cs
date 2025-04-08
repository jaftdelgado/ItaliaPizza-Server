using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class OrderDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
