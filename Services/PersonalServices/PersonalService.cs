using Model;
using Services.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class PersonalService : IPersonalManager
    {
        public List<PersonalDTO> GetAllPersonals()
        {
            var dao = new PersonalDAO();
            var personals = dao.GetAllPersonals();

            return personals.Select(p => new PersonalDTO
            {
                PersonalID = p.PersonalID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                RFC = p.RFC,
                Username = p.Username,
                Password = p.Password,
                ProfilePic = p.ProfilePic,
                HireDate = p.HireDate,
                IsActive = p.IsActive,
                RoleID = p.RoleID,
                EmailAddress = p.EmailAddress,
                PhoneNumber = p.PhoneNumber,
                AddressID = p.AddressID,
                IsOnline = p.IsOnline,
                Address = new AddressDTO
                {
                    Id = p.Address.AddressID,
                    AddressName = p.Address.AddressName,
                    ZipCode = p.Address.ZipCode,
                    City = p.Address.City
                }
            }).ToList();
        }

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

        public bool UpdatePersonal(PersonalDTO personalDTO)
        {
            var updatedPersonal = new Personal
            {
                PersonalID = personalDTO.PersonalID,
                FirstName = personalDTO.FirstName,
                LastName = personalDTO.LastName,
                RFC = personalDTO.RFC,
                EmailAddress = personalDTO.EmailAddress,
                PhoneNumber = personalDTO.PhoneNumber,
                ProfilePic = personalDTO.ProfilePic,
                RoleID = personalDTO.RoleID
            };

            var updatedAddress = new Address
            {
                AddressID = personalDTO.AddressID,
                AddressName = personalDTO.Address.AddressName,
                ZipCode = personalDTO.Address.ZipCode,
                City = personalDTO.Address.City
            };

            var dao = new PersonalDAO();
            return dao.UpdatePersonal(updatedPersonal, updatedAddress);
        }

        public bool DeletePersonal(int personalID)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.DeletePersonal(personalID);
        }

        public bool ReactivatePersonal(int personalID)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.ReactivatePersonal(personalID);
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

        public bool IsPersonalEmailAvailable(string email)
        {
            PersonalDAO personalDAO = new PersonalDAO();
            return personalDAO.IsPersonalEmailAvailable(email);
        }
    }
}
