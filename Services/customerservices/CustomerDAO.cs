using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Services
{
    public class CustomerDAO
    {
        public int AddCustomer(Customer customer, Address address)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Addresses.Add(address);
                        context.SaveChanges();

                        customer.AddressID = address.AddressID;

                        context.Customers.Add(customer);
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

        public bool IsCustomerEmailAvailable(string email)
        {
            using (var context = new italiapizzaEntities())
            {
                return !context.Customers.Any(p => p.EmailAddress == email);
            }
        }

        public List<Customer> GetCustomers()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Customers.Include("Address").ToList();
            }
        }
    }
}
