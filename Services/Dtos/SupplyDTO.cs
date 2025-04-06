using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplyDTO
    {
        [DataMember]
        public int SupplyID { get; set; }

        [DataMember]
        public string SupplyName { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public decimal Stock { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string MeasureUnit { get; set; }

        [DataMember]
        public int SupplierID { get; set; }

        [DataMember]
        public int SupplyCategoryID { get; set; }
    }
}
