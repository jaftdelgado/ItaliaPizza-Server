using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.ServiceModel.Channels;

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

        public bool DeleteCustomer(int customerID)
        {
            try
            {
                using (var context = new italiapizzaEntities())
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerID);
                    if (customer != null)
                    {
                        customer.IsActive = false;
                        context.SaveChanges();
                        return true; 
                    }
                    return false;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateCustomer(Customer updatedCustomer, Address updatedAddress)
        {
            try
            {
                using (var context = new italiapizzaEntities())
                {
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerID == updatedCustomer.CustomerID);
                    if (existingCustomer == null) return false;

                    var existingAddress = context.Addresses.FirstOrDefault(a => a.AddressID == existingCustomer.AddressID);
                    if (existingAddress == null) return false;

                    existingCustomer.FirstName = updatedCustomer.FirstName;
                    existingCustomer.LastName = updatedCustomer.LastName;
                    existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
                    existingCustomer.EmailAddress = updatedCustomer.EmailAddress;

                    existingAddress.AddressName = updatedAddress.AddressName;
                    existingAddress.ZipCode = updatedAddress.ZipCode;
                    existingAddress.City = updatedAddress.City;

                    context.SaveChanges();
                    return true;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ReactivateCustomer(int customerID)
        {
            try
            {
                using (var context = new italiapizzaEntities())
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerID);
                    if (customer != null && !customer.IsActive)
                    {
                        customer.IsActive = true;
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
