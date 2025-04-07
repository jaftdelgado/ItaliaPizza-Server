using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplyDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string MeasureUnit { get; set; }
    }
}
