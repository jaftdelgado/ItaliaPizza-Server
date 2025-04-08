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
                LastName = personalDTO.LastName,
                RFC = personalDTO.RFC,
                EmailAddress = personalDTO.EmailAddress,
                PhoneNumber = personalDTO.PhoneNumber,
                Username = personalDTO.Username,
                Password = personalDTO.Password,
                ProfilePic = personalDTO.ProfilePic,
                IsActive = true,
                RoleID = personalDTO.RoleID,
                HireDate = DateTime.Now,
            };

            var address = new Address
            {
                AddressName = personalDTO.Address.AddressName,
                ZipCode = personalDTO.Address.ZipCode,
                City = personalDTO.Address.City
            };

            PersonalDAO personalDAO = new PersonalDAO();
            int result = personalDAO.AddPersonal(personal, address);
            return result;
        }


        public bool IsUsernameAvailable(string username)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsUsernameAvailable(username);
        }

        public bool IsRfcUnique(string rfc)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsRfcUnique(rfc);
        }

        public bool IsEmailAvailable(string email)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsEmailAvailable(email);
        }
    }
}
