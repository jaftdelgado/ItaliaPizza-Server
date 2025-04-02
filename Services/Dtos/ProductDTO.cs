using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Services.Dtos
{
    [DataContract]
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
        public bool isPrepared { get; set; }
        public int SupplierID { get; set; }
        public string Status { get; set; }
        public byte[] Photo { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
