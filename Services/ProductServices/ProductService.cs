using Model;
using Services.Dtos;

namespace Services
{
    public class ProductService : IProductManager
    {
        public ProductDTO AddProduct(ProductDTO productDTO)
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
            Product result = productDAO.AddProduct(product);
            if (result != null)
            {
                productDTO.Id = result.ProductID;
                return productDTO;
            }
            else
            {
                return null;
            }
        }
    }
}
