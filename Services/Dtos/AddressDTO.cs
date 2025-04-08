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
        public string ZipCode { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}
