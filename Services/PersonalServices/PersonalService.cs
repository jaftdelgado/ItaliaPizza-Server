using Model;
using Services.Dtos;
using System;

namespace Services
{
    public class PersonalService : IPersonalManager
    {
        public int AddPersonal(PersonalDTO personalDTO)
        {
            var personal = new Personal
            {
                FirstName = personalDTO.FirstName,
                FatherName = personalDTO.FatherName,
                MotherName = personalDTO.MotherName,
                RFC = personalDTO.RFC,
                Username = personalDTO.Username,
                Password = personalDTO.Password,
                ProfilePic = personalDTO.ProfilePic,
                RoleID = personalDTO.RoleID,
                HireDate = DateTime.Now,
            };

            PersonalDAO personalDAO = new PersonalDAO();
            int result = personalDAO.AddPersonal(personal);
            return result;
        }

        public bool IsUsernameAvailable(string username)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsUsernameTaken(username);
        }

        public bool IsRfcUnique(string rfc)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsRfcTaken(rfc);
        }
    }
}
