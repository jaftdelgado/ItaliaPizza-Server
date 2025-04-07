using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplyCategoryDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
