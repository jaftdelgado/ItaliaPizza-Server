using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class CashRegisterDTO
    {
        [DataMember]
        public int CashRegisterID { get; set; }

        [DataMember]
        public DateTime OpeningDate { get; set; }

        [DataMember]
        public decimal InitialBalance { get; set; }

        [DataMember]
        public DateTime? ClosingDate { get; set; }

        [DataMember]
        public decimal? FinalBalance { get; set; }

        [DataMember]
        public decimal? CashierAmount { get; set; }
    }
}
