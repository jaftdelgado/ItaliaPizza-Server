using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace Services.Dtos
{
    [DataContract]
    public class SupplierOrderDTO
    {
        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public string SupplierName { get; set; } 

        [DataMember]
        public DateTime OrderedDate { get; set; }

        [DataMember]
        public DateTime? Delivered { get; set; }

        [DataMember]
        public string OrderFolio { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public int? Status { get; set; }

        [DataMember]
        public List<OrderItemDTO> Items { get; set; }

        [DataContract]
        public class OrderItemDTO
        {
            [DataMember]
            public int SupplyID { get; set; }

            [DataMember]
            public string SupplyName { get; set; }

            [DataMember]
            public byte[] SupplyPic { get; set; }

            [DataMember]
            public decimal Quantity { get; set; }

            [DataMember]
            public decimal Subtotal { get; set; }

            [DataMember]
            public decimal UnitPrice { get; set; }

            [DataMember]
            public int MeasureUnit { get; set; }
        }
    }
}
