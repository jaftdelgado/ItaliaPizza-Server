using Model;
using System.Data.Entity.Validation;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class PersonalDAO
    {
        public List<Personal> GetAllPersonals()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Personals.Include("Address").ToList();
            }
        }

        public int AddPersonal(Personal personal, Address address)
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

                        personal.AddressID = address.AddressID;

                        context.Personals.Add(personal);
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

        public bool DeletePersonal(int personalID)
        {
            using (var context = new italiapizzaEntities())
            {
                var personal = context.Personals.FirstOrDefault(p => p.PersonalID == personalID);
                if (personal != null)
                {
                    personal.IsActive = false;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool IsUsernameAvailable(string username)
        {
            using (var context = new italiapizzaEntities())
            {
                return !context.Personals.Any(p => p.Username == username);
            }
        }

        public bool IsRfcUnique(string rfc)
        {
            using (var context = new italiapizzaEntities())
            {
                return !context.Personals.Any(p => p.RFC == rfc);
            }
        }

        public bool IsPersonalEmailAvailable(string email)
        {
            using (var context = new italiapizzaEntities())
            {
                return !context.Personals.Any(p => p.EmailAddress == email);
            }
        }
    }
}
