using Model;
using Services.Dtos;

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
                Status = "Activo",
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
