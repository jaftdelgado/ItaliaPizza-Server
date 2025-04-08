using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

[DataContract]
public class SupplierOrderDTO
{
    [DataMember]
    public int SupplierID { get; set; }

    [DataMember]
    public DateTime OrderedDate { get; set; }

    [DataMember]
    public string OrderFolio { get; set; }

    [DataMember]
    public decimal Total { get; set; }

    [DataMember]
    public string Status { get; set; }

    [DataMember]
    public List<OrderItemDTO> Items { get; set; }

    [DataContract]
    public class OrderItemDTO
    {
        [DataMember]
        public int SupplyID { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}
