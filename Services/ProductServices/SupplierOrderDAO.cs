using System;
using Model;
using System.Data.Entity.Validation;

namespace Model.DAO
{
    public class SupplierOrderDAO
    {
        public int AddOrder(Supplier_Order order)
        {
            int result = 0;

            using (var context = new italiapizzaEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Supplier_Order.Add(order);
                        context.SaveChanges();
                        transaction.Commit();
                        result = 1;

                    }
                    catch (DbEntityValidationException ex)
                    {
                        transaction.Rollback();
                        foreach (var validationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
