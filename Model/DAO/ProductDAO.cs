using System;
using System.Data.Entity.Validation;

namespace Model.DAO
{
    public class ProductDAO
    {
        public int AddProduct(Product product)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Products.Add(product);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        result = 1;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        dbContextTransaction.Rollback();
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                        result = 0;
                    }
                }
            }
            return result;
        }
    }
}
