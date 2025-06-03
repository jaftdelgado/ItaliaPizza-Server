using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dtos
{
    internal class DeliveryDTO
    {
    }

    [DataContract]
    public class DeliveryDriverDTO
    {
        [DataMember]
        public int PersonalID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
