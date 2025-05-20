using System.Runtime.Serialization;

[DataContract]
public class SupplierDTO
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string SupplierName { get; set; }
    
    [DataMember]
    public string ContactName { get; set; }

    [DataMember]
    public string PhoneNumber { get; set; }
    
    [DataMember]
    public string EmailAddress { get; set; }
    
    [DataMember]
    public string Description { get; set; }
    
    [DataMember]
    public bool IsActive { get; set; }

    [DataMember]
    public int CategorySupply { get; set; }
    [DataMember]
    public bool IsDeletable { get; set; }
}
