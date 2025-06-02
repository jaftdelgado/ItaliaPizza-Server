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
                SupplyID = productDTO.SupplyID,
                RecipeID = productDTO.RecipeID
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
            var products = productDAO.GetAllProductsWithRecipe(activeOnly);
            var productDTOs = new List<ProductDTO>();

            foreach (var product in products)
            {
                var dto = new ProductDTO
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    Category = product.Category,
                    Price = product.Price,
                    IsPrepared = product.IsPrepared,
                    ProductPic = product.ProductPic,
                    Description = product.Description,
                    ProductCode = product.ProductCode,
                    IsActive = product.IsActive,
                    SupplyID = product.SupplyID,
                    RecipeID = product.RecipeID,
                    IsDeletable = !productDAO.IsProductUsedInOrders(product.ProductID),
                    Recipe = null
                };

                if (product.RecipeID.HasValue && product.Recipe != null)
                {
                    dto.Recipe = new RecipeDTO
                    {
                        RecipeID = product.Recipe.RecipeID,
                        PreparationTime = product.Recipe.PreparationTime,
                        Steps = product.Recipe.RecipeSteps?
                            .OrderBy(rs => rs.StepNumber)
                            .Select(rs => new RecipeStepDTO
                            {
                                RecipeStepID = rs.RecipeStepID,
                                RecipeID = rs.RecipeID,
                                StepNumber = rs.StepNumber,
                                Instruction = rs.Instruction // corregido campo
                            }).ToList(),

                        Supplies = product.Recipe.RecipeSupplies?
                            .Select(rsp => new RecipeSupplyDTO
                            {
                                RecipeSupplyID = rsp.RecipeSupplyID,
                                RecipeID = rsp.RecipeID,
                                SupplyID = rsp.SupplyID,
                                UseQuantity = rsp.UseQuantity // corregido campo
                            }).ToList()
                    };
                }

                productDTOs.Add(dto);
            }

            return productDTOs;
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
                SupplyID = productDTO.SupplyID,
                RecipeID = productDTO.RecipeID 
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
