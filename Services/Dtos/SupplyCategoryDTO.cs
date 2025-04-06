using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplyCategoryDTO
    {
        [DataMember]
        public int SupplyCategoryID { get; set; }

        [DataMember]
        public string CategoryName { get; set; }
    }
}
