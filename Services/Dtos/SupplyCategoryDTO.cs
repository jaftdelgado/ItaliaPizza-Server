using System.Runtime.Serialization;

[DataContract]
public class SupplyCategoryDTO
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }
}
