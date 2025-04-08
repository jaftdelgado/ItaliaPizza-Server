using Model;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;

namespace Services
{
    public class SupplierDAO
    {
        private readonly italiapizzaEntities _context;

        public SupplierDAO() : this(new italiapizzaEntities()) { }

        public SupplierDAO(italiapizzaEntities context)
        {
            _context = context;
        }

        public int RegisterSupplier(Supplier supplier)
        {
            int result = 0;

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Suppliers.Add(supplier);
                        _context.SaveChanges();

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
                        result = 0;
                    }
                }
            }
            catch (EntityException ex) 
            {
                Console.WriteLine(ex.Message);
                result = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = 0;
            }

            return result;
        }
    }
}
