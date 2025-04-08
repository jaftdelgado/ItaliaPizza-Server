using System.Runtime.Serialization;
using System;

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

    [DataMember]
    public int? OrderID { get; set; }

    [DataMember]
    public int? CashRegisterID { get; set; }
}
