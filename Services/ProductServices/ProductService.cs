using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductManager
    {
        public int AddProduct(ProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.name,
                Category = productDTO.category,
                Price = productDTO.price,
                IsPrepared = productDTO.isPrepared,
                SupplierID = productDTO.SupplierID,
                Status = productDTO.Status,
                Photo = productDTO.Photo,
                Description = productDTO.Description,
                Code = productDTO.Code
            };
            return 0;
        }
    }
}
