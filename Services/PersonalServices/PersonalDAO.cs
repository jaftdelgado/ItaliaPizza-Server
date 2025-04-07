using Model;
using System.Data.Entity.Validation;
using System;
using System.Linq;

namespace Services
{
    public class PersonalDAO
    {
        public int AddPersonal(Personal personal)
        {
            int result = 0;
            using (var context = new italiapizzaEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
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
    }
}
