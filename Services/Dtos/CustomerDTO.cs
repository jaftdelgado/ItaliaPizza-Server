using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class CustomerDTO
    {
        [DataMember]
        public int CustomerID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public DateTime RegDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public AddressDTO Address { get; set; }

    }
}
