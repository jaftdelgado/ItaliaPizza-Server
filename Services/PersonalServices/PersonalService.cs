using Model;
using Services.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class PersonalService : IPersonalManager
    {
        private readonly IPersonalDAO _dao;

        public PersonalService() : this(new PersonalDAO()) { }

        public PersonalService(IPersonalDAO dao)
        {
            _dao = dao;
        }

        public List<PersonalDTO> GetAllPersonals()
        {
            var personals = _dao.GetAllPersonals();

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

        public List<DeliveryDriverDTO> GetDeliveryDrivers()
        {
            var drivers = _dao.GetDeliveryDrivers();

            return drivers.Select(d => new DeliveryDriverDTO
            {
                PersonalID = d.PersonalID,
                FirstName = d.FirstName,
                LastName = d.LastName,
                PhoneNumber = d.PhoneNumber
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

            return _dao.AddPersonal(personal, address);
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

            return _dao.UpdatePersonal(updatedPersonal, updatedAddress);
        }

        public bool DeletePersonal(int personalID)
        {
            return _dao.DeletePersonal(personalID);
        }

        public bool ReactivatePersonal(int personalID)
        {
            return _dao.ReactivatePersonal(personalID);
        }

        public bool IsUsernameAvailable(string username)
        {
            return _dao.IsUsernameAvailable(username);
        }

        public bool IsRfcUnique(string rfc)
        {
            return _dao.IsRfcUnique(rfc);
        }

        public bool IsPersonalEmailAvailable(string email)
        {
            return _dao.IsPersonalEmailAvailable(email);
        }
    }
}
