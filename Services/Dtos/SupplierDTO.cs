using System.Runtime.Serialization;

[DataContract]
public class SupplierDTO
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string ContactName { get; set; }

    [DataMember]
    public string PhoneNumber { get; set; }

    [DataMember]
    public int CategorySupply { get; set; } 
}
