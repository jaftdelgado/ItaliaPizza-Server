using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Model
{
    public interface IProductDAO
    {
        Product AddProduct(Product product);
        List<Product> GetAllProductsWithRecipe(bool activeOnly = false);
        bool UpdateProduct(Product updatedProduct);
        bool DeleteProduct(int productId);
        bool ReactivateProduct(int productId);
        bool IsProductUsedInOrders(int productId);
        bool ExistsProductCode(string code);
    }
    public class ProductDAO : IProductDAO
    {
        public Product AddProduct(Product product)
        {
            using (var context = new italiapizzaEntities())
            {
                context.Products.Add(product);
                context.SaveChanges();
                return product;
            }
        }

        public List<Product> GetAllProductsWithRecipe(bool activeOnly = false)
        {
            using (var context = new italiapizzaEntities())
            {
                var query = context.Products
                    .Include(p => p.Recipe.RecipeSteps)
                    .Include(p => p.Recipe.RecipeSupplies)
                    .AsQueryable();

                if (activeOnly)
                {
                    query = query.Where(p => p.IsActive);
                }

                return query.ToList();
            }
        }


        public bool UpdateProduct(Product updatedProduct)
        {
            using (var context = new italiapizzaEntities())
            {
                var existingProduct = context.Products.FirstOrDefault(p => p.ProductID == updatedProduct.ProductID);
                if (existingProduct == null)
                    return false;

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Category = updatedProduct.Category;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.IsPrepared = updatedProduct.IsPrepared;
                existingProduct.ProductPic = updatedProduct.ProductPic;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.SupplyID = updatedProduct.SupplyID;
                existingProduct.RecipeID = updatedProduct.RecipeID;

                context.SaveChanges();
                return true;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (var context = new italiapizzaEntities())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product != null)
                {
                    product.IsActive = false;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool ReactivateProduct(int productId)
        {
            using (var context = new italiapizzaEntities())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product != null && !product.IsActive)
                {
                    product.IsActive = true;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool IsProductUsedInOrders(int productId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Product_Order.Any(po => po.ProductID == productId);
            }
        }

        public bool ExistsProductCode(string code)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Products.Any(p => p.ProductCode == code);
            }
        }
    }
}
