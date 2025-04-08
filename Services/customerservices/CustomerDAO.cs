using Model;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;

namespace Services.CustomerServices
{
    public class CustomerDAO
    {
        private readonly italiapizzaEntities _context;

        public CustomerDAO() : this (new italiapizzaEntities()) { }

        public CustomerDAO(italiapizzaEntities context)
        {
            _context = context;
        }

        public int RegisterCustomer(Customer customer)
        {
            int result = 0;

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Customers.Add(customer);
                        _context.SaveChanges();

                        var address = new Address
                        {
                            AddressID = customer.CustomerID,
                            AddressName = "Prueba 1",
                            ZipCode = "99999",
                            City = "Evergreen"
                        };

                        _context.Addresses.Add(address);
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
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = 0;
            }

            return result;
        }
    }
}
