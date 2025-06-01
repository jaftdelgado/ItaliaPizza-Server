using Model;
using ServerUtilities;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ProductService : IProductManager
    {
        private readonly ProductDAO productDAO = new ProductDAO();

        public int AddProduct(ProductDTO productDTO)
        {
            if (productDTO.IsPrepared == true) productDTO.SupplyID = null;

            productDTO.ProductCode = ProductCodeGenerator.GenerateUniqueProductCode(
                productDTO.Category.Value, code => productDAO.ExistsProductCode(code)
            );

            var product = new Product
            {
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price,
                IsPrepared = productDTO.IsPrepared,
                ProductPic = productDTO.ProductPic,
                Description = productDTO.Description,
                ProductCode = productDTO.ProductCode,
                IsActive = true,
                SupplyID = productDTO.SupplyID
            };

            var result = productDAO.AddProduct(product);

            if (result != null)
            {
                productDTO.ProductID = result.ProductID;
                return productDTO.ProductID;
            }
            else
                return -1;
        }

        public List<ProductDTO> GetAllProducts(bool activeOnly = false)
        {
            var products = productDAO.GetAllProducts(activeOnly);
            return products.Select(p => new ProductDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                IsPrepared = p.IsPrepared,
                ProductPic = p.ProductPic,
                Description = p.Description,
                ProductCode = p.ProductCode,
                IsActive = p.IsActive,
                SupplyID = p.SupplyID,
                IsDeletable = !productDAO.IsProductUsedInOrders(p.ProductID)
            }).ToList();
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            var updatedProduct = new Product
            {
                ProductID = productDTO.ProductID,
                Name = productDTO.Name,
                Category = productDTO.Category,
                Price = productDTO.Price,
                IsPrepared = productDTO.IsPrepared,
                ProductPic = productDTO.ProductPic,
                Description = productDTO.Description,
                SupplyID = productDTO.SupplyID
            };

            return productDAO.UpdateProduct(updatedProduct);
        }

        public bool DeleteProduct(int productId)
        {
            if (!productDAO.IsProductUsedInOrders(productId))
            {
                return productDAO.DeleteProduct(productId);
            }
            return false;
        }

        public bool ReactivateProduct(int productId)
        {
            return productDAO.ReactivateProduct(productId);
        }

        public bool IsProductDeletable(int productId)
        {
            return !productDAO.IsProductUsedInOrders(productId);
        }
    }
}
