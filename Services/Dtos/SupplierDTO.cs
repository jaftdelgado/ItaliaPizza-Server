using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplierDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
