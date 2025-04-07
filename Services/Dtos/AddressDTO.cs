using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class AddressDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string AddressName { get; set; }

        [DataMember]
        public int ZipCode { get; set; }

        [DataMember]
        public string District { get; set; }
    }
}
