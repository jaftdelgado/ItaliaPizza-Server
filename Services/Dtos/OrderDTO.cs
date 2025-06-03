﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class OrderDTO
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public int? CustomerID { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public decimal? Total { get; set; }

        [DataMember]
        public bool? IsDelivery { get; set; }

        [DataMember]
        public int? PersonalID { get; set; }

        [DataMember]
        public string AttendedByName { get; set; }

        [DataMember]
        public int? DeliveryID { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string TableNumber { get; set; }

        [DataMember]
        public List<ProductOrderDTO> Items { get; set; }

        [DataMember]
        public DeliveryDTO DeliveryInfo { get; set; }
    }

    [DataContract]
    public class ProductOrderDTO
    {
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int? Quantity { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public byte[] ProductPic { get; set; }
    }


    [DataContract]
    public class DeliveryDTO
    {
        [DataMember]
        public int DeliveryID { get; set; }

        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public int DeliveryDriverID { get; set; }

        [DataMember]
        public string CustomerFullName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string DeliveryDriverName { get; set; }
    }

}
