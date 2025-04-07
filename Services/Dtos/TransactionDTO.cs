using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class TransactionDTO
    {
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
