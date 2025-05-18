using System.Runtime.Serialization;

namespace Services.Dtos
{
    [DataContract]
    public class SupplyDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int MeasureUnit { get; set; }

        [DataMember]
        public int SupplyCategoryID { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public int? SupplierID { get; set; }

        [DataMember]
        public decimal Stock { get; set; }

        [DataMember]
        public byte[] SupplyPic { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        public bool IsDeletable { get; set; }
    }
}