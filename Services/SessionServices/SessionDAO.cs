using Model;
using Services.Dtos;
using System.Linq;

namespace Services
{
    public interface ISessionDAO
    {
        PersonalDTO SignIn(string username, string password);
        int UpdateActivity(int personalID);
        int SignOut(int personalID);
    }

    public class SessionDAO : ISessionDAO
    {
        public PersonalDTO SignIn(string username, string password)
        {
            using (var context = new italiapizzaEntities())
            {
                var personal = context.Personals
                    .FirstOrDefault(p => p.Username == username && p.Password == password && p.IsActive == true);
                if (personal != null)
                {
                    return new PersonalDTO
                    {
                        PersonalID = personal.PersonalID,
                        FirstName = personal.FirstName,
                        LastName = personal.LastName,
                        Username = personal.Username,
                        EmailAddress = personal.EmailAddress,
                        PhoneNumber = personal.PhoneNumber,
                        RFC = personal.RFC,
                        ProfilePic = personal.ProfilePic,
                        RoleID = personal.RoleID
                        
                    };
                }
                return null;
            }
        }
        public int UpdateActivity(int personalID)
        {
            using (var context = new italiapizzaEntities())
            {
                var personal = context.Personals.FirstOrDefault(p => p.PersonalID == personalID);
                if (personal != null)
                {
                    personal.IsOnline = true;
                    context.SaveChanges();
                    return 1;
                }
                return 0;
            }
        }

        public int SignOut(int personalID)
        {
            using (var context = new italiapizzaEntities())
            {
                var personal = context.Personals.FirstOrDefault(p => p.PersonalID == personalID);
                if (personal != null)
                {
                    personal.IsOnline = false;
                    context.SaveChanges();
                    return 1;
                }
                return 0;
            }
        }
    }
}
