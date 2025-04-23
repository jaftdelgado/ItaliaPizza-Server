using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

[DataContract]
public class OrderSummaryDTO
{
    [DataMember]
    public int OrderID { get; set; }

    [DataMember]
    public DateTime OrderDate { get; set; }


    [DataMember]
    public decimal Total { get; set; }

    [DataMember]
    public List<OrderItemSummaryDTO> Products { get; set; }
}

[DataContract]
public class OrderItemSummaryDTO
{
    [DataMember]
    public string Product { get; set; }

    [DataMember]
    public int Quantity { get; set; }
}
