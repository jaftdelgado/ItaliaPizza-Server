using System;
using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class PersonalDTO
    {
        [DataMember]
        public int PersonalID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string FatherName { get; set; }

        [DataMember]
        public string MotherName { get; set; }

        [DataMember]
        public string RFC { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public byte[] ProfilePic { get; set; }

        [DataMember]
        public DateTime HireDate { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int RoleID { get; set; }
    }

}
