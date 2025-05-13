using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class TransactionDTO
    {
        [DataMember]
        public int TransactionID { get; set; }

        [DataMember]
        public int CashRegisterID { get; set; }

        [DataMember]
        public char FinancialFlow { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public int Concept { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? OrderID { get; set; }

        [DataMember]
        public int? SupplierOrderID { get; set; }

    }
}
