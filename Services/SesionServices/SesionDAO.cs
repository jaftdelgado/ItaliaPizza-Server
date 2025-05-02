using Model;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     class SesionDAO
    {
        public PersonalDTO Login(string username, string password)
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
    }
}
