using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class WasteSupplyDTO
    {
        [DataMember]
        public int WasteID { get; set; }
        [DataMember]
        public decimal Quantity { get; set; }
        [DataMember]
        public int SupplyID { get; set; }
    }
}
