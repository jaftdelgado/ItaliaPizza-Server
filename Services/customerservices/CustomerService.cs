using Model;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CustomerService : ICustomerManager
    {

        public int AddCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                EmailAddress = customerDTO.EmailAddress,
                PhoneNumber = customerDTO.PhoneNumber,
            };

            var address = new Address
            {
                AddressName = customerDTO.Address.AddressName,
                ZipCode = customerDTO.Address.ZipCode,
                City = customerDTO.Address.City
            };

            CustomerDAO customerDAO = new CustomerDAO();
            int result = customerDAO.AddCustomer(customer, address);
            return result;
        }

        public List<CustomerDTO> GetCustomers()
        {
            var dao = new CustomerDAO();
            var customers = dao.GetCustomers();

            return customers.Select(c => new CustomerDTO
            {
                CustomerID = c.CustomerID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                EmailAddress = c.EmailAddress,
                PhoneNumber = c.PhoneNumber,
                IsActive = c.IsActive,
                AddressID = c.AddressID,
                Address = new AddressDTO
                {
                    Id = c.Address.AddressID,
                    AddressName = c.Address.AddressName,
                    ZipCode = c.Address.ZipCode,
                    City = c.Address.City
                }
            }).ToList();
        }

        public bool IsCustomerEmailAvailable(string email)
        {
            CustomerDAO customerDAO = new CustomerDAO();
            return customerDAO.IsCustomerEmailAvailable(email);
        }

        bool ICustomerManager.DeleteCustomer(int customerID)
        {
            throw new System.NotImplementedException();
        }

        bool ICustomerManager.ReactivateCustomer(int customerID)
        {
            throw new System.NotImplementedException();
        }

        bool ICustomerManager.UpdateCustomer(CustomerDTO customerDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
