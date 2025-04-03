using Model;
using Model.DAO;
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
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price,
                IsPrepared = productDTO.IsPrepared,
                SupplierID = productDTO.SupplierID,
                Status = productDTO.Status,
                Photo = productDTO.Photo,
                Description = productDTO.Description,
                Code = productDTO.Code
            };

            ProductDAO productDAO = new ProductDAO();
            int result = productDAO.AddProduct(product);
            return result;
        }
    }
}
